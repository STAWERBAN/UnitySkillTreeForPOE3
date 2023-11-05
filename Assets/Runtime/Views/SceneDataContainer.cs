using SkillGraph.Screens.Views;
using UnityEngine;

namespace SkillGraph.Views
{
    public class SceneDataContainer : MonoBehaviour
    {
        [SerializeField] private ScreenViewBase _overdraftBalanceScreen;
        [SerializeField] private ScreenViewBase _negativeBalanceScreen;
        [SerializeField] private ScreenViewBase _resetSkillWarningScreen;
        [SerializeField] private ScreenViewBase _baseSkillResetScreen;

        public ScreenViewBase OverdraftBalanceScreen => _overdraftBalanceScreen;
        public ScreenViewBase NegativeBalanceScreen => _negativeBalanceScreen;
        public ScreenViewBase ResetSkillWarningScreen => _resetSkillWarningScreen;
        public ScreenViewBase BaseSkillResetScreen => _baseSkillResetScreen;
    }
}