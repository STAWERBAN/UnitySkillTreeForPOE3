using System;
using System.Collections.Generic;

using PathOfExile3.Runtime.Models;

namespace PathOfExile3.Runtime.Skills
{
    public class SkillSystem
    {
        private readonly SkillConfig[] _skillConfigs;
        private readonly SkillSystemModel _skillSystemModel;

        public event Action<BaseSkillConfig, bool> SkillStateChanged = delegate { };

        public SkillSystem(SkillConfig[] skillConfigs)
        {
            _skillConfigs = skillConfigs;
            _skillSystemModel = new SkillSystemModel();
        }

        public void Init()
        {
            var skillPointsDictionary = new Dictionary<BaseSkillConfig, Skill>();

            for (var i = 0; i < _skillConfigs.Length; i++)
            {
                var config = _skillConfigs[i];
                var skillPoint = new Skill(config);

                skillPointsDictionary.Add(config, skillPoint);
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

            if (!skill.TryActivate()) return;

            SkillStateChanged.Invoke(skillConfig, true);
        }

        public void DeactivateSkill(BaseSkillConfig skillConfig)
        {
            var skill = _skillSystemModel.GetSkillPoint(skillConfig);

            if (!skill.TryDeactivate()) return;

            SkillStateChanged.Invoke(skillConfig, false);
        }

        public void ResetAll()
        {
            foreach (var skillConfig in _skillConfigs)
            {
                DeactivateSkill(skillConfig);
            }
        }
    }
}