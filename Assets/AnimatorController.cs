using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        Animator anim;
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (GetComponent<Monster>().stats.newMode == MonsterStats.mode.active)
            {
                anim.SetBool("Active", true);
                anim.SetBool("Idle", false);
            }
            else if (GetComponent<Monster>().stats.newMode == MonsterStats.mode.idle)
            {
                anim.SetBool("Idle", true);
                anim.SetBool("Active", false);
            }
        }
    }
}
