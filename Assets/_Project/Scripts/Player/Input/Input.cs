using Game.Levels;
using Reactivity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.Input
{
    public class Input : MonoBehaviour, IPlayerInput, ILevelListener
    {
        public Vector2 MoveAxis { get; private set; }
        public IReactProperty<bool> Active => _active;

        private PlayerControls _controls;
        private readonly ReactProperty<bool> _active = new ();

        [Inject]
        private void Construct(ILevel level)
        {
            level.Register(this);
        }
        
        private void OnEnable()
        {
            _controls ??= new PlayerControls();

            _active.Value = true;
            SubscribeOnControls();
            _controls.Enable();
        }

        private void OnDisable()
        {
            _active.Value = false;
            UnsubscribeOnControls();
            _controls.Disable();
        }

        public void OnLevelPrepared()
        {
            _active.Value = false;
            _controls.Disable();
        }

        public void OnLevelStarted()
        {
            _active.Value = true;
            _controls.Enable();
        }

        private void SubscribeOnControls()
        {
            _controls.Movement.Move.performed += ReadMoveAxis;
            _controls.Movement.Move.canceled += ReadMoveAxis;
        }

        private void UnsubscribeOnControls()
        {
            _controls.Movement.Move.performed -= ReadMoveAxis;
            _controls.Movement.Move.canceled -= ReadMoveAxis;
        }

        private void ReadMoveAxis(InputAction.CallbackContext context)
        {
            MoveAxis = context.ReadValue<Vector2>();
        }
    }
}