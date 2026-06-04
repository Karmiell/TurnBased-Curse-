using UnityEngine;
[CreateAssetMenu()]

public class ActionSO : ScriptableObject
{
 public BaseAction.State state;
 public string nameAction;
 public Transform visualAction; 
 public int actionCost;
}
