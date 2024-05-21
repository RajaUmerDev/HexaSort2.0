using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Grid : MonoBehaviour, IGrid
{ 
    [SerializeField] List<List<GameObject>> GridSystem = new List<List<GameObject>>();
    [SerializeField] List<List<GameObject>> TileContainer = new List<List<GameObject>>();
    [SerializeField] private Tile theTile;
    public GameObject gridCellPrefab;
    [SerializeField] private GridTypes _gridTypes;
    private int[] _gridSize = new int[2];

    private void Start()
    {
        GenerateGrid();
        //theTile.SetDependency(this);
    }
    
    private void InitializeGrid(int cols, int rows,List<List<GameObject>> twoDArray)
    {
        //GameObject nullContainer = new GameObject("InitObjBox");
        for (int i = 0; i < cols; i++)
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < rows; j++)
            {
                row.Add(null);
//                GameObject emptyObject = new GameObject("Empty");
//                emptyObject.transform.SetParent(nullContainer.transform);
//                row.Add(emptyObject);
            }
            twoDArray.Add(row);
        }
    }

     void IGrid.AddSelectedObjectInGrid(GameObject tile,GameObject pickedObject)
    {
        int cols = _gridSize[0], rows = _gridSize[1];
        int plotColumnIndex=-1, plotRowIndex=-1;
        bool isIndexFound = false;
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (TileContainer[i][j] == tile)
                {
                    plotColumnIndex = i;
                    plotRowIndex = j;
                    isIndexFound = true;
                    break;
                }
            }
        }

        if (isIndexFound)
        {
            Debug.Log("-> "+plotColumnIndex +" | " +plotRowIndex);
            GridSystem[plotColumnIndex][plotRowIndex] = pickedObject;
            CheckForAdjacentTile(plotColumnIndex,plotRowIndex,rows,cols);   
        }


    }

     private void CheckForAdjacent(int colIndexOfShell,int rowIndexOfShell,int maxRowSize, int maxColSize)
    {
        if ((rowIndexOfShell + 1)<maxRowSize && GridSystem[colIndexOfShell][rowIndexOfShell+ 1] != null)
        {
            if(GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell][rowIndexOfShell+ 1].name){
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            }
        }// looking at one next row index

        if((rowIndexOfShell-1)>= 0 && GridSystem[colIndexOfShell][rowIndexOfShell + -1] != null)
        {
            if (GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell][rowIndexOfShell + -1].name)
            {
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            }// looking for one previous row index
        }

        if ((colIndexOfShell + 1) < maxColSize && GridSystem[colIndexOfShell + 1][rowIndexOfShell] != null)
        {
            if ((GridSystem[colIndexOfShell][rowIndexOfShell].name ==
                 GridSystem[colIndexOfShell + 1][rowIndexOfShell].name))
            {
                Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
            } //looking for next col & same row
        }
        if((colIndexOfShell + 1) < maxColSize && (rowIndexOfShell-1 >=0))
        {
            if (GridSystem[colIndexOfShell + 1][rowIndexOfShell - 1] != null)
            {
                if (GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell + 1][rowIndexOfShell - 1].name)
                {
                    Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                } //looking for next col & previous row
            }

            if((rowIndexOfShell+1)<maxRowSize &&  GridSystem[colIndexOfShell+1][rowIndexOfShell+1] != null){
                if ((rowIndexOfShell+1)<maxRowSize && GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell+1][rowIndexOfShell+1].name)
                {
                    Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                }//looking for next col & next row
            }
        }

        if ((colIndexOfShell-1 >= 0))
        {
            if (GridSystem[colIndexOfShell - 1 ][rowIndexOfShell] != null)
            {
                if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell - 1][rowIndexOfShell].name))
                {
                    Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                }//looking for previous col & same row
            }

            if ((rowIndexOfShell-1)>= 0 && (colIndexOfShell-1)>=0)
            {    
                Debug.Log("PP");
                if (GridSystem[colIndexOfShell - 1][rowIndexOfShell-1] != null)
                {
                    if ((GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell - 1][rowIndexOfShell-1].name))
                    {
                        Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                    }//looking for previous col & previus row
                }

                if ( GridSystem[colIndexOfShell - 1][rowIndexOfShell+1] != null)
                {
                    if ((rowIndexOfShell+1)<maxRowSize  && (GridSystem[colIndexOfShell][rowIndexOfShell].name == GridSystem[colIndexOfShell - 1][rowIndexOfShell+1].name)) 
                    {
                        Debug.Log(GridSystem[colIndexOfShell][rowIndexOfShell].name);
                    }//looking for previous col & next row
                }
               
            }
          
        }
       
    }



