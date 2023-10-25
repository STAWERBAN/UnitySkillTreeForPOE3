using System;
using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Skills;
using PathOfExile3.Runtime.Skills.Configs;
using PathOfExile3.Runtime.Skills.Points;
using PathOfExile3.Runtime.View;

namespace PathOfExile3.Runtime.Controllers
{
    public class SkillUIController : IDisposable
    {
        private SkillSystemView _skillSystemView;
        private SkillSystem _skillSystem;
        private SkillPanelView _skillPanelView;
        private SkillWallet _systemWallet;

        private BaseSkillConfig _currentSkillConfig;

        public SkillUIController(SkillSystemView skillSystemView,
            SkillSystem skillSystem, SkillPanelView skillPanelView, SkillWallet systemWallet)
        {
            _skillSystemView = skillSystemView;
            _skillSystem = skillSystem;
            _skillPanelView = skillPanelView;
            _systemWallet = systemWallet;
        }

        public void Dispose()
        {
            foreach (var skillButtonView in _skillSystemView.SkillButtonViews)
            {
                skillButtonView.OnButtonClick -= HandleSkillButtonClick;
            }

            _skillPanelView.BuyButton.onClick.RemoveListener(HandleBuyButtonClick);
            _skillPanelView.ResetButton.onClick.RemoveListener(HandleBuyResetClick);
            _skillPanelView.CloseButton.onClick.RemoveListener(CloseSkillDescriptionPanel);
        }

        public void Init()
        {
            foreach (var skillButtonView in _skillSystemView.SkillButtonViews)
            {
                skillButtonView.OnButtonClick += HandleSkillButtonClick;
            }

            _skillPanelView.BuyButton.onClick.AddListener(HandleBuyButtonClick);
            _skillPanelView.ResetButton.onClick.AddListener(HandleBuyResetClick);
            _skillPanelView.CloseButton.onClick.AddListener(CloseSkillDescriptionPanel);
        }

        private void HandleBuyButtonClick()
        {
            _systemWallet.Withdraw(_skillSystem.GetSkill(_currentSkillConfig).GetCost());
            _skillSystem.DeactivateSkill(_currentSkillConfig);

            CloseSkillDescriptionPanel();
        }

        private void HandleBuyResetClick()
        {
            _systemWallet.Put(_skillSystem.GetSkill(_currentSkillConfig).GetCost());
            _skillSystem.ActivateSkill(_currentSkillConfig);

            CloseSkillDescriptionPanel();
        }

        private void CloseSkillDescriptionPanel()
        {
            _currentSkillConfig = null;
            _skillPanelView.gameObject.SetActive(false);
        }

        private void HandleSkillButtonClick(BaseSkillConfig skillConfig)
        {
            _currentSkillConfig = skillConfig;

            _skillPanelView.gameObject.SetActive(true);

            var skill = _skillSystem.GetSkill(skillConfig);
            var cost = skill.GetCost();

            _skillPanelView.CostText.text = cost + "Sp";
            _skillPanelView.DescriptionText.text = skill.GetDescription();
            _skillPanelView.HeaderText.text = skill.GetSkillName();

            var isSkillBought = skill.IsActive;

            _skillPanelView.BuyButton.gameObject.SetActive(!isSkillBought);
            _skillPanelView.ResetButton.gameObject.SetActive(isSkillBought);

            if (isSkillBought)
                SetResetButtonSettings(skillConfig);
            else
                SetBuyButtonSettings(skillConfig, cost);
        }

        private void SetBuyButtonSettings(BaseSkillConfig skill, int cost)
        {
            var canBeBought = _systemWallet.IsEnough(cost);
            var canBeActivated = _skillSystem.CanToActivateSkill(skill);

            _skillPanelView.BuyButton.interactable = canBeBought && canBeActivated;
        }

        private void SetResetButtonSettings(BaseSkillConfig skill)
        {
            var canDeactivate = _skillSystem.CanToDeactivateSkill(skill);

            _skillPanelView.ResetButton.interactable = canDeactivate;
        }
    }
}