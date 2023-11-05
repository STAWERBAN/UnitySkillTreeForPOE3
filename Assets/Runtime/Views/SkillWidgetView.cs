using System;
using Cysharp.Threading.Tasks;
using SkillGraph.SkillSystem.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkillGraph.Views
{
    public class SkillWidgetView : MonoBehaviour, ISkillWidgetView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private Color _activeStateColor = Color.green;
        [SerializeField] private Color _inactiveStateColor = Color.white;

        public event Action Clicked;
        public event Action PointerEnter;
        public event Action PointerExit;

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExit?.Invoke();
        }

        public async UniTask VisualizeStateAsync(bool state)
        {
            // todo: implement async dotween visualization or maybe not
            _icon.color = state ? Color.green : Color.white;
        }
    }
}