using System;
using System.Threading;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
private enum TurnStates
    {
        WaitingUnitTurn, 
        TakingTurn,
        ReTake
    }

    private TurnStates curentState;

    private void Awake()
    {
        curentState = TurnStates.WaitingUnitTurn;
    }
    private void Start()
    {
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange; 
    }

    private void Update()
    {
        if(TurnSystem.Instance.GetUnitTurnBool())return;

        switch (curentState)
        {
            case TurnStates.WaitingUnitTurn:
            //acho que esse é vazio mesmo
            break;

            case TurnStates.TakingTurn:
            //if(Timer(5))return;

            foreach(var atual in UnitManager.Instance.GetEnemyList())
            {
            if(TryDoBestAction(atual))Debug.Log("Inimigo Fazendo coisa!");

            else TurnSystem.Instance.NextTurn();
            }
            break;

            case TurnStates.ReTake:
            //lembro que tinha três opções mas não sei para que isso serve
            break;
        }
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        curentState = TurnStates.TakingTurn;
    }

    private bool TryDoBestAction(Unit unit)
    {
        var actionMove = unit.GetActionMove();
        if(unit.TryDoAction(actionMove, actionMove.GetGridPositionList()[UnityEngine.Random.Range(0, actionMove.GetGridPositionList().Count)]))return true;


        return false;
    }
    private void Clear()
    {
        
    }
    private bool Timer(float time)
    {
        float atual = 0f;
        atual += Time.deltaTime;
        if(atual < time)return true;
        return false;
    }
}
