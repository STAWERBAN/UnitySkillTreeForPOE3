using System.Threading;
using Cysharp.Threading.Tasks;
using SkillGraph.Screens.Visualization;
using UnityEngine;

namespace SkillGraph.Screens.Views
{
    public abstract class ScreenViewBase : MonoBehaviour
    {
        [SerializeReference] private IScreenDisplayStrategy _displayStrategy;

        private void OnValidate()
        {
            _displayStrategy ??= GetStrategy();
            _displayStrategy.Validate(gameObject);
        }

        protected abstract IScreenDisplayStrategy GetStrategy();

        internal async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            await _displayStrategy.ShowAsync(cancellationToken);
        }

        internal async UniTask HideAsync(CancellationToken cancellationToken)
        {
            await _displayStrategy.HideAsync(cancellationToken);
        }

        internal void ShowImmediately()
        {
            _displayStrategy.Show();
        }

        internal void HideImmediately()
        {
            _displayStrategy.Hide();
        }
    }
}