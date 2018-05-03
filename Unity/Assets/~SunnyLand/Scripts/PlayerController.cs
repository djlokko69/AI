using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyLand
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f; // Movement speed
        public int health = 100;
        public int damage = 50;
        [Tooltip("The force that's applied when hit by another object")]
        public float hitForce = 4f; // Force applied when players hits an object
        public float damageForce = 4f; // Force applied whjen player is hit by object
        public float maxVelocity = 3f; // Maximum velocity to limit the player to
        public float maxSlopeAngle = 45f; // Maximum angle the player can walk up
        [Header("Grounding")]
        public float rayDistance = .5f; // Distance of ground ray detector
        public bool isGrounded = false;
        [Header("Crouch")]
        public bool isCrouching = false;
        [Header("Jump")]
        public float jumpHeight = 2f; // Height that the player can jump to
        public int maxJumpCount = 2; // how many multi-jumps the player can do max
        public bool isJumping = false;
        [Header("Climb")]
        public float climbSpeed = 5f;
        public bool isClimbing = false;
        public bool isOnSlope = false;

        private Vector3 groundNormal = Vector3.up;
        private int currentJump = 0;
        private float inputH, inputV;
        // References
        private SpriteRenderer rend;
        private Rigidbody2D rigid;

        #region Unity Functions
        // Use this for initialization
        void Awake()
        {
            rend = GetComponent<SpriteRenderer>();
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            PerformClimb();
            PerformMove();
            PerformJump();

        }
        void FixedUpdate()
        {
            DetectGround();
            CheckSlope();
        }
        void OnDrawGizmos()
        {
            // Draw the gound ray
            Ray groundRay = new Ray(transform.position, Vector3.down);
            Gizmos.DrawLine(groundRay.origin,
                            groundRay.origin + groundRay.direction * rayDistance);
            // Draw direction line
            Vector3 right = Vector3.Cross(groundNormal, Vector3.forward);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position - right * 1f, 
                            transform.position + right * 1f);

        }
        #endregion

        #region Custom Functions
        public void Jump()
        {
            isJumping = true;
        }
        public void Crouch()
        {

        }
        public void UnCrouch()
        {

        }
        public void Move(float horizontal)
        {
            // If there is horizonal input
            if(horizontal != 0)
            {
                // Flip the sprite in the correct direction
                rend.flipX = horizontal < 0;
            }
            // Store the input for later
            inputH = horizontal;
        }
        public void Climb(float vertical)
        {

        }
        public void Hurt(int damage)
        {

        }

        private void PerformClimb()
        {

        }
        private void PerformMove()
        {
            Vector3 right = Vector3.Cross(groundNormal, Vector3.forward);
            // Add force in direction using horizontal input
            rigid.AddForce(right * inputH * speed);
            // Limit the velocity to max velocity
            LimitVelocity();
        }
        private void PerformJump()
        {
            // Are we Jumping
            if (isJumping)
            {
                // Are we allowed to Jump?
                if (currentJump < maxJumpCount)
                {
                    // Increment jump by 1
                    currentJump++;
                    rigid.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                }
                // Jump is finished!
                isJumping = false;
            }
        }
        private void DetectGround()
        {
            // Create a ground ray
            Ray groundRay = new Ray(transform.position, Vector3.down);
            // Shoot ray below the player and get all the hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(groundRay.origin, 
                                                       groundRay.direction, 
                                                       rayDistance);
            // Loop through all the hits
            foreach (var hit in hits)
            {
                // Check if we hit an enemy
                CheckEnemy(hit);
                // Check if we hit the ground
                if (CheckGround(hit))
                {
                    // Exit the loop
                    break;
                }
            }
        }
        private void CheckSlope()
        {
            if (isOnSlope) { rigid.drag = 5f; }
            else { rigid.drag = 0f; }
        }
        private bool CheckGround(RaycastHit2D hit)
        {
            // If 
            if(hit.collider!=null && // Exists and
               hit.collider.name!= name && // is not the player AND
               hit.collider.isTrigger == false) // is not a trigger
            {
                // Reset the jump
                currentJump = 0;
                // Player is in the grounded state
                isGrounded = true;
                // Updated the ground normal
                groundNormal = hit.normal;

                // Check for slopes
                float slopeAngle = Vector3.Angle(Vector3.up, hit.normal); // finding the angle between two axis
                isOnSlope = Mathf.Abs(slopeAngle) > 0 &&
                            Mathf.Abs(slopeAngle) < maxSlopeAngle;
                #region long way to write it
                /*if(Mathf.Abs(slopeAngle)> 0 && 
                   Mathf.Abs(slopeAngle) < maxSlopeAngle)
                {
                    isOnSlope = true;
                }
                else
                {
                    isOnSlope = false;
                }
                */
                #endregion

                // If we reached the max slope angle
                if (slopeAngle >= maxSlopeAngle)
                {
                    // Push the player down the
                    // slope (by adding more gravity)
                    rigid.AddForce(Physics2D.gravity);
                }


                // Return true! (ground is found)
                return true; 
            }

            // Return false! (ground is not found)
            return false;
        }
        private void CheckEnemy(RaycastHit2D hit)
        {

        }
        private void LimitVelocity()
        {
            // Cache rigid velocity into smaller variable
            Vector3 vel = rigid.velocity;
            // If vel length is greater than max velocity
            if (vel.magnitude > maxVelocity)
            {
                // Cap the velocity to maxVelocity
                vel = vel.normalized * maxVelocity;
            }
            // Apply newly calculated vel to rigidbody
            rigid.velocity = vel;
        }
        private void StopClimbing()
        {

        }
        private void DisablePhysics()
        {

        }
        private void EnablePhysics()
        {

        }

        #endregion
    }
}
