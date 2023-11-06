using SkillGraph.Screens.Views;
using SkillGraph.Screens.Visualization;
using TMPro;
using UnityEngine;

namespace SkillGraph.Views
{
    public class WarningScreenView : ScreenViewBase
    {
        [SerializeField] private TextMeshProUGUI _messageText;

        protected override IScreenDisplayStrategy GetStrategy()
        {
            return new DefaultAppearance();
        }

        public void SetMessage(string message)
        {
            _messageText.text = message;
        }
    }
}