using System;
using Unity.Mathematics;
using UnityEngine;

public class HealthSystem : MonoBehaviour,IDamageble
{
[SerializeField]private int modCons = 10;

private Unit unit;
public event Action<int,int> OnHealthChange;
private int maxHealthPoints;
private int atualHealthPoints;
private GridPosition gridPosition;

    void Awake()
    {
        unit = GetComponent<Unit>();
        maxHealthPoints = unit.GetStatusBase().statusBase.constituicao * modCons;
        
    }

    private void Start()
    {
        gridPosition = unit.GetGridPosition();
        LevelGrid.Instance.AddDamagebleAtGridPosition(this, gridPosition);
        unit.OnGridPositionChange += unit_OnGridPositionChange;
        ResetHealth();
    }
    private void unit_OnGridPositionChange(object sender, EventArgs e)
    {
        var newPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
        LevelGrid.Instance.ChangeGridPositionDamageble(gridPosition, newPosition, this);
        gridPosition = newPosition;
    }

    public void TakeDamage(int amount)
    {
    atualHealthPoints -= amount;
    atualHealthPoints = math.clamp(atualHealthPoints, 0, maxHealthPoints);
        
    OnHealthChange?.Invoke(maxHealthPoints, atualHealthPoints);
    }
    private void ResetHealth()
    {
      atualHealthPoints = maxHealthPoints;
      OnHealthChange?.Invoke(maxHealthPoints, atualHealthPoints);

    }
}
