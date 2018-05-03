using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoneHome
{
    public class Player : MonoBehaviour
    {
        public float movementSpeed = 10f;
        public float maxVelocity = 10f;
        public GameObject deathParticales;

        private Rigidbody rigid;
        private Transform cam; // << Camera ADDED

        private Vector3 spawnPoint;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();

            cam = Camera.main.transform; //<< Camera ADDED

            // Record starting position
            spawnPoint = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            Vector3 inputDir = new Vector3(inputH, 0, inputV);

            // Rotate input to face direction of Camera (flat on surface)
            inputDir = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * inputDir;

            /* OldCode: forces to move and Cant use with physics
            // Copy position
            Vector3 position = transform.position;
            // Offset to the new position
            position += inputDir * movementSpeed * Time.deltaTime;
            // Apply new position to rigidbody
            rigid.MovePosition(position);
            */

            //NewCode
            // Add Force to Rigidbody
            rigid.AddForce(inputDir * movementSpeed);
            // Copy velocity into smaller variable
            Vector3 vel = rigid.velocity;
            // Check if vel's magnitude is greater than velocity
            if (vel.magnitude > maxVelocity)
            {
                // Cap the Velocity
                vel = vel.normalized * maxVelocity;
            }
            // Apply the new velocity to rigidbody
            rigid.velocity = vel;

        }

        public void Reset()
        {
            // Spawn death particles where we reset
            Instantiate(deathParticales, transform.position, transform.rotation);
            // Reset position of the player to start position
            transform.position = spawnPoint;
            // Reset the Velocity
            rigid.velocity = Vector3.zero;
        }
    }
}
