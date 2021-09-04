using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Life
{
    public class LifeExpense : MonoBehaviour
    {
        public float income;
        public List<float> livingCosts;
        [SerializeField] private float totalLivingCosts;

        public List<float> assetDebt;
        public List<float> debtPayments;
        public List<float> assetDividends;

        [SerializeField] private float totalAssetDebt;
        [SerializeField] private float totalAssetDebtPayments;
        [SerializeField] private float totalAssetDividends;

        [SerializeField] private float assetCashflow;
        [SerializeField] private float profits;

 
        public void ReCalculate()
        {
            AddLivingCosts();
            AddAssetDebt();
            CalcAssetCashflow();
            CalcProfit();
        }

        public float AddLivingCosts()
        {
            totalLivingCosts = 0;

            foreach (float cost in livingCosts)
                totalLivingCosts += cost;

            return totalLivingCosts;
        }

        public float AddAssetDebt()
        {
            totalAssetDebt = 0;

            foreach (float debt in assetDebt)
                totalAssetDebt += debt;

            return totalAssetDebt;
        }

        public float AddAssetDebtPayments()
        {
            totalAssetDebtPayments = 0;

            foreach (float asset in debtPayments)
                totalAssetDebtPayments += asset;

            return totalAssetDebtPayments;
        }

        public float AddAssetDividends()
        {
            totalAssetDividends = 0;

            foreach (float asset in assetDividends)
                totalAssetDividends += asset;

            return totalAssetDividends;
        }

        public float CalcAssetCashflow()
        {
            assetCashflow = AddAssetDividends() - AddAssetDebtPayments();
            return assetCashflow;
        }

        public float CalcProfit()
        {
            profits = (income + CalcAssetCashflow()) - AddLivingCosts();
            return profits;
        }
    }
}
