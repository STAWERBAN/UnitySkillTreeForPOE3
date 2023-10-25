using UnityEngine;

namespace PathOfExile3.Runtime.View
{
    public class SkillSystemView : MonoBehaviour
    {
        [SerializeField] private SkillButtonView[] _skillButtonViews;

        public ISkillButton[] SkillButtonViews => _skillButtonViews;
    }
}