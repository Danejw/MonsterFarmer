using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Monster
{
    public class MonsterDataPlugger : MonoBehaviour
    {
        public Monster monster;

        public TMP_Text nameData;
        public TMP_Text ageData;
        public TMP_Text sizeData;
        public TMP_Text healthData;
        public TMP_Text happinessData;
        public TMP_Text energyData;
        public TMP_Text strengthData;
        public TMP_Text agilityData;

        private void Update()
        {
            nameData.text = monster.stats.nickname;
            ageData.text = System.Math.Round( monster.stats.age, 2).ToString();
            sizeData.text = System.Math.Round(monster.stats.size, 2).ToString();
            healthData.text = System.Math.Round(monster.stats.health, 2).ToString();
            happinessData.text = System.Math.Round(monster.stats.happiness, 2).ToString();
            energyData.text = System.Math.Round(monster.stats.energy, 2).ToString();
            strengthData.text = System.Math.Round(monster.stats.strength, 2).ToString();
            agilityData.text = System.Math.Round(monster.stats.agility, 2).ToString();
        }
    }
}
