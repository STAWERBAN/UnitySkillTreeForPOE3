using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace SkillGraph.SkillSystem.Data
{
    [CreateAssetMenu(menuName = "Skill/Configs", fileName = "SkillConfig")]
    public class SkillData : ScriptableObject
    {
        public string Identifier => _identifier;

        internal string Name => _name;

        internal int Price => _price;

        internal string Description => _description;

        internal SkillData[] AdjacentSkillData => _adjacentSkillData.ToArray();

        internal bool Persistent => _persistent;

        [SerializeField, HideInInspector] private string _identifier;
        [SerializeField] private string _name;
        [SerializeField] private int _price;
        [SerializeField] private string _description;
        [SerializeField] private List<SkillData> _adjacentSkillData;
        [SerializeField] private bool _persistent;

#if UNITY_EDITOR
        private SkillData[] _adjacentSkillsCache = Array.Empty<SkillData>();
#endif

        [Conditional("UNITY_EDITOR")]
        private void OnValidate()
        {
            _adjacentSkillData ??= new List<SkillData>();

            _identifier ??= Guid.NewGuid().ToString();

            var removedParent = _adjacentSkillsCache.Except(_adjacentSkillData);
            var addedParent = _adjacentSkillData.Except(_adjacentSkillsCache);

            foreach (var skillConfig in removedParent)
            {
                skillConfig.RemoveNearestSkill(this);
            }

            foreach (var skillConfig in addedParent)
            {
                skillConfig.AddNearestSkill(this);
            }

            _adjacentSkillsCache = _adjacentSkillData.ToArray();
        }

        [Conditional("UNITY_EDITOR")]
        private void AddNearestSkill(SkillData baseSkillConfig)
        {
            if (_adjacentSkillData.Contains(baseSkillConfig))
            {
                UnityEngine.Debug.LogWarning("Skill reference already exists");
                return;
            }

            _adjacentSkillData.Add(baseSkillConfig);
        }

        [Conditional("UNITY_EDITOR")]
        private void RemoveNearestSkill(SkillData baseSkillConfig)
        {
            _adjacentSkillData.Remove(baseSkillConfig);
        }
    }
}