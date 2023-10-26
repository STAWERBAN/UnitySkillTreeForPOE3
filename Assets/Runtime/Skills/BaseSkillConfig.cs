using UnityEngine;

namespace PathOfExile3.Runtime.Skills.Configs
{
    public abstract class BaseSkillConfig : ScriptableObject
    {
        [SerializeField] private string skillName;
        [SerializeField] private int _index;

        public string SkillName => skillName;
        public int Index => _index;
    }
}