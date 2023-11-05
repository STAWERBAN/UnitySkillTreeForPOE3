using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Screens.Views;

namespace SkillGraph.Screens.Services
{
    public class ScreenService : IScreenService
    {
        private readonly List<ScreenViewBase> _activeScreens;

        public ScreenService()
        {
            _activeScreens = new List<ScreenViewBase>();
        }

        public void ShowImmediately(ScreenViewBase screenView)
        {
            if (_activeScreens.Contains(screenView))
            {
                return;
            }

            _activeScreens.Add(screenView);

            screenView.ShowImmediately();
        }

        public async UniTask ShowAsync(ScreenViewBase screenView, CancellationToken cancellationToken)
        {
            try
            {
                if (_activeScreens.Contains(screenView))
                {
                    return;
                }

                _activeScreens.Add(screenView);

                await screenView.ShowAsync(cancellationToken);
            }
            catch
            {
                screenView.HideImmediately();
            }
        }

        public async UniTask HideAsync(ScreenViewBase screenView, CancellationToken cancellationToken)
        {
            try
            {
                await screenView.HideAsync(cancellationToken);
            }
            catch
            {
                screenView.HideImmediately();
            }
            finally
            {
                _activeScreens.Remove(screenView);
            }
        }
    }
}