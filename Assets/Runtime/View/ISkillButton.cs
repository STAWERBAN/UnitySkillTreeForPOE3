using System;
using PathOfExile3.Runtime.Skills;
using PathOfExile3.Runtime.Skills.Configs;

namespace PathOfExile3.Runtime.View
{
    public interface ISkillButton
    {
        public event Action<BaseSkillConfig> OnButtonClick;
    }
}