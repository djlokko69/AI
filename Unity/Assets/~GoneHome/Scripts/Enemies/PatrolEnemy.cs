using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoneHome
{
    public class PatrolEnemy : MonoBehaviour
    {
        public Transform waypointGroup;
        public float movementSpeed = 10f;
        // How close the enemy needs to be to switch waypoints
        public float closeness = 1f;

        private Transform[] waypoints;
        private int currentIndex = 0;

        private Vector3 spawnPoint;

        // Use this for initialization
        void Start()
        {
            // Making a copy of the starting position
            spawnPoint = transform.position;

            int length = waypointGroup.childCount;
            waypoints = new Transform[length];
         // for (initialization; condition; iteration)
            for (int i = 0; i < length; i++)
            {
                waypoints[i] = waypointGroup.GetChild(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Get the current waypoint
            Transform current = waypoints[currentIndex];

            // Move the enemy towards current waypoint
            Vector3 position = transform.position;
            Vector3 direction = current.position - position;
            position += direction.normalized * movementSpeed * Time.deltaTime;
            // Apply new position to transform
            transform.position = position;
            // Rotation(Optional)
            transform.rotation = Quaternion.LookRotation(direction);

            // Get distance to current waypoint
            float distance = Vector3.Distance(position, current.position);
            // Is the enemy close to current waypoint?
            if (distance <= closeness)
            {
                // Switch to next waypoint
                currentIndex++;
            }

            // Check if currentIndex >= lenght (2)
            if (currentIndex >= waypoints.Length)
            {
                // Loop back around to first waypoint
                currentIndex = 0;
            }
        }

        public void Reset()
        {
            // Reset position of player to starting position
            transform.position = spawnPoint;
            currentIndex = 0;// Reset current waypoint to starting one
        }
    }
}
