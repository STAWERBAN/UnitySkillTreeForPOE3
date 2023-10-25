using System;
using PathOfExile3.Runtime.Skills.Configs;
using PathOfExile3.Runtime.Skills.Points;
using UnityEngine;
using UnityEngine.UI;

namespace PathOfExile3.Runtime.View
{
    public class SkillButtonView : MonoBehaviour, ISkillButton
    {
        [SerializeField] private Button _buttonComponent;
        [SerializeField] private BaseSkillConfig _skillConfig;
        [SerializeField] private Image _buttonImage;

        public BaseSkillConfig SkillConfig => _skillConfig;
        public event Action<BaseSkillConfig, ISkillButton> OnButtonClick = delegate { };

        private Skill _skill;

        private void OnEnable()
        {
            _buttonComponent.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _buttonComponent.onClick.RemoveListener(ButtonClicked);
        }

        public void ActivateSkillStateVisualization()
        {
            _buttonImage.color = Color.green;
        }

        public void DeactivateSkillStateVisualization()
        {
            _buttonImage.color = Color.white;
        }

        private void ButtonClicked()
        {
            OnButtonClick.Invoke(_skillConfig, this);
        }
    }
}