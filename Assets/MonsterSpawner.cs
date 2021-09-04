using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterSpawner : MonoBehaviour
    {
        public Player player;
        public GameObject monsterPrefab;

        public Vector3 position;
        public Quaternion direction;

        public void CreateMonster()
        {
            GameObject newMonster = Instantiate(monsterPrefab, position, direction);
            print(newMonster.GetComponent<Monster>().stats.name);
            player.AddMonster(newMonster.GetComponent<Monster>());
        }









    }
}
