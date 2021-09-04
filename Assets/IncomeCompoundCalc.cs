using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeCompoundCalc : MonoBehaviour
{
    public float yearlyInvestment;
    public float apy;
    public int time;
    public float total;
    public float dividend;
    void Start()
    {
        for (int i = 0; i < time; i++)
        {
            dividend = total * apy;

            total += (yearlyInvestment + dividend);
        }
    }
}
