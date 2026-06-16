using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridSystem
{

    private int altura;
    private int largura;
    private int tamanhoCelula;

    public GridObject[,] grid;
    public GridSystem(int altura, int largura, int tamanhoCelula = 2)
    {
        this.altura = altura;
        this.largura = largura;

        
        this.tamanhoCelula = tamanhoCelula;

        grid = new GridObject[altura,largura];

        for(int i = 0; i < altura; i++)
        {
            for(int j = 0; j < largura; j++)
            {
                GridPosition gridPosition = new GridPosition(i,j);
                grid[i,j] = new GridObject(this, gridPosition);
                
                

            }
        }

    }
    public GridPosition GetGridPosition(Vector3 WordPosition)
    {
        return grid[
            Mathf.RoundToInt(WordPosition.x/tamanhoCelula), 
            Mathf.RoundToInt(WordPosition.z/tamanhoCelula)].gridPosition;
    }
   public Vector3 GetWorldPosition(int X, int Z)
    {
        return new Vector3(X,0,Z) * tamanhoCelula;
    }
    
    public void GridTextAtGridSystem(Transform textTransform)
    {
           for(int i = 0; i < altura; i++)
        {
            for(int j = 0; j < largura; j++)
            {
                GameObject.Instantiate(textTransform,GetWorldPosition(i,j),Quaternion.identity);

            }
        }
    }
     public GridObject GetGridObject(GridPosition gridPosition)
    {
        return grid[gridPosition.X, gridPosition.Z];
    }
    public bool ValidGridPositionAll(GridPosition gridPosition)
    {
        if(gridPosition.X >= 0 && gridPosition.X < largura &&
           gridPosition.Z >= 0 && gridPosition.Z < altura &&
           GetGridObject(gridPosition).HasNotUnit())return true;
        return false;
    }
    public bool ValidGridPosition(GridPosition gridPosition)
    {
        if(gridPosition.X >= 0 && gridPosition.X < largura &&
           gridPosition.Z >= 0 && gridPosition.Z < altura)return true;
        return false;
    }
    public int GetAltura() => altura;
    public int GetLargura() => largura;
}





public class GridObject
{
    
    
    public GridPosition gridPosition;
    private GridSystem gridSystem;
    private List<Unit> unitsList;
    private List<IDamageble> damageblesList;
    
    

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        
        unitsList = new List<Unit>();
        damageblesList = new List<IDamageble>();
        
    }
 
  
 public override string ToString()
    {
        string listUnitString = "";
        foreach(var atual in unitsList)
        {
            listUnitString += atual + "\n";
        }
        return $"X:{gridPosition.X}; Y:{gridPosition.Z}\n" + listUnitString;
    }
    public List<IDamageble> GetDamageblesList() => damageblesList;
    public List<Unit> GetUnitsList() => unitsList;
    public bool HasNotUnit() => unitsList.Count == 0;

}





public struct GridPosition
{
  public int X;
  public int Z;
 
    public GridPosition( int X, int Z)
    {
    this.X = X;
    this.Z = Z; 
    }
   
    public static bool operator != (GridPosition a, GridPosition b)
    {
        return !(a == b);
    }
      public static bool operator == (GridPosition a, GridPosition b)
    {
        return a.Z == b.Z && a.X == b.X;
    }
    public override bool Equals(object obj)
    {
        if(!(obj is GridPosition))return false;
        
        return (GridPosition)obj == this;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Z,X);
    }
    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.X + b.X, a.Z + b.Z);
    }
     public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.X - b.X, a.Z - b.Z);
    }
    public int GetX() => X;
    public int GetZ() => Z;

}