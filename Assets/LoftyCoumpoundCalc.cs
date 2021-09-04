using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoftyCoumpoundCalc : MonoBehaviour
{
    // preset variables
    public float amtOfTokens = 2;
    public float valueOfTokens = 50;
    public float totalTokens = 1959;
    public float monthlyTotalNetProfit = 524;

    // calculated Variables
    public float initialInvestment;
    public float monthlyDividendPerToken;
    public float yearlyDividendPerToken;


    public float monthlyDividends;

    public bool compound;

    public float totalPersonalNetWorth;
    public float incomeToSupercede = 4000;
    public float timeTillSupercedeInMonths;
    public float timeTillSupercedeInYears;


    void Start()
    {
        initialInvestment = amtOfTokens * valueOfTokens;

        CalcPerToken();

        int monthCount = 0;

        // how long will it take to supercede income
        while (monthlyDividends < incomeToSupercede)
        {
            monthlyDividends += monthlyDividendPerToken * amtOfTokens;
            monthCount++;

            if (compound)
            {
                // compound when the monthlynet profit can buy another property (>50), buy more tokens
                if (monthlyDividends > valueOfTokens)
                {
                    amtOfTokens += Mathf.RoundToInt(monthlyDividends / valueOfTokens);
                }
            }
        }

        // calculate how long it'll take
        timeTillSupercedeInMonths =  monthCount;
        timeTillSupercedeInYears = monthCount / 12;
        
    }

    public void CalcPerToken()
    {
        monthlyDividendPerToken = monthlyTotalNetProfit / totalTokens;
        yearlyDividendPerToken = monthlyDividendPerToken * 12;
    }
}
