using UnityEngine;

namespace SkillGraph.Views
{
    [RequireComponent(typeof(LineRenderer)), ExecuteInEditMode]
    public class LineConnector : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private Transform _target;

        private LineRenderer _lineRenderer;

        private void OnValidate()
        {
            _lineRenderer ??= GetComponent<LineRenderer>();
        }

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