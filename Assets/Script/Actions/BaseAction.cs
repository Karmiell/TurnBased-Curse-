using System;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    public enum State
    {
        UnitTurn,
        EnemyTurn,
        Move,
        Attacking,
        Healing,
        EndTurn,
    }
   
    
    [SerializeField]private State actionType;
    public virtual void ActionInteract(){}



}
