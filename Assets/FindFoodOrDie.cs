using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.Game
{
    public class FindFoodOrDie : MonoBehaviour
    {
        /*
        // Singleton
        private static FindFoodOrDie instance;
        public static FindFoodOrDie Instance
        {
            get { return instance; }
        }
        */

        public GameObject platform;
        public GameObject food;
        public GameObject monster;

        private GameObject foodInstance;
        private Vector3 platformSize;

        // Random Positioning
        Vector3 foodPostion;
        Vector3 startingMonsterPosition;

        private void Start()
        {
            // subsribe to events
            Food.onCollectFood += ResetFood;
            Food.onCollectFood += AddEnergyToMonster;

            platformSize = platform.transform.lossyScale;
            
            Init();
        }

        private void OnDestroy()
        {
            Food.onCollectFood -= ResetFood;
        }


        private void Init()
        {
            foodPostion.z = GenerateRandomZ();
            foodPostion.x = GenerateRandomX();
            foodPostion.y = 0.1f;

            ResetMonster();

            if (!foodInstance)
                foodInstance = Instantiate(food, new Vector3(foodPostion.x, foodPostion.y, foodPostion.z), Quaternion.identity, transform);
        }
        public void ResetFood()
        {
            foodPostion.z = GenerateRandomZ();
            foodPostion.x = GenerateRandomX();
            foodPostion.y = 0.1f;

            foodInstance.transform.position = new Vector3(foodPostion.x, foodPostion.y, foodPostion.z);
        }

        public void ResetMonster()
        {
            startingMonsterPosition.z = GenerateRandomZ();
            startingMonsterPosition.x = GenerateRandomX();
            startingMonsterPosition.y = .001f;

            monster.transform.position = new Vector3(startingMonsterPosition.x, startingMonsterPosition.y, startingMonsterPosition.z);
        }

        private void AddEnergyToMonster()
        {
            monster.GetComponent<Monster>()?.addEnergy(10);
        }

        private float GenerateRandomZ()
        {
            return Random.Range(-platformSize.z / 2 + 1, platformSize.z / 2 - 1);
        }
        private float GenerateRandomX()
        {
            return Random.Range(-platformSize.x / 2 + 1, platformSize.x / 2 - 1);
        }

    }
}
