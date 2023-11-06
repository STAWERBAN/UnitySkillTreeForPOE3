using UnityEngine;

namespace SkillGraph.Views
{
    [RequireComponent(typeof(LineRenderer)), ExecuteInEditMode]
    public class LineConnector : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private SkillWidgetView _linkedWidgetView;
        [SerializeField] private SkillWidgetView _parentSkillView;
        [SerializeField] private Color _activeStateColor = Color.green;
        [SerializeField] private Color _inactiveStateColor = Color.white;

        private LineRenderer _lineRenderer;

        private void OnValidate()
        {
            _lineRenderer ??= GetComponent<LineRenderer>();
        }

        private void Awake()
        {
            _linkedWidgetView.ChangeVisualizeState += OnLinkedWidgetChangeState;
            _parentSkillView.ChangeVisualizeState += OnParentChangeState;
        }

        private void OnDestroy()
        {
            _linkedWidgetView.ChangeVisualizeState -= OnLinkedWidgetChangeState;
            _parentSkillView.ChangeVisualizeState -= OnParentChangeState;
        }

        private void OnLinkedWidgetChangeState(bool state)
        {
            _lineRenderer.startColor = _lineRenderer.endColor =
                state && _parentSkillView.IsSkillActive ? _activeStateColor : _inactiveStateColor;
        }

        private void OnParentChangeState(bool state)
        {
            _lineRenderer.startColor = _lineRenderer.endColor =
                state && _linkedWidgetView.IsSkillActive ? _activeStateColor : _inactiveStateColor;
        }


#if UNITY_EDITOR
        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _target.position);
        }
#endif
    }
}