using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Models;
using SkillGraph.Screens.Services;
using SkillGraph.SkillSystem.Exceptions;
using SkillGraph.SkillSystem.Models;
using SkillGraph.SkillSystem.Modules;
using SkillGraph.Systems;
using SkillGraph.Views;

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
        private readonly WarningScreenView _warningScreenView;

        private readonly List<Skill> _cashedSkillList = new();

        public SkillGraphController(IScreenService screenService, SkillModule skillModule, Wallet skillPointsWallet,
            SkillWidgetView[] widgets, SkillGraphView skillGraph, SkillScreenView skillScreenView,
            WarningScreenView warningScreenView)
        {
            _screenService = screenService;
            _skillModule = skillModule;

            _skillPointsWallet = skillPointsWallet;

            _widgets = widgets;

            _skillGraph = skillGraph;
            _skillScreenView = skillScreenView;
            _warningScreenView = warningScreenView;
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

                widget.Clicked += () => OpenSkillScreen(_skillModule.GetSkill(widget));

                skill.Active += value => OnSkillStateChanged(widget, value);

                widget.VisualizeStateAsync(skill.Active).Forget();
            }

            _skillGraph.SetBalance(_skillPointsWallet.Balance);

            _skillGraph.Clear += ClearTree;

            _skillScreenView.ResetClicked += ResetSkill;
            _skillScreenView.PurchaseClicked += PurchaseSkill;
            _skillPointsWallet.BalanceChanged += UpdateBalance;
        }

        void IDisposable.Dispose()
        {
            foreach (var widget in _widgets)
            {
                widget.Dispose();
            }

            _skillGraph.Clear -= ClearTree;
            _skillScreenView.ResetClicked -= ResetSkill;
            _skillScreenView.PurchaseClicked -= PurchaseSkill;
            _skillPointsWallet.BalanceChanged -= UpdateBalance;
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
                if (!_skillModule.Current.AdjacentSkills.Any(a => a.Active))
                {
                    _warningScreenView.SetMessage("Skill is not connected with head");
                    _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
                    return;
                }

                _skillPointsWallet.Withdraw(_skillModule.Current.Price);
            }
            catch (OverdraftException exception)
            {
                _warningScreenView.SetMessage(exception.Message);

                _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
                return;
            }
            catch (ArgumentException exception)
            {
                _warningScreenView.SetMessage(exception.Message);

                _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
                return;
            }

            _skillModule.Current.Active.Value = true;
            _screenService.HideAsync(_skillScreenView, CancellationToken.None).Forget();
        }

        private void ResetSkill()
        {
            try
            {
                var skill = _skillModule.Current;

                if (skill.Persistent)
                {
                    _warningScreenView.SetMessage("Cant to reset base skill");

                    _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();

                    return;
                }

                foreach (var adjacentSkill in skill.AdjacentSkills)
                {
                    _cashedSkillList.Clear();
                    _cashedSkillList.Add(skill);

                    if (!adjacentSkill.Active)
                        continue;

                    if (!IsSkillConnectedWithBase(adjacentSkill))
                    {
                        _warningScreenView.SetMessage(
                            "Skill cant be reset because adjacent skill is not connected with head");

                        _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
                    }
                }

                skill.Active.Value = false;

                _skillPointsWallet.Put(skill.Price);

                _screenService.HideAsync(_skillScreenView, CancellationToken.None).Forget();
            }
            catch (ArgumentException exception)
            {
                _warningScreenView.SetMessage(exception.Message);

                _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
            }
            catch (SkillResetException exception)
            {
                _warningScreenView.SetMessage(exception.Message);

                _screenService.ShowAsync(_warningScreenView, CancellationToken.None).Forget();
            }
        }

        private bool IsSkillConnectedWithBase(Skill verifiableSkill)
        {
            if (verifiableSkill.Persistent)
                return true;

            foreach (var adjacentSkill in verifiableSkill.AdjacentSkills)
            {
                if (_cashedSkillList.Contains(adjacentSkill))
                    continue;

                _cashedSkillList.Add(adjacentSkill);

                if (!adjacentSkill.Active)
                    continue;

                if (IsSkillConnectedWithBase(adjacentSkill))
                    return true;
            }

            throw new SkillResetException(verifiableSkill.Name);
        }

        private void OnSkillStateChanged(SkillWidgetView widget, bool isActivated)
        {
            widget.VisualizeStateAsync(isActivated).Forget();
        }

        private void ClearTree()
        {
            var skills = _skillModule.GetAllSkill();

            foreach (var skill in skills)
            {
                if (skill.Persistent || !skill.Active) continue;

                skill.Active.Value = false;

                _skillPointsWallet.Put(skill.Price);
            }
        }
    }
}