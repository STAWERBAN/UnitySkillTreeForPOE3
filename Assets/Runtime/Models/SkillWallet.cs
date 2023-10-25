using System;

namespace PathOfExile3.Runtime.Models
{
    public class SkillWallet
    {
        private int _balance;

        public SkillWallet(int balance)
        {
            _balance = balance;
        }

        public bool IsEnough(int cost)
        {
            return _balance > cost;
        }

        public void Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be negative", nameof(amount));
            }

            if (_balance < amount)
            {
                throw new ArgumentException("Not enough balance", nameof(amount));
            }

            _balance -= amount;
        }

        public void Put(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be negative", nameof(amount));
            }
            
            _balance += amount;
        }
    }
}