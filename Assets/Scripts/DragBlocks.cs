using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBlocks : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    private GameObject BlockImage;
    private GridCellScript gridCellScript;
    private GameObject gridObject;
    private Grid gridScript;
    private Vector3 StartPosition=new Vector3(0,0,0);
    private float GapBetweenBlocks;

    [SerializeField] Vector2Int[] AllBlocks; // bloklar böyle yapýlcak yeni bu yusuf düzenle bunu


    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid");
        gridScript = gridObject.GetComponent<Grid>();

        //CreateBlockImage();
        //StartPosition = transform.localPosition;
    }

    private void CreateBlockImage()
    {
        BlockImage = transform.Find("Block").gameObject;
        GapBetweenBlocks = gridScript.GapBetweenCells;
        

        for (int i = 0; i < AllBlocks.Length; i++)
        {
            GameObject obje = Instantiate(BlockImage);
            Debug.Log("yaz");
            Vector3 objepos = transform.position;
            objepos += new Vector3(AllBlocks[i].y, AllBlocks[i].x) * GapBetweenBlocks;
            obje.transform.localPosition = objepos;
            obje.transform.parent = this.transform;
            obje.transform.localScale = BlockImage.transform.localScale;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);// burda bu gridi baba belirle
        image.raycastTarget = true;
        gridCellScript = GetComponentInParent<GridCellScript>();//bizim gridin scripti burda burdan grid numarasýný al pivotla eþitle
        Vector2Int OriginPosOnTheGrid = gridCellScript.CellNum;
        Vector2Int[] BlockLocations=new Vector2Int[AllBlocks.Length];
        
        for(int i =0;i<AllBlocks.Length; i++)
        {
            

            BlockLocations[i].x = AllBlocks[i].y + OriginPosOnTheGrid.x;
            BlockLocations[i].y = AllBlocks[i].x + OriginPosOnTheGrid.y;
        }

        if (gridScript.TryToPlace(BlockLocations)) // trytoplace baþarýlý ise grid scripti denk gelen yerleri fill eder
        {
            Destroy(this.gameObject);
            
        }
        else
        {
            
            //transform.localPosition=StartPosition;
            //return pice to start
        }
        
        
    }
}
