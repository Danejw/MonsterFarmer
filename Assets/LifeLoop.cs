using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Life
{
    public class LifeLoop : LifeExpense
    {
        [SerializeField] private float account;
        [SerializeField] private float accountDebt;



        public bool snowballCashflow;
        public bool payDownDebtWithIncome;
        public float payDownAmount;


        [SerializeField] private int timeStep;

        public int timeInterval;
        public float timeTillAdjustment;

        private void Start()
        {
            accountDebt = AddAssetDebt();
        }


        private void Update()
        {
            // loop that timesteps are controlable by its parameters
            timeTillAdjustment += 1 * Time.deltaTime;
            if (timeTillAdjustment >= timeInterval)
            {
                account += income - AddLivingCosts();

                if (accountDebt > 0)
                {
                    // pay down debt with asset cashflow
                    if (snowballCashflow)
                        accountDebt -= CalcAssetCashflow();
                    else
                        account += CalcAssetCashflow();

                    // pay down debt with income
                    if (payDownDebtWithIncome)
                    {
                        // subtract from income
                        account -= payDownAmount;
                        // subtract from debt amount
                        accountDebt -= payDownAmount;
                    }
                }
                else
                {
                    account += CalcAssetCashflow();
                }


                timeTillAdjustment = 0;
                timeStep += 1;
            }
        }



    }
}