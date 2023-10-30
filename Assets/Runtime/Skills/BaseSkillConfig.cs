using UnityEngine;

namespace PathOfExile3.Runtime.Skills
{
    public abstract class BaseSkillConfig : ScriptableObject
    {
        [SerializeField] private string _skillName;

        public string SkillName => _skillName;
    }
}