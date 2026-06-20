using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnSystem : MonoBehaviour
{
public static TurnSystem Instance;

public event EventHandler OnTurnChange;
private bool unitTurn = true;
private int countTurns = 1;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    

    public void NextTurn()
    {
        countTurns++;
        unitTurn = !unitTurn;
        OnTurnChange?.Invoke(this, EventArgs.Empty);
    }
    public bool GetUnitTurnBool() => unitTurn;
    public int GetCurrentTurnInt() => countTurns;
    



}
