using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace PathOfExile3.Runtime.View
{
    public class SkillSystemView : MonoBehaviour
    {
        [SerializeField] private SkillButtonView[] _skillButtonViews;
        [SerializeField] private Button _addSkillPointButton;
        [SerializeField] private Button _showSkillTreeButton;
        [SerializeField] private Button _resetAllTreeButton;
        [SerializeField] private TMP_Text _skillBalanceText;
        [SerializeField] private GameObject _skillTreeRoot;

        public ISkillButton[] SkillButtonViews => _skillButtonViews;
        public Button AddSkillPointButton => _addSkillPointButton;
        public Button ShowSkillTreeButton => _showSkillTreeButton;
        public Button ResetAllTreeButton => _resetAllTreeButton;
        public TMP_Text SkillBalanceText => _skillBalanceText;
        public GameObject SkillTreeRoot => _skillTreeRoot;
    }
}