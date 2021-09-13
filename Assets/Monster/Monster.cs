using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    [RequireComponent(typeof(Rigidbody))]
    public class Monster : MonoBehaviour
    {
        public bool debug;

        public MonsterStats stats;

        [SerializeField] private bool isSelected;

        [SerializeField] protected float ageSpeed = 1;
        [SerializeField] protected float recoverySpeed = 1;
        [SerializeField] protected float recoveryInterval = 1;

        private Rigidbody body;
        private Renderer render;
        private Color color;
        private Vector3 size;

        private List<float> dataHappiness;
        private List<float> dataEnergy;
        [SerializeField] private int averagingInterval = 5;
        [SerializeField] private float averageHappiness;
        // private float averageEnergy;

        private int prevAge;
        private bool isAnniversary;

        #region Unity Methods
        private void Start()
        {
            body = GetComponent<Rigidbody>();
            render = GetComponent<Renderer>();

            dataHappiness = new List<float>();
            dataEnergy = new List<float>();
            prevAge = Mathf.RoundToInt(stats.age);

            color = new Color(stats.red, stats.green, stats.blue);
            size = new Vector3(stats.size, stats.size, stats.size);
        }

        protected virtual void Update()
        {
            // Color

            // Sets the color of the monster at runtime
            color.r = stats.red;
            color.g = stats.green;
            color.b = stats.blue;
            if (render) render.material.color = color;


            // States
            switch (stats.newMode)
            {
                case MonsterStats.mode.idle:
                    break;

                case MonsterStats.mode.sleep:
                    IncreaseHealth();
                    IncreaseEnergy();

                    if (stats.health == stats.maxHealth && stats.energy == stats.maxEnergy) stats.newMode = MonsterStats.mode.idle;
                    break;

                case MonsterStats.mode.active:
                    if (stats.newMode != MonsterStats.mode.sleep && stats.newMode != MonsterStats.mode.freeze)
                    {
                        Age();
                        LivingCosts();
                        Grow();
                        DecreaseHappiness();
                    }

                    if (stats.health == 0) stats.newMode = MonsterStats.mode.freeze; // no health
                    if (stats.age == stats.lifespan) stats.newMode = MonsterStats.mode.deepFreeze; // no lifespan left
                    if (stats.energy == 0) stats.newMode = MonsterStats.mode.sleep; // no energy;
                    if (stats.happiness == 0) stats.newMode = MonsterStats.mode.idle;

                    break;

                case MonsterStats.mode.care:
                    IncreaseHappiness();

                    if (stats.happiness == stats.maxHappiness) stats.newMode = MonsterStats.mode.idle;
                    break;

                case MonsterStats.mode.freeze:
                    break;

                case MonsterStats.mode.deepFreeze:
                    break;
            }

            // Birthday
            // Populates data lists every birthday year
            if (prevAge != Mathf.RoundToInt(stats.age))
            {
                dataHappiness.Insert(0, stats.happiness);
                dataEnergy.Insert(0, stats.energy);

                if (dataHappiness.Count > averagingInterval)
                {
                    dataHappiness.RemoveAt(averagingInterval);
                    dataEnergy.RemoveAt(averagingInterval);
                }

                if (debug) Debug.Log("Happy Birthday! Your Monster just turned " + Mathf.Round(stats.age));

                // increment prev age
                prevAge = Mathf.RoundToInt(stats.age);

                // Anniversary year?
                if (Mathf.RoundToInt(stats.age) % averagingInterval == 0) isAnniversary = true;
            }

            // Anniversary
            // Calculates averages from the data lists
            if (isAnniversary)
            {
                // calculate average happiness over the last x years
                averageHappiness = 0;
                foreach (float data in dataHappiness)
                    averageHappiness += data;

                averageHappiness /= averagingInterval;

                // if the monsters average happiness is below average, the lifespan is shortened
                if (averageHappiness < stats.maxHappiness / 2) stats.lifespan -= 1;
                // if the monster's average happiness is above average, the lifespan is lengthened
                else if (averageHappiness > stats.maxHappiness / 2) stats.lifespan += 1;

                /*
                // calculate average energy over the last x years
                averageEnergy = 0;
                foreach (float data in dataEnergy)
                    averageEnergy += data;

                averageEnergy /= averagingInterval;

                
                // if the monsters average energy is below average, the happiness is decreased
                if (averageEnergy < stats.maxEnergy / 2) stats.happiness -= 5;
                // if the monster's average energy is above average, the happiness is increased
                else if (averageEnergy > stats.maxEnergy / 2) stats.happiness += 5;
                */

                isAnniversary = false;

                if (debug) Debug.Log("Happy " + Mathf.Round( stats.age ) + " Year Anniversary! Your Monster just turned " + Mathf.Round( stats.age ));
            }


        }
        #endregion

        #region Private Methods
        private void Age()
        {
            if (stats.age == stats.lifespan) return;

            // Age increases lifespan incrementally until lifespan is reached
            if (stats.age < stats.lifespan)
                stats.age += ageSpeed * Time.deltaTime;
            else if (stats.age >= stats.lifespan)
            {
                stats.age = stats.lifespan;

                if (debug) Debug.Log("Dead!");
            }
        }

        private void LivingCosts()
        {
            // Energy decreasing overtime (living costs)
            if (stats.energy > 0 && stats.energy <= stats.maxEnergy)
            {
                stats.energy -= ageSpeed * Time.deltaTime;

                if (debug) Debug.Log("Applying Living Costs");
            }
            else if (stats.energy <= 0)
            {
                stats.energy = 0;

                if (debug) Debug.Log("No Energy");
            }
        }

        private void Grow()
        {
            if (stats.size == stats.maxSize) return;

            // Increases the size of the monster as it ages at runtime
            if (stats.size >= 0 && stats.size < 10) 
                stats.size += ageSpeed * Time.deltaTime;
            else
            {
                stats.size = stats.maxSize;

                if (debug) Debug.Log("Max Sized");
            }


            size.x = stats.size;
            size.y = stats.size;
            size.z = stats.size;
            transform.localScale = size;
        }

        private void IncreaseHappiness()
        {
            if (stats.happiness < stats.maxHappiness) System.Math.Round( stats.happiness += recoverySpeed * Time.deltaTime * recoveryInterval, 2);
            else
            {
                stats.happiness = stats.maxHappiness;

                if (debug) Debug.Log("Very Happy! Yay");

            }

        }

        private void DecreaseHappiness()
        {
            if (stats.happiness > 0) System.Math.Round( stats.happiness -= recoverySpeed * Time.deltaTime * recoveryInterval, 2);
            else
            {
                stats.happiness = 0;

                if (debug) Debug.Log("UnHappy");
            }

        }


        private void IncreaseHealth()
        {
            if (stats.health < stats.maxHealth) stats.health += recoverySpeed * Time.deltaTime * recoveryInterval;
            else
            {
                stats.health = stats.maxHealth;

                if (debug) Debug.Log("Fully Rested");
            }
        }

        private void IncreaseEnergy()
        {
            if (stats.energy < stats.maxEnergy) stats.energy += recoverySpeed * Time.deltaTime * recoveryInterval;
            else
            {
                stats.energy = stats.maxEnergy;

                if (debug) Debug.Log("Fully Recharged");
            }
        }

        #endregion

        #region Public Methods
        public void addEnergy(float amt)
        {
            if (stats.energy < stats.maxEnergy) stats.energy += amt;
            if (stats.energy > stats.maxEnergy) stats.energy = stats.maxEnergy;

            // Measure here to get the total amount of energy added (event)
        }

        private void removeEnergy(float amt)
        {
            if (stats.energy > 0) stats.energy -= amt;
            if (stats.energy - amt < 0) stats.energy = 0;

            // Measure here to get the total amount of energy removed (event)
        }

        public void gotoSleep()
        {
            stats.newMode = MonsterStats.mode.sleep;
            Debug.Log("Sleeping");
            // Sleep Event
        }

        public void Active()
        {
            stats.newMode = MonsterStats.mode.active;
            Debug.Log("Active");
            // Active Event
        }
        public void TakeCare()
        {
            stats.newMode = MonsterStats.mode.care;
            Debug.Log("Caring");
            // Care Event
        }

        public void Feed()
        {
            addEnergy(5);
            Debug.Log("Added Energy through food");
            // Eat Event
        }

        // To Do: Make sure the nickname can only be changed by the owner
        public void ChangeNickName(string name)
        {
            if (Player.Instance.playerName != stats.owner) return;

            stats.nickname = name;
            // Change Name Event

            if (debug) Debug.Log("Changed Nickname to: " + name);
        }



        // Bigger sized monsters means more results in strength training, but less results in agility training
        // Training randomizes the outputs with a range of 1-10, using size, energy as parts of inputs in the formula
        // The Size and Energy Levels play a role in getting better results from the training

        // To Do: (need to add happiness factor into account as well)
        public void DoStrengthTraining()
        {
            if (stats.strength < stats.maxStrength && stats.newMode != MonsterStats.mode.sleep && stats.newMode != MonsterStats.mode.freeze)
            {
                float performance = Random.Range(1, 10);
                float result = performance * ( stats.size * .01f) + performance * ( stats.energy * .01f);
                stats.strength += result;

                if (stats.strength > stats.maxStrength)
                {
                    stats.strength = stats.maxStrength;

                    if (debug) Debug.Log("Your Monster Strength Stats are Maxed Out!");
                }

                if (debug) Debug.Log("Your Monster Gained " + result + " Strength Points because of the training");
            }
        }

        public void DoAgilityTraining()
        {
            if (stats.agility < stats.maxAgility && stats.newMode != MonsterStats.mode.sleep && stats.newMode != MonsterStats.mode.freeze)
            {
                float performance = Random.Range(1, 10);
                float result = performance * ((100 - stats.size) * .01f) + performance * ( stats.energy * .01f);
                stats.agility += result;

                if (stats.agility > stats.maxAgility)
                {
                    stats.agility = stats.maxAgility;

                    if (debug) Debug.Log("Your Monster Agility Stats are Maxed Out!");
                }

                if (debug) Debug.Log("Your Monster Gained " + result + " Agility Points because of the training");
            }
        }

        #endregion
    }
}
