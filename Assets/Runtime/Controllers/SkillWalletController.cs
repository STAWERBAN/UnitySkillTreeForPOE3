﻿using System;

using PathOfExile3.Runtime.Models;
using PathOfExile3.Runtime.Skills;

namespace PathOfExile3.Runtime.Controllers
{
    public class SkillWalletController : IDisposable
    {
        private readonly SkillSystem _skillSystem;
        private readonly SkillWallet _wallet;

        public SkillWalletController(SkillSystem skillSystem, SkillWallet wallet)
        {
            _skillSystem = skillSystem;
            _wallet = wallet;
        }

        public void Init()
        {
            _skillSystem.SkillStateChanged += HandleSkillChangeState;
        }

        public void Dispose()
        {
            _skillSystem.SkillStateChanged -= HandleSkillChangeState;
        }

        private void HandleSkillChangeState(BaseSkillConfig skillConfig, bool isActivated)
        {
            if (isActivated)
            {
                _wallet.Withdraw(_skillSystem.GetSkill(skillConfig).GetCost());
            }
            else
            {
                _wallet.Put(_skillSystem.GetSkill(skillConfig).GetCost());
            }
        }
    }
}