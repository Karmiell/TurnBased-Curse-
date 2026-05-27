using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private LayerMask layerMask;
    private int altura;
    private int largura;
    private int tamanhoCelula;

    private GridObject[,] grid;
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
                grid[i,j] = new GridObject(this, gridPosition);
                // acho que aqui deveria ter uma função como essa abaixo, mas isso tambem parece errado e nem sei como usaria esse Vector 3 ja que ele é baseado no mouse e fica mudando o tempo inteiro achei que devereria pegar 0 ponto 0,0,0 do mundo ou algo assim
                var position = MouseWorld.Instance.GetWorldMousePosition(layerMask).point;

            }
        }

    }
    /*public GridPosition GetGridPosition(Vector3 position)
    {
        //nao faço ideia de como tenho que usar esse Vector3 para retorna a posição exata dentro do grid mas lembro que existia alguma função parecida com essa
    

        return gridPosition;
    }*/
    
}
public class GridObject
{
    //essa classe foi feita em um outro scipt no curso mas acho que nao tem problema fazer no mesmo, é mais facil para vizualizar tudo
    public List<Unit> units;
    private bool isWalkable;
    private GridPosition gridPosition;
    private GridSystem gridSystem;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition, bool isWalkable = true)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
        units = new List<Unit>();

        
    }

}
public struct GridPosition
{
  private int X;
  private int Z;

    public GridPosition( int X, int Z)
    {
    this.X = X;
    this.Z = Z;
    }
    public override string ToString()
    {
        return $"X:{X}; Y:{Z}";
    }
    
}