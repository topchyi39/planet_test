using System;
using Character;
using Player.Input;
using UnityEngine;
using Zenject;

namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovementConfig config;
        [SerializeField] private Rigidbody playerRigidbody;
        
        [SerializeField] private Transform groundCenter;
        
        [SerializeField] private float moveThreshold = 0.03f;
        
        private IPlayerInput _input;
        private ICharacter _character;
        
        private Vector2 _moveInput;
        
        private Vector3 _horizontalVelocity;
        private Vector3 _verticalVelocity;
        
        private Vector3 _alignPoint;
        private Vector3 _alignNormal;
        
        private IPlayerView View => _character?.View;
        private Vector3 CenterDirection => (groundCenter.position - transform.position).normalized;

        private const float GravityForce = 9.8f;
        
        private void OnValidate()
        {
            playerRigidbody ??= GetComponent<Rigidbody>();
        }

        [Inject]
        private void Construct(IPlayerInput input)
        {
            _input = input;
        }

        private void Awake()
        {
            playerRigidbody.freezeRotation = true;
            playerRigidbody.useGravity = false;
        }

        private void Update()
        {
            _moveInput = _input.MoveAxis;
            Rotate();
            ProcessAnimation();
        }

        private void FixedUpdate()
        {
            AlignToGround();
            MoveHorizontal();
        }
        
        public void Init(ICharacter character)
        {
            _character = character;
        }
        
        private void Rotate()
        {
            var alignRotation = Quaternion.FromToRotation(transform.up, -CenterDirection);
            transform.rotation = alignRotation * transform.rotation;
            
            if (!(_moveInput.magnitude > moveThreshold)) return;
            
            var direction = new Vector3(_moveInput.x, 0, _moveInput.y);
            var eulerAnglesY = Quaternion.LookRotation(direction).eulerAngles.y;
            View.RotateYSmooth(eulerAnglesY, config.RotationSpeed * Time.deltaTime);
        }

        private void MoveHorizontal()
        {
            var direction = new Vector3(_moveInput.x, 0, _moveInput.y);
            direction = transform.TransformDirection(direction);
            
            var velocity = direction * config.Speed;
            velocity -= playerRigidbody.velocity;
            
            
            playerRigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }

        private void AlignToGround()
        {
            playerRigidbody.AddForce(CenterDirection * GravityForce, ForceMode.Acceleration);
        }

        private void ProcessAnimation()
        {
            View.UpdateVerticalSmoothly(_moveInput.magnitude);
        }
    }
}
