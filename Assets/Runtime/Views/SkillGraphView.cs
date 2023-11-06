using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkillGraph.Views
{
    public class SkillGraphView : MonoBehaviour
    {
        [SerializeField] private Button _clearButton;
        [SerializeField] private TextMeshProUGUI _skillPointBalance;

        public event Action Clear = delegate { };

        private void Awake()
        {
            _clearButton.onClick.AddListener(ResetToDefault);
        }

        private void OnDestroy()
        {
            _clearButton.onClick.RemoveListener(ResetToDefault);
        }

        public void SetBalance(int price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price value should be positive", nameof(price));
            }

            _skillPointBalance.text = price + " Sp";
        }

        private void ResetToDefault()
        {
            Clear.Invoke();
        }
    }
}