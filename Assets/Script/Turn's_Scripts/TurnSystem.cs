using System;
using UnityEngine;


public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance;


public event EventHandler OnTurnChange;
  private bool unitTurn = true;

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
        unitTurn = !unitTurn;
        OnTurnChange?.Invoke(this, EventArgs.Empty);
    }
    public bool GetUnitTurnBool() => unitTurn;
    



}
