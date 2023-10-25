using System;

namespace PathOfExile3.Runtime.Models
{
    public class SkillWallet
    {
        public event Action<int> BalanceChanged = delegate { };

        private int _balance;

        public int Balance => _balance;

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

            BalanceChanged.Invoke(_balance);
        }

        public void Put(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be negative", nameof(amount));
            }

            _balance += amount;

            BalanceChanged.Invoke(_balance);
        }
    }
}