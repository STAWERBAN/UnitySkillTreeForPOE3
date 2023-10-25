﻿using System.Collections.Generic;
using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Skills.Configs;
using PathOfExile3.Runtime.Skills.Points;

namespace PathOfExile3.Runtime.Skills
{
    public class SkillSystem
    {
        private readonly SkillConfig[] _skillConfigs;
        private readonly SkillSystemModel _skillSystemModel;

        public SkillSystem(SkillConfig[] skillConfigs)
        {
            _skillConfigs = skillConfigs;
            _skillSystemModel = new SkillSystemModel();
        }

        public void Init()
        {
            var skillPointsDictionary = new Dictionary<BaseSkillConfig, Skill>();

            foreach (var skillConfig in _skillConfigs)
            {
                var skillPoint = new Skill(skillConfig);

                skillPointsDictionary.Add(skillConfig, skillPoint);
            }

            _skillSystemModel.SetSkillDictionary(skillPointsDictionary);
        }

        public Skill GetSkill(BaseSkillConfig skillConfig)
        {
            return _skillSystemModel.GetSkillPoint(skillConfig);
        }

        public bool CanToActivateSkill(BaseSkillConfig skillConfig)
        {
            return _skillSystemModel.CanToActivateSkill(skillConfig);
        }

        public bool CanToDeactivateSkill(BaseSkillConfig skillConfig)
        {
            return _skillSystemModel.CanToDeactivateSkill(skillConfig);
        }

        public void ActivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = _skillSystemModel.GetSkillPoint(skillConfig);

            skill.Activate();
        }

        public void DeactivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = _skillSystemModel.GetSkillPoint(skillConfig);

            skill.Deactivate();
        }
    }
}