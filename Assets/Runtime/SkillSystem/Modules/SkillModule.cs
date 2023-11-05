using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SkillGraph.SkillSystem.Data;
using SkillGraph.SkillSystem.Models;
using SkillGraph.SkillSystem.ViewModels;
using SkillGraph.SkillSystem.Views;

namespace SkillGraph.SkillSystem.Modules
{
    public class SkillModule
    {
        private readonly SkillViewModel[] _skillViewModels;

        public Skill Current { get; set; }

        public SkillModule(IEnumerable<ISkillDataContainer> skillViewModels)
        {
            var skillViewModelsArray = skillViewModels.ToArray();
            
            _skillViewModels = skillViewModelsArray.Select(container =>
                    new SkillViewModel(Skill.Create(container.SkillData), container.Widget))
                .ToArray();

            foreach (var skillData in skillViewModelsArray.Select(a => a.SkillData))
            {
                var skill = GetSkill(skillData);

                if (skill == null)
                    continue;

                skill.AdjacentSkills = skillData.AdjacentSkillData.Select(GetSkill).ToArray();
            }
        }

        [CanBeNull]
        public Skill GetSkill(ISkillWidgetView widget)
        {
            return _skillViewModels.FirstOrDefault(vM => vM.Widget == widget)?.Skill;
        }

        [CanBeNull]
        public Skill GetSkill(SkillData skillData)
        {
            return _skillViewModels.FirstOrDefault(vM => vM.Skill.Equals(skillData))?.Skill;
        }
    }
}