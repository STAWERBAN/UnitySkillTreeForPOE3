using System;
using SkillGraph.SkillSystem.Exceptions;
using SkillGraph.SkillSystem.Utilities;

namespace SkillGraph.Models
{
    public class Wallet
    {
        public event Action<int> BalanceChanged;

        public int Balance
        {
            get => _balance;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value must be positive", nameof(value));
                }

                _balance = value;

                BalanceChanged?.Invoke(Balance);
            }
        }

        private int _balance;

        public void Put(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            if (Balance < amount)
            {
                throw new OverdraftException();
            }

            Balance -= amount;
        }
    }
}