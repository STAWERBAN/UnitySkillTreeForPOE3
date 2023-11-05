#if UNITASK_DOTWEEN_SUPPORT
using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace SkillGraph.Screens.Visualization
{
    [Serializable]
    public class BounceAppearance : IScreenDisplayStrategy
    {
        [SerializeField]
        private GameObject _content;

        [SerializeField]
        private Image _fadeLayout;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private Vector3[] _appearValues;

        [SerializeField]
        private Vector3[] _disappearValues;

        [SerializeField]
        private float _appearanceDuration = 0.5f;

        void Validate(GameObject root)
        {
            if (_content == null)
            {
                _content = root.transform.childCount > 0
                    ? root.transform.GetChild(0).gameObject
                    : root;
            }

            if (_appearValues.Length == 0)
            {
                _appearValues = new[]
                {
                    new Vector3(1.15f, 1.15f, 1.15f),
                    new Vector3(0.9f, 0.9f, 0.9f),
                    new Vector3(1f, 1f, 1f)
                };
            }

            if (_disappearValues.Length == 0)
            {
                _disappearValues = new[]
                {
                    new Vector3(1.15f, 1.15f, 1.15f),
                    new Vector3(0f, 0f, 0f)
                };
            }
        }

        async UniTask IScreenDisplayStrategy.ShowAsync(CancellationToken cancellationToken)
        {
            _content.SetActive(true);

            _target.localScale = _disappearValues[^1];

            await VisualizeAnimationAsync(_appearValues, 1, _appearanceDuration, cancellationToken);
        }

        async UniTask IScreenDisplayStrategy.HideAsync(CancellationToken cancellationToken)
        {
            await VisualizeAnimationAsync(_disappearValues, 0, _appearanceDuration, cancellationToken);

            _content.SetActive(false);
        }

        void IScreenDisplayStrategy.Show()
        {
            _content.SetActive(true);

            _target.localScale = _appearValues[^1];
        }

        void IScreenDisplayStrategy.Hide()
        {
            _content.SetActive(false);

            _target.localScale = _disappearValues[^1];
        }

        private async UniTask VisualizeAnimationAsync(Vector3[] scales,
            float fadeValue,
            float duration,
            CancellationToken cancellationToken)
        {
            var fadeTask = _fadeLayout.DOFade(fadeValue, duration / 2)
                .SetLink(_fadeLayout.gameObject)
                .AwaitForComplete(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);

            var bounceTask = DoBounce(scales, duration, cancellationToken);

            await UniTask.WhenAll(fadeTask, bounceTask);
        }

        private async UniTask DoBounce(Vector3[] scales, float duration, CancellationToken cancellationToken)
        {
            var bouncesCount = scales.Length;
            var stepDuration = duration / bouncesCount;

            var scaleSequence = DOTween.Sequence();

#pragma warning disable 4014
            for (var i = 0; i < bouncesCount; i++)
            {
                scaleSequence.Append(_target.DOScale(scales[i], stepDuration));
            }
#pragma warning restore 4014

            await scaleSequence.SetLink(_target.gameObject)
                .AwaitForComplete(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
        }
    }
}
#endif