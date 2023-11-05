using SkillGraph.SkillSystem.Data;
using SkillGraph.SkillSystem.Views;

namespace SkillGraph.SkillSystem.Models
{
    public interface ISkillDataContainer
    {
        public ISkillWidgetView Widget { get; }

        public SkillData SkillData { get; }
    }
}