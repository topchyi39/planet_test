using UnityEngine;

namespace Player.Movement
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Player/Movement", order = 0)]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}