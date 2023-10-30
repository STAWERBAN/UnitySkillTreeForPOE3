using System;

namespace PathOfExile3.Runtime.Models
{
    public class SkillWallet
    {
        public event Action<int> BalanceChanged = delegate { };

        private int _balance;

        public int Balance
        {
            get => _balance;
            private set
            {
                _balance = value;

                BalanceChanged.Invoke(Balance);
            }
        }

        public SkillWallet(int balance)
        {
            Balance = balance;
        }

        public bool IsEnough(int cost)
        {
            return Balance >= cost;
        }

        public void Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be negative", nameof(amount));
            }

            if (Balance < amount)
            {
                throw new ArgumentException("Not enough balance", nameof(amount));
            }

            Balance -= amount;
        }

        public void Put(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount can not be negative", nameof(amount));
            }

            Balance += amount;
        }
    }
}