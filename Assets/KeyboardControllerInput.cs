using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        var forwardAmount = Vector3.Dot(transform.forward, move);

        // Move slower if moving backwards
        if (forwardAmount > 0)
            controller.Move(speed * Time.deltaTime * move);
        else if (forwardAmount < 0)
            controller.Move((speed/4) * Time.deltaTime * move);
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Horizontal");

        transform.localEulerAngles += new Vector3(0, horizontalRotation * rotationalSpeed, 0);
    }
}
