using PathOfExile3.Runtime.Controllers;
using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Skills;
using PathOfExile3.Runtime.Skills.Configs;
using PathOfExile3.Runtime.View;
using UnityEngine;

namespace PathOfExile3.Runtime
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private SkillConfig[] _skillConfigs;
        [SerializeField] private SkillSystemView _skillSystemView;
        [SerializeField] private SkillPanelView _skillPanelView;

        private SkillSystem _skillSystem;
        private SkillUIController _skillUIController;
        private SkillWallet _skillWallet;

        private void Awake()
        {
            _skillWallet = new SkillWallet(10);

            _skillSystem = new SkillSystem(_skillConfigs);
            _skillUIController = new SkillUIController(_skillSystemView, _skillSystem, _skillPanelView, _skillWallet);

            _skillSystem.Init();
            _skillUIController.Init();
        }
    }
}