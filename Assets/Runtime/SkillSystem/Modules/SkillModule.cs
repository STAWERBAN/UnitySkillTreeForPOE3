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
                
                var adjacentSkills = new Skill[skillData.AdjacentSkillData.Length];

                for (var i = 0; i < skillData.AdjacentSkillData.Length; i++)
                {
                    adjacentSkills[i] = GetSkill(skillData.AdjacentSkillData[i]);
                }

                skill.AdjacentSkills = adjacentSkills;
            }
        }

        [CanBeNull]
        public Skill GetSkill(ISkillWidgetView widget)
        {
            return _skillViewModels.FirstOrDefault(viewModel => viewModel.Widget == widget)?.Skill;
        }

        [CanBeNull]
        public Skill GetSkill(SkillData skillData)
        {
            return _skillViewModels.FirstOrDefault(viewModel => viewModel.Skill.Equals(skillData))?.Skill;
        }

        public IEnumerable<Skill> GetAllSkill()
        {
            return _skillViewModels.Select(a => a.Skill);
        }
    }
}