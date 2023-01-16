using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;
    private GridObject[,] gridObjectArray; //array with 2 dimensions

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < this.width; x++)
        {
            for (int z=0; z < this.height; z++)
            {
               GridPosition gridPosition = new GridPosition(x, z);
               gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }
        
    }

    //gets the x/z position in Unity's world position 
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    //gets position on the grid
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public void CreateDebugObject(Transform debugPrefab)
    {
        for (int x = 0; x < this.width; x++)
        {
            for (int z = 0; z < this.height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);


                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
}