using System;

namespace SkillGraph.SkillSystem.Utilities
{
    public class ObservableProperty<T> : IDisposable
    {
        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                Changed?.Invoke(_value);
            }
        }

        private event Action<T> Changed;

        private T _value;

        public void Dispose()
        {
            Changed = null;
        }

        public static ObservableProperty<T> operator +(ObservableProperty<T> prop, Action<T> subscription)
        {
            prop.Subscribe(subscription);

            return prop;
        }

        public static ObservableProperty<T> operator -(ObservableProperty<T> prop, Action<T> subscription)
        {
            prop.Unsubscribe(subscription);

            return prop;
        }

        public static implicit operator T(ObservableProperty<T> prop)
        {
            return prop.Value;
        }

        private void Subscribe(Action<T> subscription)
        {
            Changed += subscription;
        }

        private void Unsubscribe(Action<T> subscription)
        {
            Changed -= subscription;
        }
    }
}