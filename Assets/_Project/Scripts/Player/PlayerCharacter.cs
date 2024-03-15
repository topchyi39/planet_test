using Character;
using Game.Levels;
using Player.Input;
using Player.Movement;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Player
{
    
    public class PlayerCharacter : MonoBehaviour, ICharacter, IPlayer, ILevelListener
    {
        public Vector3 Position => transform.position;
        public IPlayerView View => view;
        public bool LockOn { get; private set; }
        
        [SerializeField] private PlayerView view;
        [SerializeField] private PlayerMovement movement;
        
        private IPlayerInput _input;

        [Inject]
        private void Construct(IPlayerInput input, ILevel level)
        {
            movement.Init(this);
            level.Register(this);
        }

        public void OnLevelPrepared()
        {
            gameObject.SetActive(false);
            movement.enabled = false;
        }

        public void OnLevelStarted()
        {
            gameObject.SetActive(true);
            movement.enabled = true;
        }
    }
}