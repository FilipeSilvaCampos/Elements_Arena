using System.Collections;
using UnityEngine;


public class Grid
{
    int height;
    int width;

    public int[,] gridArray;

    public Grid(int height, int width)
    {
        this.height = height;
        this.width = width;

        gridArray = new int[height, width];
    }
}
