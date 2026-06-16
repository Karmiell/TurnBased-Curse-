using System;
using UnityEngine;

public interface IDamageble
{
public event Action<int,int> OnHealthChange;
public void TakeDamage(int amount);

}
