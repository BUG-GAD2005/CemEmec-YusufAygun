using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct CellStruct
{
    public int Row;
    public int Col;
    public bool Filled;
    public GameObject CellActor;

    public void SetImage()
    {
        CellActor.GetComponent<GridCellScript>().SetImageFill(Filled);
    }
}
public class Grid : MonoBehaviour
{
    int TotalColumns=10;
    int TotalRows = 10;
    [SerializeField] GameObject OriginalCellObject;// gets cloned and determines where first cell will be then gets deleted
    Vector2 StartPos=new Vector2(0,0);
    
    CellStruct[,] TheGrid;
    public float GapBetweenCells = 0.9f;

  
    [SerializeField] SpawnBlocks SpawnerScript;

    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Text scoreText;
    int score;
    private bool isGameEnd = false;

    
    void Start()
    {
        TheGrid = new CellStruct[TotalColumns, TotalRows];
        StartPos.x=OriginalCellObject.transform.position.x; ;
        StartPos.y=OriginalCellObject.transform.position.y;
        CreateGrid();
        Destroy(OriginalCellObject);

        scoreText.text = "Score: ";
    }
                  
    void Update()
    {
        if (isGameEnd) 
        {
            if (Input.GetKeyDown(KeyCode.R)) 
            {
                Debug.Log("Restart");
                SceneManager.LoadScene("TheGame", LoadSceneMode.Single);
            }
        }
    }

    public bool TryToPlace(Vector2Int[] WantedCells)
    {
        bool CanPlaced = true;
        foreach (Vector2Int ACell in WantedCells)
        {
            if (ACell.x >= TotalColumns || ACell.x < 0 || ACell.y >= TotalRows || ACell.y < 0)
            {
                CanPlaced=false; 
                break;
            }
            CanPlaced = IsEmpty(ACell);
            if (!CanPlaced)
            {
                break;
            }
        }

        if (CanPlaced)
        {
            PlaceOnGrid(WantedCells);
            ControlFull();
           
        }
        
        return CanPlaced;
    }

    void PlaceOnGrid(Vector2Int[] Cells)
    {
        foreach (Vector2Int ACell in Cells)
        {
            FillCell(ACell);
        }
    }
    
    void EndGame()
    {
        Debug.Log("GameOver");
        isGameEnd = true;
        gameOverText.SetActive(true);
    }
    public bool IsGameEnd()
    {
        bool CanPut;
        List<Vector2Int[]> BLockCells = new List<Vector2Int[]>();

        foreach (GameObject nesne in SpawnerScript.RemainingObjects())
        {

            BLockCells.Add(nesne.GetComponent<DragBlocks>().AllBlocks);

        }

        foreach (Vector2Int[] Blocklar in BLockCells)
        {
            for (int i = 0; i < TotalRows; i++)
            {
                for (int j = 0; j < TotalColumns; j++)
                {
                    CanPut = true;
                    foreach (Vector2Int ACell in Blocklar)
                    {
                        if (!IsEmpty(new Vector2Int( ACell.y + j, ACell.x + i)))
                        {
                            CanPut = false;
                            break;
                        }
                    }
                    if (CanPut)
                    {
                        
                        return false;
                    }
                }
            }
        }

            
            
        EndGame();
        return true;
    }


    


    
    
    void FillCell(Vector2Int pos)
    {
        TheGrid[pos.y, pos.x].Filled = true;
        TheGrid[pos.y, pos.x].SetImage();
    }

    void EmptyCell(Vector2Int pos)
    {
        TheGrid[pos.y, pos.x].Filled = false;
        TheGrid[pos.y, pos.x].SetImage();
    }

    public bool IsEmpty(Vector2Int posvec)
    {
        if (posvec.x < 0 || posvec.x >= TotalRows || posvec.y < 0 || posvec.y >= TotalColumns)
            return false;
        return !TheGrid[posvec.y, posvec.x].Filled;
    }

    void ControlFull()
    {
        List<int> RowsToDelete = new List<int>();
        List<int> ColumnsToDelete= new List<int>();

        for(int i=0;i<TotalColumns;i++)
        {
            if(IsFullrow(i))
                RowsToDelete.Add(i);
        }

        for (int i = 0; i < TotalColumns; i++)
        {
            if (IsFullColumn(i))
                ColumnsToDelete.Add(i);
        }

        foreach (int i in RowsToDelete)
        {
            for(int j = 0; j< TotalColumns; j++)
            {
                EmptyCell(new Vector2Int(j, i));
            }
            score++;
        }
        foreach (int i in ColumnsToDelete)
        {
            for (int j = 0; j < TotalRows; j++)
            {
                EmptyCell(new Vector2Int(i, j));
            }
            score++;
        }
        scoreText.text = "Score: " + score;
    }
    bool IsFullrow(int row)
    {
        for(int i=0; i<TotalRows; i++)
        {
            if (!TheGrid[row, i].Filled)
                return false;
        }
        return true;
    }
    bool IsFullColumn(int row)
    {
        for (int i = 0; i < TotalColumns; i++)
        {
            if (!TheGrid[i, row].Filled)
                return false;
        }
        return true;
    }
    void CreateGrid()
    {
    
        
        for(int i=0; i<TotalRows;i++)
        {
            
            for(int k=0; k<TotalColumns;k++)
            {
                TheGrid[i, k].Row = i;
                TheGrid[i, k].Col = k;
                TheGrid[i, k].Filled = false;
                TheGrid[i, k].CellActor = Instantiate(OriginalCellObject);
               
                TheGrid[i, k].CellActor.transform.localPosition = new Vector2(StartPos.x + GapBetweenCells * i, StartPos.y + GapBetweenCells * k);
                //TheGrid[i, k].CellActor.transform.parent=this.transform;   
                TheGrid[i, k].CellActor.transform.SetParent(this.transform);
                TheGrid[i, k].CellActor.transform.localScale= OriginalCellObject.transform.localScale;
                TheGrid[i,k].CellActor.GetComponent<GridCellScript>().CellNum= new Vector2Int( k, i);
            }
        }
    }



}
