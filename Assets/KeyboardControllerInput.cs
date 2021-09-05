using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

[RequireComponent(typeof(CharacterController))]

public class KeyboardControllerInput : MonoBehaviour
{
    public bool debug;

    CharacterController controller;
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

        // default start state
        isNotMoving = true;
    }
    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        float verticalMove = Input.GetAxis("Vertical");

        // Moves the character forward based of its local rotations
        Vector3 move = transform.forward * verticalMove;
        move = move.normalized;
        float forwardAmount = Vector3.Dot(transform.forward, move);

        // cache the mode that the monster is in at runtime
        MonsterStats.mode mode = GetComponent<Monster.Monster>().stats.newMode;

        if (forwardAmount > 0)
        {
            // Move forwards
            controller.Move(speed * Time.deltaTime * move);
            // Sets mode to active if moving forward
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;

            // update state variables
            isMovingForward = true;
            isNotMoving = false;
        }
        else if (forwardAmount < 0)
        {
            // Move slower if moving backwards
            controller.Move((speed / 4) * Time.deltaTime * move);
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

        if (debug) Debug.Log(forwardAmount);

        velocity = verticalMove;
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
}
