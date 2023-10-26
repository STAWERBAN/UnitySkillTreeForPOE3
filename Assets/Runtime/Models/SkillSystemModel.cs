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

        private List<Skill> _cashedCheckedList = new();

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
            var headerSkills = skill.GetNearestSkill();
            return headerSkills.Any(IsSkillActive);
        }

        public bool CanToDeactivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = GetSkillPoint(skillConfig);

            _cashedCheckedList.Clear();

            if (skill.IsPersistent())
                return false;

            var childConfigs = skill.GetNearestSkill();

            foreach (var childConfig in childConfigs)
            {
                var childSkill = GetSkillPoint(childConfig);

                _cashedCheckedList.Clear();
                _cashedCheckedList.Add(skill);

                if (!childSkill.IsActive)
                    continue;

                if (!InConnectedWithHead(childSkill))
                    return false;
            }


            return true;
        }

        private bool InConnectedWithHead(Skill checkedSkill)
        {
            if (checkedSkill.IsPersistent())
                return true;

            var childSkillConfigs = checkedSkill.GetNearestSkill();

            foreach (var childSkillConfig in childSkillConfigs)
            {
                var childSkill = GetSkillPoint(childSkillConfig);

                if (_cashedCheckedList.Contains(childSkill))
                    continue;

                _cashedCheckedList.Add(childSkill);

                if (!childSkill.IsActive)
                    continue;

                if (InConnectedWithHead(childSkill))
                    return true;
            }

            return false;
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