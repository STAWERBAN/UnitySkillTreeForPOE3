using System;

using PathOfExile3.Runtime.Skills;

namespace PathOfExile3.Runtime.View
{
    public interface ISkillButton
    {
        public BaseSkillConfig SkillConfig { get; }
        public event Action<BaseSkillConfig, ISkillButton> OnButtonClick;
        public void ActivateSkillStateVisualization();
        public void DeactivateSkillStateVisualization();
    }
}