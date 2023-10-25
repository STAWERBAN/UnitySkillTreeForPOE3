using System;
using System.Collections.Generic;
using System.Linq;
using PathOfExile3.Runtime.Skills.Configs;
using PathOfExile3.Runtime.Skills.Points;

namespace PathOfExile3.Runtime.Models
{
    public class SkillSystemModel
    {
        private Dictionary<BaseSkillConfig, Skill> _skillPointsDictionary = new();

        public void SetSkillDictionary(Dictionary<BaseSkillConfig, Skill> dictionary)
        {
            _skillPointsDictionary = dictionary;
        }

        public Skill GetSkillPoint(BaseSkillConfig skillConfig)
        {
            try
            {
                return GetSkillFromDictionary(skillConfig);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public bool IsSkillActive(BaseSkillConfig skillConfig)
        {
            return GetSkillPoint(skillConfig).IsActive;
        }

        public bool CanToActivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = GetSkillPoint(skillConfig);
            var headerSkill = skill.GetHeaderSkill();
            return headerSkill.Any(IsSkillActive);
        }

        public bool CanToDeactivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = GetSkillPoint(skillConfig);
            var childSkills = skill.GetChildSkill();
            var headerSkill = skill.GetHeaderSkill();
            return headerSkill.Length > 0 && childSkills.Any(IsSkillActive);
        }

        private Skill GetSkillFromDictionary(BaseSkillConfig skillConfig)
        {
            var skillPoint = _skillPointsDictionary[skillConfig];

            if (skillPoint == null)
            {
                throw new ArgumentException("SkillPoint is not added", skillConfig.SkillName);
            }

            return skillPoint;
        }
    }
}