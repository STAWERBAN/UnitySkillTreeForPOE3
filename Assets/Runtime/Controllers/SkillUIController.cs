using System;
using System.Collections.Generic;

using PathOfExile3.Runtime.View;
using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Skills;

namespace PathOfExile3.Runtime.Controllers
{
    public class SkillUIController : IDisposable
    {
        private SkillSystemView _skillSystemView;
        private SkillSystem _skillSystem;
        private SkillPanelView _skillPanelView;
        private SkillWallet _skillWallet;

        private BaseSkillConfig _currentSkillConfig;
        private Dictionary<BaseSkillConfig, ISkillButton> _skillButtonDictionary = new();

        public SkillUIController(SkillSystemView skillSystemView,
            SkillSystem skillSystem, SkillPanelView skillPanelView, SkillWallet skillWallet)
        {
            _skillSystemView = skillSystemView;
            _skillSystem = skillSystem;
            _skillPanelView = skillPanelView;
            _skillWallet = skillWallet;
        }

        public void Init()
        {
            foreach (var skillButtonView in _skillSystemView.SkillButtonViews)
            {
                skillButtonView.OnButtonClick += HandleSkillButtonClick;

                _skillButtonDictionary.Add(skillButtonView.SkillConfig, skillButtonView);

                if (_skillSystem.GetSkill(skillButtonView.SkillConfig).IsActive)
                {
                    skillButtonView.ActivateSkillStateVisualization();
                }
                else
                {
                    skillButtonView.DeactivateSkillStateVisualization();
                }
            }

            _skillPanelView.BuyButton.onClick.AddListener(HandleBuyButtonClick);
            _skillPanelView.ResetButton.onClick.AddListener(HandleResetClick);
            _skillPanelView.CloseButton.onClick.AddListener(CloseSkillDescriptionPanel);
            _skillSystemView.ShowSkillTreeButton.onClick.AddListener(ChangeShowSkillTreeState);
            _skillSystemView.AddSkillPointButton.onClick.AddListener(IncreaseBalance);
            _skillSystemView.ResetAllTreeButton.onClick.AddListener(_skillSystem.ResetAll);

            _skillWallet.BalanceChanged += OnBalanceChanged;
            _skillSystem.SkillStateChanged += OnSkillSettingChanged;

            _skillSystemView.SkillBalanceText.text = _skillWallet.Balance.ToString();
        }

        public void Dispose()
        {
            foreach (var skillButtonView in _skillSystemView.SkillButtonViews)
            {
                skillButtonView.OnButtonClick -= HandleSkillButtonClick;
            }

            _skillPanelView.BuyButton.onClick.RemoveListener(HandleBuyButtonClick);
            _skillPanelView.ResetButton.onClick.RemoveListener(HandleResetClick);
            _skillPanelView.CloseButton.onClick.RemoveListener(CloseSkillDescriptionPanel);
            _skillSystemView.ShowSkillTreeButton.onClick.RemoveListener(ChangeShowSkillTreeState);
            _skillSystemView.AddSkillPointButton.onClick.RemoveListener(IncreaseBalance);
            _skillSystemView.ResetAllTreeButton.onClick.RemoveListener(_skillSystem.ResetAll);

            _skillWallet.BalanceChanged -= OnBalanceChanged;
            _skillSystem.SkillStateChanged -= OnSkillSettingChanged;
        }

        private void ChangeShowSkillTreeState()
        {
            var skillTree = _skillSystemView.SkillTreeRoot;

            skillTree.SetActive(!skillTree.activeSelf);
        }

        private void OnBalanceChanged(int balance)
        {
            _skillSystemView.SkillBalanceText.text = balance.ToString();
        }

        private void HandleBuyButtonClick()
        {
            try
            {
                _skillSystem.ActivateSkill(_currentSkillConfig);

                CloseSkillDescriptionPanel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void HandleResetClick()
        {
            try
            {
                _skillSystem.DeactivateSkill(_currentSkillConfig);

                CloseSkillDescriptionPanel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void CloseSkillDescriptionPanel()
        {
            _currentSkillConfig = null;
            _skillPanelView.gameObject.SetActive(false);
        }

        private void HandleSkillButtonClick(BaseSkillConfig skillConfig, ISkillButton selectedButton)
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
            var canBeBought = _skillWallet.IsEnough(cost);
            var canBeActivated = _skillSystem.CanToActivateSkill(skill);

            _skillPanelView.BuyButton.interactable = canBeBought && canBeActivated;
        }

        private void SetResetButtonSettings(BaseSkillConfig skill)
        {
            var canDeactivate = _skillSystem.CanToDeactivateSkill(skill);

            _skillPanelView.ResetButton.interactable = canDeactivate;
        }

        private void IncreaseBalance()
        {
            try
            {
                _skillWallet.Put(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnSkillSettingChanged(BaseSkillConfig skillConfig, bool isActivated)
        {
            var button = _skillButtonDictionary[skillConfig];

            if (isActivated)
            {
                button.ActivateSkillStateVisualization();
            }
            else
            {
                button.DeactivateSkillStateVisualization();
            }
        }
    }
}