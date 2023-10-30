using UnityEngine;

using PathOfExile3.Runtime.View;
using PathOfExile3.Runtime.Skills;
using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Controllers;

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
        private SkillWalletController _skillWalletController;

        private void Awake()
        {
            _skillWallet = new SkillWallet(0);

            _skillSystem = new SkillSystem(_skillConfigs);
            _skillUIController = new SkillUIController(_skillSystemView, _skillSystem, _skillPanelView, _skillWallet);
            _skillWalletController = new SkillWalletController(_skillSystem, _skillWallet);

            _skillSystem.Init();
            _skillUIController.Init();
            _skillWalletController.Init();
        }

        private void OnDestroy()
        {
            _skillUIController.Dispose();
            _skillWalletController.Dispose();
        }
    }
}