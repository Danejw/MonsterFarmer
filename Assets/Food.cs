using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Game
{
    public class Food : MonoBehaviour
    {
        public delegate void CollectFood();
        public static event CollectFood onCollectFood;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Monster")
            {
                onCollectFood?.Invoke();
                
                // play sound
                if (!audioSource.isPlaying) audioSource?.Play();
            }
        }
    }
}
