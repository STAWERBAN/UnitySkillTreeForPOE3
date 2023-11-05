using SkillGraph.SkillSystem.Models;
using SkillGraph.SkillSystem.Views;

namespace SkillGraph.SkillSystem.ViewModels
{
    internal sealed class SkillViewModel
    {
        internal ISkillWidgetView Widget { get; private set; }

        internal Skill Skill { get; private set; }

        internal SkillViewModel(Skill skill, ISkillWidgetView view)
        {
            Widget = view;
            Skill = skill;
        }
    }
}