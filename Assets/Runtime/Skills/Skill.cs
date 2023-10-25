using PathOfExile3.Runtime.Skills.Configs;

namespace PathOfExile3.Runtime.Skills.Points
{
    public class Skill
    {
        private readonly SkillConfig _skillConfig;

        private bool _isActive;

        public bool IsActive => _isActive;

        public Skill(SkillConfig skillConfig)
        {
            _skillConfig = skillConfig;
            _isActive = skillConfig.IsPersistent;
        }

        public void Activate()
        {
            _isActive = true;
        }

        public bool IsPersistent()
        {
            return _skillConfig.IsPersistent;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        public int GetCost()
        {
            return _skillConfig.Cost;
        }

        public string GetDescription()
        {
            return _skillConfig.Description;
        }

        public string GetSkillName()
        {
            return _skillConfig.SkillName;
        }

        public BaseSkillConfig[] GetHeaderSkill()
        {
            return _skillConfig.HeaderSkills;
        }

        public BaseSkillConfig[] GetChildSkill()
        {
            return _skillConfig.ChildSkills;
        }
    }
}