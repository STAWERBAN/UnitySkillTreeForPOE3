using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace SkillGraph.Screens.Visualization
{
    [Serializable]
    internal class DefaultAppearance : IScreenDisplayStrategy
    {
        [SerializeField]
        private GameObject _content;

        UniTask IScreenDisplayStrategy.ShowAsync(CancellationToken cancellationToken)
        {
            _content.SetActive(true);

            return UniTask.CompletedTask;
        }

        UniTask IScreenDisplayStrategy.HideAsync(CancellationToken cancellationToken)
        {
            _content.SetActive(false);

            return UniTask.CompletedTask;
        }

        void IScreenDisplayStrategy.Show()
        {
            _content.SetActive(true);
        }

        void IScreenDisplayStrategy.Hide()
        {
            _content.SetActive(false);
        }
    }
}