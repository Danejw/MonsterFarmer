using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterSettings : ScriptableObject
    {
        [Space(10)] [Header("Limits")][Space(2)]

        public int maxHappiness = 100;
        public int maxHealth = 100;
        public int lifespan = 100;
        public int maxEnergy = 100;
        public int maxStrength = 100;
        public int maxAgility = 100;
        public int maxSize = 100;
    }
}
