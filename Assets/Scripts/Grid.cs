using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;


public class Grid : MonoBehaviour, IGrid
{ 
    [ShowInInspector] List<List<GameObject>> GridSystem = new List<List<GameObject>>();
    [ShowInInspector] List<List<GameObject>> TileContainer = new List<List<GameObject>>();
    [SerializeField] private GameObject[] col1;
    [SerializeField] private GameObject[] col2;
    [SerializeField] private Tile theTile;
    public GameObject gridCellPrefab;
    private void Start()
    {    
        
        InitializeGrid(4, 6,GridSystem);
        InitializeGrid(4, 6,TileContainer);
//        for (int i = 0; i < col1.Length; i++)
//        {
//            if (col1[i] != null)
//            {
//                GridSystem[0][i] = col1[i];
//            }
//        }
//        for (int i = 0; i < col2.Length; i++)
//        {
//            if (col2[i] != null)
//            {
//                GridSystem[1][i] = col2[i];
//            }
//        }
        GenerateGrid();
        //theTile.SetDependency(this);
    }
    
    private void InitializeGrid(int cols, int rows,List<List<GameObject>> twoDArray)
    {
        for (int i = 0; i < cols; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < rows; j++)
            {
                row.Add(null);
            }
            twoDArray.Add(row);
        }
    }

     void IGrid.AddSelectedObjectInGrid(GameObject tile,GameObject pickedObject)
    {
        int cols = 4, rows = 6;
        int plotColumnIndex=-1, plotRowIndex=-1;
        
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (TileContainer[i][j] == tile)
                {
                    plotColumnIndex = i;
                    plotRowIndex = j;
                    break;
                }
            }
        }

        if (plotColumnIndex != -1 || plotRowIndex != -1)
        {    
            Debug.Log("-> "+plotColumnIndex +" | " +plotRowIndex);
            GridSystem[plotColumnIndex][plotRowIndex] = pickedObject;
        }
        CheckForAdjacent(plotColumnIndex,plotRowIndex);   
    }

     private void CheckForAdjacent(int colIndexOfShell,int rowIndexOfShell)
    {
        if ((rowIndexOfShell + 1)<6)
        {
            Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
        }// looking at one next in a row

        if((rowIndexOfShell-1)>= 0 && GridSystem[colIndexOfShell][rowIndexOfShell + -1] != null)
        {
            if (GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell][rowIndexOfShell + -1].name)
            {
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            }// looking for one previous in a row
        }

        if ((colIndexOfShell+1)<4  && GridSystem[colIndexOfShell+1][rowIndexOfShell] != null)
        {
            if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell+1][rowIndexOfShell].name))
            {
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            }//looking for next col & same row

            if (GridSystem[colIndexOfShell+1][rowIndexOfShell-1] != null)
            {
                if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell+1][rowIndexOfShell-1].name))
                {
                    Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                }//looking for next col & same row
            }
        }

        if ((colIndexOfShell-1 >= 0) &&  GridSystem[colIndexOfShell - 1 ][rowIndexOfShell] != null)
        {
            if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell - 1][rowIndexOfShell].name))
            {
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            }//looking for previous col & same row

            if ((rowIndexOfShell-1)>= 0 && (colIndexOfShell-1)>=0 && GridSystem[colIndexOfShell - 1][rowIndexOfShell-1] != null)
            {
                if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell - 1][rowIndexOfShell-1].name))
                {
                    Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                }//looking for previous col & same row
            }
          
        }
       
    }

    void GenerateGrid()
    {
        int rows = 4;
        int columns = 6;
        float cellSize = 0f;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate position for the cell
                float offset = (row % 2 == 0) ? 0 : 0.5f; 
                //int offset = 0;
                Vector3 cellPosition = new Vector3((-(row*0.89f)), 0f, (row * cellSize+col+offset)-3f);
                // Instantiate the grid cell at the calculated position
                GameObject gridCell = Instantiate(gridCellPrefab, cellPosition, transform.rotation) as GameObject;
                gridCell.transform.rotation = Quaternion.Euler(0,-90,0);
                // Set parent to this GameObject for organizational purposes
                gridCell.transform.SetParent(transform);
                TileContainer[row][col] = gridCell;
                //gameObject.transform.rotation = Quaternion.Euler(-48,0,0);
            }
        }
    }
}
