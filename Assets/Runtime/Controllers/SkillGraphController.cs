using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Models;
using SkillGraph.Screens.Services;
using SkillGraph.Screens.Views;
using SkillGraph.SkillSystem.Exceptions;
using SkillGraph.SkillSystem.Models;
using SkillGraph.SkillSystem.Modules;
using SkillGraph.SkillSystem.Utilities;
using SkillGraph.Systems;
using SkillGraph.Views;
using UnityEngine;

namespace SkillGraph.Controllers
{
    public class SkillGraphController : IInstallable, IDisposable
    {
        private readonly IScreenService _screenService;

        private readonly SkillModule _skillModule;

        private readonly Wallet _skillPointsWallet;
        private readonly SkillWidgetView[] _widgets;

        private readonly SkillGraphView _skillGraph;
        private readonly SkillScreenView _skillScreenView;

        private readonly SceneDataContainer _sceneDataContainer;

        public SkillGraphController(IScreenService screenService,
            SkillModule skillModule,
            Wallet skillPointsWallet,
            SkillWidgetView[] widgets,
            SkillGraphView skillGraph,
            SkillScreenView skillScreenView,
            SceneDataContainer sceneDataContainer)
        {
            _screenService = screenService;
            _skillModule = skillModule;

            _skillPointsWallet = skillPointsWallet;

            _widgets = widgets;

            _skillGraph = skillGraph;
            _skillScreenView = skillScreenView;
            _sceneDataContainer = sceneDataContainer;
        }

        void IInstallable.Install()
        {
            foreach (var widget in _widgets)
            {
                var skill = _skillModule.GetSkill(widget);

                if (skill == null)
                {
                    continue;
                }

                widget.Clicked += () => OpenSkillScreen(skill);

                skill.Active += value => OnSkillStateChanged(widget, value);

                widget.VisualizeStateAsync(skill.Active).Forget();
            }

            _skillGraph.SetBalance(_skillPointsWallet.Balance);

            _skillPointsWallet.BalanceChanged += UpdateBalance;

            _skillScreenView.PurchaseClicked += PurchaseSkill;
            _skillScreenView.ResetClicked += ResetSkill;
        }

        void IDisposable.Dispose()
        {
            _skillPointsWallet.BalanceChanged -= UpdateBalance;

            _skillScreenView.PurchaseClicked -= PurchaseSkill;
            _skillScreenView.ResetClicked -= ResetSkill;
        }

        private void OpenSkillScreen(Skill skill)
        {
            _skillScreenView.SetTitle(skill.Name);
            _skillScreenView.SetDescription(skill.Description);
            _skillScreenView.SetPrice(skill.Price);
            _skillScreenView.SetPurchaseState(skill.Active.Value);

            _screenService.ShowAsync(_skillScreenView, CancellationToken.None).Forget();

            _skillModule.Current = skill;
        }

        private void UpdateBalance(int balance)
        {
            _skillGraph.SetBalance(balance);
        }

        private void PurchaseSkill()
        {
            try
            {
                _skillPointsWallet.Withdraw(_skillModule.Current.Price);
            }
            catch (OverdraftException exception)
            {
                Debug.LogException(exception);

                _screenService.ShowAsync(_sceneDataContainer.OverdraftBalanceScreen, CancellationToken.None);
                return;
            }
            catch (ArgumentException exception)
            {
                Debug.LogException(exception);

                _screenService.ShowAsync(_sceneDataContainer.NegativeBalanceScreen, CancellationToken.None);
                return;
            }

            _skillModule.Current.Active.Value = true;
        }

        private void ResetSkill()
        {
            try
            {
                var skill = _skillModule.Current;

                if (skill.Persistent)
                {
                    _screenService.ShowAsync(_sceneDataContainer.BaseSkillResetScreen, CancellationToken.None);
                    return;
                }

                var cashedSkillList = new List<Skill>();

                foreach (var adjacentSkill in skill.AdjacentSkills)
                {
                    cashedSkillList.Clear();
                    cashedSkillList.Add(adjacentSkill);

                    if (!adjacentSkill.Active)
                        continue;

                    if (!IsSkillConnectedWithBase(adjacentSkill, cashedSkillList))
                    {
                        _screenService.ShowAsync(_sceneDataContainer.ResetSkillWarningScreen, CancellationToken.None);
                    }
                }

                _skillPointsWallet.Put(skill.Price);
            }
            catch (ArgumentException exception)
            {
                Debug.LogException(exception);

                _screenService.ShowAsync(_sceneDataContainer.NegativeBalanceScreen, CancellationToken.None).Forget();
            }
            catch (SkillResetException)
            {
                _screenService.ShowAsync(_sceneDataContainer.ResetSkillWarningScreen, CancellationToken.None).Forget();
            }
        }

        private bool IsSkillConnectedWithBase(Skill verifiableSkill, List<Skill> cashedSkillList)
        {
            if (verifiableSkill.Persistent)
                return true;

            foreach (var adjacentSkill in verifiableSkill.AdjacentSkills)
            {
                if (cashedSkillList.Contains(adjacentSkill))
                    continue;

                cashedSkillList.Add(adjacentSkill);

                if (!adjacentSkill.Active)
                    continue;

                if (IsSkillConnectedWithBase(adjacentSkill, cashedSkillList))
                    return true;
            }

            throw new SkillResetException();
        }

        private void OnSkillStateChanged(SkillWidgetView widget, bool isActivated)
        {
            widget.VisualizeStateAsync(isActivated).Forget();
        }
    }
}