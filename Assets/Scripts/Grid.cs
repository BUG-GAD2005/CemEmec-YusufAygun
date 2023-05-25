using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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


    void Start()
    {
        TheGrid = new CellStruct[TotalColumns, TotalRows];
        StartPos.x=OriginalCellObject.transform.position.x; ;
        StartPos.y=OriginalCellObject.transform.position.y;
        CreateGrid();
        Destroy(OriginalCellObject);

        
    }
                  
    void Update()
    {
        
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
    
    
    void FillCell(Vector2Int pos)
    {
        TheGrid[pos.y, pos.x].Filled = true;
        TheGrid[pos.y, pos.x].SetImage();
    }

    public bool IsEmpty(Vector2Int posvec)
    {
        return !TheGrid[posvec.y, posvec.x].Filled;
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
