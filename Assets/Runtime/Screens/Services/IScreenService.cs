using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Screens.Views;

namespace SkillGraph.Screens.Services
{
    public interface IScreenService
    {
        void ShowImmediately(ScreenViewBase screenView);

        UniTask ShowAsync(ScreenViewBase screenView, CancellationToken cancellationToken);

        UniTask HideAsync(ScreenViewBase screenView, CancellationToken cancellationToken);
    }
}