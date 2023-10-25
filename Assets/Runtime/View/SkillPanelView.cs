using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace PathOfExile3.Runtime.View
{
    public class SkillPanelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _costText;
        [FormerlySerializedAs("_exitButton")] [SerializeField] private Button closeButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _resetButton;

        public TMP_Text HeaderText => _headerText;
        public TMP_Text DescriptionText => _descriptionText;
        public TMP_Text CostText => _costText;
        public Button CloseButton => closeButton;
        public Button BuyButton => _buyButton;
        public Button ResetButton => _resetButton;
    }
}