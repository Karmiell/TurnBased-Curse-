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

        //esse tamanhoCelula era usado em alguma multiplicação por um Vector 3 que veio do mouse, mas nao vejo como eu faria isso e nem onde colocaria o resultado dessa equação.
        this.tamanhoCelula = tamanhoCelula;

        grid = new GridObject[altura,largura];

        for(int i = 0; i < altura; i++)
        {
            for(int j = 0; j < largura; j++)
            {
                GridPosition gridPosition = new GridPosition(i,j);
                var WordPosition = GetWorldPosition(i,j);
                grid[i,j] = new GridObject(this, gridPosition);
                // acho que aqui deveria ter uma função como essa abaixo, mas isso tambem parece errado e nem sei como usaria esse Vector 3 ja que ele é baseado no mouse e fica mudando o tempo inteiro achei que devereria pegar 0 ponto 0,0,0 do mundo ou algo assim
                

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
}

public class GridObject
{
    
    private bool isWalkable;
    public GridPosition gridPosition;
    private GridSystem gridSystem;
     private List<Unit> unitsList;
    
    

    public GridObject(GridSystem gridSystem, GridPosition gridPosition, bool isWalkable = true)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        
        unitsList = new List<Unit>();
        
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
    public List<Unit> GetUnitsList() => unitsList;

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

}