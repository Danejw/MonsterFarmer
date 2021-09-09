using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Monster;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]

public class QuadDirectionalController : MonoBehaviour
{
    public bool debug;

    CharacterController controller;
    NavMeshAgent navAgent;
    public float speed = 2;
    public float rotationalSpeed = 1;

    // States
    public bool isNotMoving;
    public bool isMovingForward;
    public bool isMovingBackward;
    public bool isTurningLeft;
    public bool isTurningRight;
    public float velocity;
    public float angularVelocity;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();

        // default start state
        isNotMoving = true;
    }
    private void Update()
    {
        NavMeshMovement();
    }

    private void Move()
    {
        // cache the mode that the monster is in at runtime
        MonsterStats.mode mode = GetComponent<Monster.Monster>().stats.newMode;

        #region Forward Movement

        float verticalMove = Input.GetAxis("Vertical");
        // Moves the character forward based of its local rotations
        Vector3 forwardMove = transform.forward * verticalMove;
        forwardMove = forwardMove.normalized;
        float forwardAmount = Vector3.Dot(transform.forward, forwardMove);

        if (forwardAmount > 0)
        {
            // Move forwards
            controller.Move(speed * Time.deltaTime * forwardMove);
            // Sets mode to active if moving forward
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

            // update state variables
            isMovingForward = true;
            isNotMoving = false;
        }
        else if (forwardAmount < 0)
        {
            // Move slower if moving backwards
            controller.Move((speed / 4) * Time.deltaTime * forwardMove);
            // set mode to active if moving backwards
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

            // update state variables
            isMovingBackward = true;
            isNotMoving = false;

            // reduce energy decreasion rate?
        }
        // if not moving at all, set the mode to idle
        else
        {
            if (mode == MonsterStats.mode.active)
            {
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.idle;
                isNotMoving = true;
            }

            // update state variables
            isMovingBackward = false;
            isMovingForward = false;
        }

        velocity = verticalMove;

        if (debug) Debug.Log("Forward Move: " + forwardAmount);
        #endregion


        #region Horizontal Movement

        float horizontalMove = Input.GetAxis("Horizontal");
        // Moves the character horizontally
        Vector3 sidewaysMove = transform.right * horizontalMove;
        sidewaysMove = sidewaysMove.normalized;
        float horizontalAmount = Vector3.Dot(transform.right, sidewaysMove);

        if (horizontalAmount < 0)
        {
            // Move left
            controller.Move(speed * Time.deltaTime * sidewaysMove);
            // set mode to active if moving backwards
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

            // update state variables
            isTurningLeft = true;
            isNotMoving = false;

            if (debug) Debug.Log("Left");
            // reduce energy decreasion rate?
        }
        else if (horizontalAmount > 0)
        {
            // Move right
            controller.Move(speed * Time.deltaTime * sidewaysMove);
            // set mode to active if moving backwards
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

            // update state variables
            isTurningRight = true;
            isNotMoving = false;

            if (debug) Debug.Log("Right");
            // reduce energy decreasion rate?
        }
        // if not moving at all, set the mode to idle
        else
        {
            if (mode == MonsterStats.mode.active)
            {
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.idle;
                isNotMoving = true;
            }

            // update state variables
            isTurningLeft = false;
            isTurningRight = false;
        }

        if (debug) Debug.Log("Horizontal Move: " + horizontalAmount);
        #endregion
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Horizontal");

        transform.localEulerAngles += new Vector3(0, horizontalRotation * rotationalSpeed, 0);

        // Update turning left and turning right state variables 
        if (horizontalRotation > 0)
            isTurningRight = true;
        else if (horizontalRotation < 0)
            isTurningLeft = true;
        else
        {
            isTurningLeft = false;
            isTurningRight = false;
        }

        angularVelocity = horizontalRotation;
    }

    private void NavMeshMovement()
    {
            // cache the mode that the monster is in at runtime
            MonsterStats.mode mode = GetComponent<Monster.Monster>().stats.newMode;

            #region Forward Movement

            float verticalMove = Input.GetAxis("Vertical");
            // Moves the character forward based of its local rotations
            Vector3 forwardMove = transform.forward * verticalMove;
            forwardMove = forwardMove.normalized;
            float forwardAmount = Vector3.Dot(transform.forward, forwardMove);

            if (forwardAmount > 0)
            {
                // Move forwards
                navAgent.Move(speed * Time.deltaTime * forwardMove);
                // Sets mode to active if moving forward
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

                // update state variables
                isMovingForward = true;
                isNotMoving = false;
            }
            else if (forwardAmount < 0)
            {
                // Move slower if moving backwards
                navAgent.Move((speed / 4) * Time.deltaTime * forwardMove);
                // set mode to active if moving backwards
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

                // update state variables
                isMovingBackward = true;
                isNotMoving = false;

                // reduce energy decreasion rate?
            }
            // if not moving at all, set the mode to idle
            else
            {
                if (mode == MonsterStats.mode.active)
                {
                    GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.idle;
                    isNotMoving = true;
                }

                // update state variables
                isMovingBackward = false;
                isMovingForward = false;
            }

            velocity = verticalMove;

            if (debug) Debug.Log("Forward Move: " + forwardAmount);
            #endregion


            #region Horizontal Movement

            float horizontalMove = Input.GetAxis("Horizontal");
            // Moves the character horizontally
            Vector3 sidewaysMove = transform.right * horizontalMove;
            sidewaysMove = sidewaysMove.normalized;
            float horizontalAmount = Vector3.Dot(transform.right, sidewaysMove);

            if (horizontalAmount < 0)
            {
                // Move left
                navAgent.Move(speed * Time.deltaTime * sidewaysMove);
                // set mode to active if moving backwards
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

                // update state variables
                isTurningLeft = true;
                isNotMoving = false;

                if (debug) Debug.Log("Left");
                // reduce energy decreasion rate?
            }
            else if (horizontalAmount > 0)
            {
                // Move right
                navAgent.Move(speed * Time.deltaTime * sidewaysMove);
                // set mode to active if moving backwards
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

                // update state variables
                isTurningRight = true;
                isNotMoving = false;

                if (debug) Debug.Log("Right");
                // reduce energy decreasion rate?
            }
            // if not moving at all, set the mode to idle
            else
            {
                if (mode == MonsterStats.mode.active)
                {
                    GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.idle;
                    isNotMoving = true;
                }

                // update state variables
                isTurningLeft = false;
                isTurningRight = false;
            }

            if (debug) Debug.Log("Horizontal Move: " + horizontalAmount);
            #endregion
        }
}
