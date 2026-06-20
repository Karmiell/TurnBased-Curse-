using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<Unit> AllyUnitList;
    private List<Unit> EnemyUnitList;
    private List<Unit> AllUnitList;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        AllyUnitList = new List<Unit>();
        EnemyUnitList = new List<Unit>();
        AllUnitList = new List<Unit>();
    }

    void Start()
    {
       Unit.OnAnyUnitSpawn += Unit_OnAnyUnitSpawn; 
    }

    private void Unit_OnAnyUnitSpawn(object sender, EventArgs e)
    {
        var unit = sender as Unit;
        if(unit.HasEnemy()) EnemyUnitList.Add(unit);
        else AllyUnitList.Add(unit);

        AllUnitList.Add(unit);    
    }

    public List<Unit> GetEnemyList() => EnemyUnitList;
    public List<Unit> GetAllyList() => AllyUnitList;
    public List<Unit> GetAllUnitList() => AllUnitList;


}
