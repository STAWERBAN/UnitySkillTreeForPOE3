using System;
using SkillGraph.SkillSystem.Data;
using SkillGraph.SkillSystem.Models;
using SkillGraph.SkillSystem.Views;
using SkillGraph.Views;
using UnityEngine;

namespace SkillGraph.Models
{
    [Serializable]
    public class SkillDataContainer : ISkillDataContainer
    {
        ISkillWidgetView ISkillDataContainer.Widget => _widget;

        SkillData ISkillDataContainer.SkillData => _skillData;

        [SerializeField]
        private SkillWidgetView _widget;

        [SerializeField]
        private SkillData _skillData;

        public SkillWidgetView GetWidget()
        {
            return _widget;
        }
    }
}