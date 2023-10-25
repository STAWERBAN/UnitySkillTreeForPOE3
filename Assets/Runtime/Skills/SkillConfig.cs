using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace PathOfExile3.Runtime.Skills.Configs
{
    [CreateAssetMenu(menuName = "Skill/Configs", fileName = "SkillConfig")]
    public class SkillConfig : BaseSkillConfig
    {
        [SerializeField] private int _cost;
        [SerializeField] private string _description;
        [SerializeField] private List<BaseSkillConfig> _headerSkills;
        [SerializeField] private List<BaseSkillConfig> _childSkills;
        [FormerlySerializedAs("_isPersistant")] [SerializeField] private bool isPersistent;

        public int Cost => _cost;
        public string Description => _description;
        public BaseSkillConfig[] ChildSkills => _childSkills.ToArray();
        public BaseSkillConfig[] HeaderSkills => _headerSkills.ToArray();
        public bool IsPersistent => isPersistent;

        #region EditorTools

#if UNITY_EDITOR

        private BaseSkillConfig[] _headerSkillsCached;
        private BaseSkillConfig[] _childSkillsCached;

        public void OnValidate()
        {
            var removedChild = _childSkillsCached.Except(_childSkills);
            var addedChild = _childSkills.Except(_childSkillsCached);

            var removedParent = _headerSkillsCached.Except(_headerSkills);
            var addedParent = _headerSkills.Except(_headerSkillsCached);

            foreach (var skillConfig in removedChild)
            {
                ((SkillConfig)skillConfig).RemoveFromParent(this);
            }

            foreach (var skillConfig in addedChild)
            {
                ((SkillConfig)skillConfig).AddToParent(this);
            }

            foreach (var skillConfig in removedParent)
            {
                ((SkillConfig)skillConfig).RemoveFromChild(this);
            }

            foreach (var skillConfig in addedParent)
            {
                ((SkillConfig)skillConfig).AddToChild(this);
            }

            _headerSkillsCached = _headerSkills.ToArray();
            _childSkillsCached = _childSkills.ToArray();
        }

        public void AddToChild(BaseSkillConfig baseSkillConfig)
        {
            if (!_childSkills.Contains(baseSkillConfig))
                _childSkills.Add(baseSkillConfig);
            else
                Debug.LogWarning($"{baseSkillConfig.SkillName} is already added");
        }

        public void RemoveFromChild(BaseSkillConfig baseSkillConfig)
        {
            if (!_childSkills.Remove(baseSkillConfig))
                Debug.Log("Something went wrong");
        }

        public void AddToParent(BaseSkillConfig baseSkillConfig)
        {
            if (!_headerSkills.Contains(baseSkillConfig))
                _headerSkills.Add(baseSkillConfig);
            else
                Debug.LogWarning($"{baseSkillConfig.SkillName} is already added");
        }

        public void RemoveFromParent(BaseSkillConfig baseSkillConfig)
        {
            if (!_headerSkills.Remove(baseSkillConfig))
                Debug.Log("Something went wrong");
        }
#endif

        #endregion
    }
}