using SkillGraph.Screens.Visualization;

namespace SkillGraph.Screens.Views
{
    internal class DefaultScreenView : ScreenViewBase
    {
        protected override IScreenDisplayStrategy GetStrategy()
        {
            return new DefaultAppearance();
        }
    }
}