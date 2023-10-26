using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace PathOfExile3.Runtime.Skills
{
    [CreateAssetMenu(menuName = "Skill/Configs", fileName = "SkillConfig")]
    public class SkillConfig : BaseSkillConfig
    {
        [SerializeField] private int _cost;
        [SerializeField] private string _description;
        [SerializeField] private List<BaseSkillConfig> _nearestSkills = new();
        [SerializeField] private bool isPersistent;

        public int Cost => _cost;
        public string Description => _description;
        public BaseSkillConfig[] NearestSkills => _nearestSkills.ToArray();
        public bool IsPersistent => isPersistent;

        #region EditorTools

#if UNITY_EDITOR

        private BaseSkillConfig[] _nearestSkillsCashed = { };

        public void OnValidate()
        {
            var removedParent = _nearestSkillsCashed.Except(_nearestSkills);
            var addedParent = _nearestSkills.Except(_nearestSkillsCashed);

            foreach (var skillConfig in removedParent)
            {
                ((SkillConfig)skillConfig).RemoveNearestSkill(this);
            }

            foreach (var skillConfig in addedParent)
            {
                ((SkillConfig)skillConfig).AddNearestSkill(this);
            }

            _nearestSkillsCashed = NearestSkills;
        }

        public void AddNearestSkill(BaseSkillConfig baseSkillConfig)
        {
            if (!_nearestSkills.Contains(baseSkillConfig))
                _nearestSkills.Add(baseSkillConfig);
            else
                Debug.LogWarning($"{baseSkillConfig.SkillName} is already added");
        }

        public void RemoveNearestSkill(BaseSkillConfig baseSkillConfig)
        {
            if (!_nearestSkills.Remove(baseSkillConfig))
                Debug.Log("Something went wrong");
        }
#endif

        #endregion
    }
}