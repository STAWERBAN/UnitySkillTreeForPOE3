using System;
using SkillGraph.Views;
using SkillGraph.SkillSystem.Data;
using SkillGraph.SkillSystem.Views;
using SkillGraph.SkillSystem.Models;
using UnityEngine;

namespace SkillGraph.Models
{
    [Serializable]
    public class SkillDataContainer : ISkillDataContainer
    {
        [SerializeField] private SkillWidgetView _widget;
        [SerializeField] private SkillData _skillData;

        ISkillWidgetView ISkillDataContainer.Widget => _widget;
        SkillData ISkillDataContainer.SkillData => _skillData;

        public SkillWidgetView GetWidget()
        {
            return _widget;
        }
    }
}