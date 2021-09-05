using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(KeyboardControllerInput))]

    public class AnimatorController : MonoBehaviour
    {
        private Animator anim;
        private KeyboardControllerInput controller;

        void Start()
        {
            anim = GetComponent<Animator>();
            controller = GetComponent<KeyboardControllerInput>();
        }

        void Update()
        {
            if (controller.isMovingForward)
            {
                anim.SetBool("IsMovingForward", true);

                // match animation speed with velocity
                anim.SetFloat("AnimationSpeed", Mathf.Abs(controller.velocity));
            }
            else
                anim.SetBool("IsMovingForward", false);


            if (controller.isMovingBackward)
            {
                anim.SetBool("IsMovingBackward", true);

                anim.SetFloat("AnimationSpeed", Mathf.Abs(controller.velocity));
            }
            else
                anim.SetBool("IsMovingBackward", false);


            if (controller.isTurningLeft)
            {
                anim.SetBool("IsTurningLeft", true);

                anim.SetFloat("AnimationSpeed", Mathf.Abs(controller.angularVelocity));
            }
            else
                anim.SetBool("IsTurningLeft", false);


            if (controller.isTurningRight)
            {
                anim.SetBool("IsTurningRight", true);

                anim.SetFloat("AnimationSpeed", Mathf.Abs(controller.angularVelocity));
            }
            else
                anim.SetBool("IsTurningRight", false);


            if (controller.isNotMoving)
            {
                anim.SetBool("IsNotMoving", true);

                anim.SetFloat("AnimationSpeed", 1);
            }
            else
                anim.SetBool("IsNotMoving", false);

        }
    }
}
