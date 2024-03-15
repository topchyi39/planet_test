using System;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshUtilities
{
    public class AgentGroundAligner : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;

        private void LateUpdate()
        {
            agent.updateUpAxis = false;
            var rotation = Quaternion.FromToRotation(Vector3.up, agent.transform.position.normalized).eulerAngles;
            rotation.y = transform.eulerAngles.y;
            agent.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}