using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Bank : MonoBehaviour
{
    [SerializeField] int StartingBalance = 150;
    [SerializeField] int CurrentBalance;

    [SerializeField] TextMeshProUGUI displayBalance;
    public int currentBalance{get{return CurrentBalance ;}}

    void Awake() 
    {
        CurrentBalance = StartingBalance;  
        Updatebalance() ;
    }

    public void Deposit(int Amount)
    {
        CurrentBalance += Mathf.Abs(Amount);
        Updatebalance();
    }

    public void Withdraw(int Amount)
    {
        CurrentBalance -=Mathf.Abs(Amount);
        Updatebalance();

        if(CurrentBalance < 0)
        {
            //Game over.
            ReloadScene();
        }
    }

    void Updatebalance()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene CurrentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrentScene.buildIndex);
    }
    
}
