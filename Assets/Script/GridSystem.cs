using UnityEngine;

public class GridSystem
{
    private int altura;
    private int largura;
    private int tamanhoCelula;

    private GridObject[,] grid;
    public GridSystem(int altura, int largura, int tamanhoCelula)
    {
        this.altura = altura;
        this.largura = largura;
        this.tamanhoCelula = tamanhoCelula;

        grid = new GridObject[altura,largura];

        for(int i = 0; i < altura; i++)
        {
            for(int j = 0; j < largura; j++)
            {
                grid[i,j] = new GridObject(tamanhoCelula);
            }
        }

    }
    
}
public struct GridObject
{
    //propriedades que eu ainda desconheço

    public GridObject(int tamanhoCelula)
    {
        //tenho quase certeza que essa tamanhoCelula nao fica aqui mas tinha que colocar ele em algum lugar
    }
    
}