//    void CheckForAdjacentTile(int tileColIndex,int tileRowIndex,int TileMaxRowSize, int TileMaxColSize)
//    {
//        int tileNextColIndex = tileColIndex + 1; int tileNextRowIndex = tileRowIndex + 1;
//        int tilePreviousColIndex = tileColIndex - 1; int tilePreviousRowIndex = tileRowIndex - 1;
//
//
//        for (int i = 0; i < TileMaxColSize; i++)
//        {
//            for (int j = 0; j < TileMaxRowSize; j++)
//            {
//                // same column
//                if(GridSystem[tileColIndex][tileNextRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//                if (GridSystem[tileColIndex][tilePreviousRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//
//
//                // next column
//                if (GridSystem[tileNextColIndex][tileNextRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//                if (GridSystem[tileNextColIndex][tilePreviousRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//                if (GridSystem[tileNextColIndex][tileColIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//
//
//                // previous column
//                if (GridSystem[tilePreviousColIndex][tileNextRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//                if (GridSystem[tilePreviousColIndex][tilePreviousRowIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//                if (GridSystem[tilePreviousColIndex][tileColIndex] == GridSystem[tileColIndex][tileRowIndex])
//                {
//                    Debug.Log(GridSystem[tileColIndex][tileRowIndex].name);
//                }
//            }
//        }
//    }


    void CheckForAdjacentTile(int tileColIndex, int tileRowIndex, int TileMaxRowSize, int TileMaxColSize)
         {
             for (int i = 0; i < TileMaxColSize; i++)
             {
                 for (int j = 0; j < TileMaxRowSize; j++)
                 {
                     if ((tileRowIndex + 1 <TileMaxRowSize && i == tileColIndex && j == tileRowIndex + 1) || (tileRowIndex - 1 >=0 && i == tileColIndex && j == tileRowIndex - 1) ||
                         (tileColIndex+1 < TileMaxColSize && i == tileColIndex + 1 && j == tileRowIndex) || 
                         ((tileColIndex + 1 < TileMaxColSize && tileRowIndex + 1 <TileMaxRowSize) && i == tileColIndex + 1 && j == tileRowIndex + 1) ||
                         ((tileColIndex + 1 < TileMaxColSize && tileRowIndex - 1 >=0) && i == tileColIndex + 1 && j == tileRowIndex - 1) ||
                         (tileColIndex-1 >=0 && i == tileColIndex - 1 && j == tileRowIndex) || ((tileColIndex - 1 >= 0 && tileRowIndex+1<TileMaxRowSize) && i == tileColIndex - 1 && j == tileRowIndex + 1) ||
                         ((tileColIndex - 1 >= 0 && tileRowIndex-1>=0) && i == tileColIndex - 1 && j == tileRowIndex - 1))
                     {
                         if (GridSystem[i][j] != null)
                         {
                             if (GridSystem[i][j].name == GridSystem[tileColIndex][tileRowIndex].name)
                             {
                                 Debug.Log(GridSystem[tileColIndex][tileRowIndex].name +"at "+i+"|" +j);
                             }
                         }
                     }
                 }
             }
         }

     private void GenerateGrid()
     {
         switch (_gridTypes.typeOfGrid)
         {
             case GridTypes.Grid.Centroid:
                GenerateCentroidGrid();
                 break;
             case GridTypes.Grid.Trigonal:
                 GenerateTrigonalGrid();
                 break;
             case GridTypes.Grid.None:
                 // do nothing
                 break;
         }
     }

    void GenerateTrigonalGrid()
    {    
        InitializeGrid(4, 6,GridSystem);
        InitializeGrid(4, 6,TileContainer);
        int rows = 4;
        int columns = 6;
        _gridSize[0] = rows;
        _gridSize[1] = columns;
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

    void GenerateCentroidGrid()
    {    
        InitializeGrid(6, 5,GridSystem);
        InitializeGrid(6, 5,TileContainer);
        int rows = 5;
        int columns = 3;
        _gridSize[0] = 6;
        _gridSize[1] = 5;
        float offset=0;
        for (int row = 0; row < rows; row++)
        {
            if (row == 2)
            {
                columns = 5;
                offset = -0.5f;
            }
            else if(row != 2)
            {
                if (row % 2 == 0)
                {
                    columns = 3;
                }
                else
                {
                    columns = 4;
                }
            }
            for (int col = 0; col < columns; col++)
            {
                if (row != 2)
                {
                    offset = (row % 2 == 0) ? 0.5f : 0; 
                }
                Vector3 cellPosition = new Vector3((-(row*0.89f))+0.5f, 0f, (col+offset)-1.7f);
                // Instantiate the grid cell at the calculated position
                GameObject gridCell = Instantiate(gridCellPrefab, cellPosition, transform.rotation) as GameObject;
                gridCell.transform.rotation = Quaternion.Euler(0, -90, 0);
                // Set parent to this GameObject for organizational purposes
                gridCell.transform.SetParent(transform);
                TileContainer[row][col] = gridCell;
                //gameObject.transform.rotation = Quaternion.Euler(-48,0,0);
            }
        }
    }
}
