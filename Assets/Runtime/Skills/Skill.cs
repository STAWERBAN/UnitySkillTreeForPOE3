namespace PathOfExile3.Runtime.Skills
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

        public bool IsPersistent()
        {
            return _skillConfig.IsPersistent;
        }

        public bool TryActivate()
        {
            if (_isActive)
                return false;

            _isActive = true;

            return true;
        }

        public bool TryDeactivate()
        {
            if (!_isActive || IsPersistent())
                return false;

            _isActive = false;

            return true;
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

        public BaseSkillConfig[] GetNearestSkill()
        {
            return _skillConfig.NearestSkills;
        }
    }
}