using UnityEngine;

namespace PathOfExile3.Runtime.Skills.Configs
{
    public abstract class BaseSkillConfig : ScriptableObject
    {
        [SerializeField] private string skillName;

        public string SkillName => skillName;
    }
}