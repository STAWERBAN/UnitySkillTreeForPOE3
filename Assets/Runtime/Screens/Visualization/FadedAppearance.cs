#if UNITASK_DOTWEEN_SUPPORT
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SkillGraph.Screens.Visualization
{
    [Serializable]
    public class FadedAppearance : IScreenDisplayStrategy
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _appearanceDuration = 0.5f;

        void IScreenDisplayStrategy.Validate(GameObject root)
        {
            if (_content == null)
            {
                _content = root.transform.childCount > 0
                    ? root.transform.GetChild(0).gameObject
                    : root;
            }

            if (_canvasGroup == null)
            {
                _canvasGroup = root.GetComponent<CanvasGroup>();

                if (_canvasGroup == null)
                {
                    _canvasGroup = root.AddComponent<CanvasGroup>();
                }
            }
        }

        async UniTask IScreenDisplayStrategy.ShowAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.alpha = 0;

            _content.SetActive(true);

            await _canvasGroup.DOFade(1, _appearanceDuration)
                .SetLink(_canvasGroup.gameObject)
                .AwaitForComplete(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
        }

        async UniTask IScreenDisplayStrategy.HideAsync(CancellationToken cancellationToken)
        {
            await _canvasGroup.DOFade(0, _appearanceDuration)
                .SetLink(_canvasGroup.gameObject)
                .AwaitForComplete(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);

            _content.SetActive(false);
        }

        void IScreenDisplayStrategy.Show()
        {
            _content.SetActive(true);

            _canvasGroup.alpha = 1;
        }

        void IScreenDisplayStrategy.Hide()
        {
            _content.SetActive(false);

            _canvasGroup.alpha = 0;
        }
    }
}
#endif