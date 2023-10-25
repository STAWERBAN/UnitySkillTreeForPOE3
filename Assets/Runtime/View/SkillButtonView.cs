using System;

using PathOfExile3.Runtime.Skills;
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

        public event Action<BaseSkillConfig> OnButtonClick = delegate { };

        private Skill _skill;

        private void OnEnable()
        {
            _buttonComponent.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _buttonComponent.onClick.RemoveListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            OnButtonClick.Invoke(_skillConfig);
        }
    }
}