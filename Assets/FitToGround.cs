using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Monster
{
    public class FitToGround : MonoBehaviour
    {
        public bool debug;

        public float velocity = 5;
        public float turnSpeed = 10;
        public float height = .5f;
        public float heightPadding = .05f;
        public LayerMask ground;
        public float maxGroundAngle = 120;

        private Vector2 input;

        private float groundAngle;

        private Vector3 forward;
        private RaycastHit hitInfo;
        [SerializeField] private bool isGrounded;

        Quaternion targetRotation;
        float angle;

        private void Update()
        {
            // GetInput();
            // CalculateDirection();
            // CalculateForward();
            CalculateGroundAngle();
            CheckGround();
            ApplyGravity();
            DrawDebugLines();

            //if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

            //Move();
            //Rotate();
        }

        private void Move()
        {
            if (groundAngle >= maxGroundAngle) return;
               
            transform.position += forward * velocity * Time.deltaTime;
        }

        private void Rotate()
        {
            targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }


        private void GetInput()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            if (debug) Debug.Log("Horizontal Input: " + input.x);
            if (debug) Debug.Log("Vertical Input: " + input.y);
        }

        private void CalculateDirection()
        {
            angle = Mathf.Atan2(input.x, input.y);
            angle = Mathf.Rad2Deg * angle;
        }

        private void CalculateForward()
        {
            if (!isGrounded)
            {
                forward = transform.forward;
                return;
            }

            forward = Vector3.Cross(transform.right, hitInfo.normal);
        }

        private void CalculateGroundAngle()
        {
            if (!isGrounded)
            {
                groundAngle = 90;
                return;
            }

            groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
        }

        private void CheckGround()
        {
            /*
            if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
            {
                if (Vector3.Distance(transform.position, hitInfo.point) < height)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * height, Time.deltaTime);
                }
                isGrounded = true;
            }

            else
                isGrounded = false;
            */

            if (debug) Debug.Log("HitInfo.Distance: " + hitInfo.distance);
            if (debug) Debug.Log("HitInfo.Collider: " + hitInfo.collider);
        }

        private void ApplyGravity()
        {
            if (!isGrounded)
            {
                transform.position += Physics.gravity * Time.deltaTime;
            }
        }

        private void DrawDebugLines()
        {
            if (!debug) return;

            

            Debug.DrawLine(transform.position, transform.position + forward * height * 2, Color.blue);
            Debug.DrawLine(transform.position, transform.position - Vector3.up * height, Color.green);
        }
    }
}
