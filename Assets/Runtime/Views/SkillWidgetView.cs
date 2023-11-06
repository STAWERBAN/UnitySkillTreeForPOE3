using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using SkillGraph.SkillSystem.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkillGraph.Views
{
    public class SkillWidgetView : MonoBehaviour, IDisposable, ISkillWidgetView, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private Color _activeStateColor = Color.green;
        [SerializeField] private Color _inactiveStateColor = Color.white;
        [SerializeField] private float _colorSwitchDuration = 0.5f;

        public event Action Clicked;
        public event Action<bool> ChangeVisualizeState;

        public bool IsSkillActive
        {
            get => _isSkillActive;
            private set
            {
                ChangeVisualizeState?.Invoke(value);
                
                _isSkillActive = value;
            }
        }

        private bool _isSkillActive;
        private Tween _tween;

        public void Dispose()
        {
            Clicked = null;
            ChangeVisualizeState = null;
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public async UniTask VisualizeStateAsync(bool state)
        {
            _tween?.Kill();
            _tween = _icon.DOColor(state ? _activeStateColor : _inactiveStateColor, _colorSwitchDuration);

            IsSkillActive = state;

            await _tween.ToUniTask();
        }
    }
}