using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MonsterData")]
    public class MonsterStats : MonsterSettings
    {
        [Space(10)][Header("Color")][Space(2)]

        [Range(0, 1)] public float red;
        [Range(0, 1)] public float green;
        [Range(0, 1)] public float blue;


        [Space(5)][Header("Meta Data")][Space(2)]

        public string owner;
        public string nickname;


        [Space(5)] [Header("Stats")][Space(2)]

        [Range(0, 100)] public float age;

        [Range(1, 10)] public float size;

        [Range(1, 100)] public float happiness;

        public enum mode { idle, active, care, sleep, freeze, deepFreeze }
        [SerializeField] public mode newMode;

        [Range(1, 100)] public float health;

        [Range(0, 100)] public float energy;

        [Range(1, 100)] public float strength;

        [Range(1, 100)] public float agility;
    }

}