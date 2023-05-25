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
    float GapBetweenCells = 0.9f;   
    

    void Start()
    {
        TheGrid = new CellStruct[TotalColumns, TotalRows];
        StartPos.x=OriginalCellObject.transform.position.x; ;
        StartPos.y=OriginalCellObject.transform.position.y;
        CreateGrid();
        Destroy(OriginalCellObject);

        Deneme();
    }

    void Deneme()
    {
        TheGrid[1, 1].Filled = true;
        TheGrid[1, 1].SetImage();
    }
    
    void Update()
    {
        
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
                TheGrid[i, k].CellActor.transform.parent=this.transform;   
                TheGrid[i, k].CellActor.transform.localScale= OriginalCellObject.transform.localScale;
            }
        }
    }

   
}
