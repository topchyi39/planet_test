using System;

namespace Reactivity
{
    public class ReactProperty<T> : IReactProperty<T>
    {
        public T Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
        public event Action<T> ValueChanged;

        private T _value;
    }
}