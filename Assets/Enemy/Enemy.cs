using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int GoldReward = 25;
    [SerializeField] int GoldPenalty = 25;
    Bank bank;
    
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if(bank == null) {return;}
        bank.Deposit(GoldReward);
    }

     public void StealGold()
    {
        if(bank == null) {return;}
        bank.Withdraw(GoldPenalty);
    }

    
}
