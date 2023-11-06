using System;
using SkillGraph.Screens.Views;
using SkillGraph.Screens.Visualization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkillGraph.Views
{
    public class SkillScreenView : ScreenViewBase
    {
        public event Action ResetClicked;
        public event Action PurchaseClicked;

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _resetButton;


        private void Awake()
        {
            _purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
            _resetButton.onClick.AddListener(OnResetButtonClicked);
        }

        private void OnDestroy()
        {
            _purchaseButton.onClick.RemoveListener(OnPurchaseButtonClicked);
            _resetButton.onClick.RemoveListener(OnResetButtonClicked);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public void SetPrice(int price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price value should be positive", nameof(price));
            }

            _price.text = price + " Sp";
        }

        public void SetPurchaseState(bool active)
        {
            _purchaseButton.gameObject.SetActive(!active);
            _resetButton.gameObject.SetActive(active);
        }

        private void OnPurchaseButtonClicked()
        {
            PurchaseClicked?.Invoke();
        }

        private void OnResetButtonClicked()
        {
            ResetClicked?.Invoke();
        }

        protected override IScreenDisplayStrategy GetStrategy()
        {
            return new BounceAppearance();
        }
    }
}