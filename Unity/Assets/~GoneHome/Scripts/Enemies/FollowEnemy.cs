using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
// Clean code: CTRL + K + D (In that order)

namespace GoneHome
{
    public class FollowEnemy : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent agent;

        private Vector3 spawnPoint;

        // Use this for initialization
        void Start()
        {
            // Making a copy of the starting position
            spawnPoint = transform.position;

            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(target.position);
        }

        public void Reset()
        {
            // Reset position of player to starting position
            transform.position = spawnPoint;
        }
    }
}
