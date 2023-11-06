using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace SkillGraph.Screens.Visualization
{
    public interface IScreenDisplayStrategy
    {
        UniTask ShowAsync(CancellationToken cancellationToken);

        UniTask HideAsync(CancellationToken cancellationToken);

        void Show();

        void Hide();

        void Validate(GameObject root) { }
    }
}