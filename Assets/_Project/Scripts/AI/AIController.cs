using System;
using Character;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private CharacterView view;
        
        protected IPlayer Player { get; private set; }
        protected ICharacterView OwnerView => view;

        [Inject]
        private void Construct(IPlayer player)
        {
            Player = player;
        }
        
        private void LateUpdate()
        {
            var currentNormalizedSpeed = Mathf.InverseLerp(0, agent.speed, agent.velocity.sqrMagnitude);

            if (currentNormalizedSpeed <= 0)
            {
                agent.enabled = false;
            }
            
            view.UpdateVerticalSmoothly(currentNormalizedSpeed);
            
            LateUpdateCallback();
        }

        public void SetTarget(Vector3 position)
        {
            if (Vector3.Distance(agent.transform.position, position) < agent.stoppingDistance) return;
            agent.enabled = true;
            agent.destination = position;
        }

        protected virtual void LateUpdateCallback() { }
        
    }
}