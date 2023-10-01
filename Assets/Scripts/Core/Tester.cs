using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class Tester : MonoBehaviour
{
    [SerializeField] int gridHeight = 1;
    [SerializeField] int gridWidth = 1;

    private void OnValidate()
    {
        Grid gridToDraw = new Grid(gridHeight, gridWidth);

        for(int i = 0; i < gridToDraw.gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridToDraw.gridArray.GetLength(1); j++)
            {
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j + 1), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.red, 100f);
            }
        }
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y);
    }
}
