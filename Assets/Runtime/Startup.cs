using System;
using System.Linq;
using SkillGraph.Controllers;
using SkillGraph.Models;
using SkillGraph.Screens.Services;
using SkillGraph.SkillSystem.Modules;
using SkillGraph.Systems;
using SkillGraph.Views;
using UnityEngine;

namespace SkillGraph
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private SkillDataContainer[] _skillDataContainer;
        [SerializeField] private CloseScreenContainer _closeScreenContainer;
        [SerializeField] private SkillGraphView _skillGraph;
        [SerializeField] private SkillScreenView _skillScreen;
        [SerializeField] private WarningScreenView _warningScreenView;
        [SerializeField] private SkillPointIncreaseProxy _skillPointIncreaseProxy;

        private IScreenService _screenService;

        private IInstallable[] _installers;
        private IDisposable[] _disposables;

        private void Awake()
        {
            _screenService = new ScreenService();

            var skillPointWallet = new Wallet();

            _closeScreenContainer.SetScreenService(_screenService);
            _skillPointIncreaseProxy.SetWallet(skillPointWallet);

            var skillModule = new SkillModule(_skillDataContainer);

            var widgets = _skillDataContainer.Select(container => container.GetWidget()).ToArray();

            var skillController = new SkillGraphController(_screenService, skillModule,
                skillPointWallet, widgets, _skillGraph, _skillScreen, _warningScreenView);

            _installers = new IInstallable[] { skillController };
            _disposables = new IDisposable[] { skillController };
        }

        private void Start()
        {
            foreach (var installer in _installers)
            {
                installer.Install();
            }
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}