using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

[RequireComponent(typeof(CharacterController))]
public class KeyboardControllerInput : MonoBehaviour
{
    CharacterController controller;
    public float speed = 2;
    public float rotationalSpeed = 1;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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

        MonsterStats.mode mode = GetComponent<Monster.Monster>().stats.newMode;

        if (forwardAmount == 1)
        {
            // Move forwards
            controller.Move(speed * Time.deltaTime * move);
            // Sets mode to active if moving forward
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;
        }
        else if (forwardAmount == -1)
        {
            // Move slower if moving backwards
            controller.Move((speed / 4) * Time.deltaTime * move);
            // set mode to active if moving backwards
            GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.active;
            // reduce energy decreasion rate?
        }
        // if not moving at all, set the mode to idle
        else
        {
            if (mode == MonsterStats.mode.active)
                GetComponent<Monster.Monster>().stats.newMode = MonsterStats.mode.idle;
        }

        print(forwardAmount);
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Horizontal");

        transform.localEulerAngles += new Vector3(0, horizontalRotation * rotationalSpeed, 0);
    }
}
