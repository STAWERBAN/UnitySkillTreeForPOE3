using UnityEngine;
using UnityEngine.EventSystems;

namespace SkillGraph.Views
{
    public class ScrollSkillTreeView : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Transform _content;
        [SerializeField] private Vector2 _limits;
        [SerializeField] private float _smooth;

        private Vector2 _currentPosition;


        private void Update()
        {
            _content.transform.position = 
                Vector2.Lerp(_content.transform.position, _currentPosition, _smooth * Time.deltaTime);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            var delta = eventData.delta / Screen.dpi;

            var x = Mathf.Clamp(_currentPosition.x + delta.x, -_limits.x, _limits.x);
            var y = Mathf.Clamp(_currentPosition.y + delta.y, -_limits.y, _limits.y);

            _currentPosition = new Vector2(x, y);
        }
    }
}