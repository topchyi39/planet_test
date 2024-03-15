using System;

namespace Reactivity
{
    public interface IReactProperty<T>
    {
        T Value { get; }
        event Action<T> ValueChanged;
    }
}