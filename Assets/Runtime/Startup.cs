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
        [SerializeField] private SkillGraphView _skillGraph;
        [SerializeField] private SkillScreenView _skillScreen;
        [SerializeField] private SceneDataContainer _sceneDataContainer;
        [SerializeField] private CloseScreenProxy _closeScreenProxy;

        private IScreenService _screenService;

        private IInstallable[] _installers;
        private IDisposable[] _disposables;

        private void Awake()
        {
            _screenService = new ScreenService();

            _closeScreenProxy.SetScreenService(_screenService);

            var skillPointWallet = new Wallet();

            var skillModule = new SkillModule(_skillDataContainer);

            var widgets = _skillDataContainer.Select(container => container.GetWidget()).ToArray();

            var skillController = new SkillGraphController(_screenService, skillModule,
                skillPointWallet, widgets, _skillGraph, _skillScreen, _sceneDataContainer);

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