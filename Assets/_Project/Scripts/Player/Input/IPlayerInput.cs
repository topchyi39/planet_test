using Reactivity;
using UnityEngine;

namespace Player.Input
{
    public interface IPlayerInput
    {
        Vector2 MoveAxis { get; }
        IReactProperty<bool> Active { get; }
    }
}