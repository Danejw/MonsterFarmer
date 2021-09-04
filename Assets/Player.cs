using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class Player : MonoBehaviour
    {
        private static Player instance;
        public static Player Instance
        {
            get { return instance; }
        }


        public string playerName;

        private string privateKey;
        private string publicKey;

        // player inventory
        [SerializeField] private List<Monster> monsters;

        private void Start()
        {
            if (!instance) instance = new Player();

            if (monsters == null)
                monsters = new List<Monster>();
        }

        public void AddMonster(Monster newMonster)
        {
            monsters.Add(newMonster);
            // Add a New Monster Event
        }

        public void RemoveMonster(Monster monster)
        {
            if (monsters.Contains(monster))
                monsters.Remove(monster);

            // Removed a Monster Event
        }

    }
}
