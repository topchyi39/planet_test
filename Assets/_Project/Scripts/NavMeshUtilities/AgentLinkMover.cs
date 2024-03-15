using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshUtilities
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentLinkMover : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private bool _isTravelling;
        
        private void OnValidate()
        {
            agent ??= GetComponent<NavMeshAgent>();

            if (agent.autoTraverseOffMeshLink) agent.autoTraverseOffMeshLink = false;
        }

        private void Update()
        {
            Debug.Log(_isTravelling);
            if (_isTravelling || !agent.isOnOffMeshLink) return;

            StartCoroutine(TravelRoutine());
        }
        
        private IEnumerator TravelRoutine()
        {
            _isTravelling = true;
            var data = agent.currentOffMeshLinkData;
            var startPoint = data.startPos;
            var endPoint = data.endPos + Vector3.up * agent.baseOffset;

            while (Vector3.Distance(agent.transform.position, endPoint) > 0.01f)
            {
                agent.transform.position =
                    Vector3.MoveTowards(agent.transform.position, endPoint, agent.speed * Time.deltaTime);
                agent.updateUpAxis = true;
                yield return new WaitForEndOfFrame();
            }
            
            agent.CompleteOffMeshLink();
            _isTravelling = false;
        }
        
    }
}