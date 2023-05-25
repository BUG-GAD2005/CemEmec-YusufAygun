using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBlocks : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public Transform parentAfterDrag;
    [SerializeField] private BlocksEnum blockType;
    private GridCellScript gridCellScript;
    private GameObject gridObject;
    private Grid gridScript;

    [SerializeField] Vector2Int[] OtherBlock; // bloklar böyle yapýlcak yeni bu yusuf düzenle bunu


    private void Start()
    {
        gridObject = GameObject.FindGameObjectWithTag("Grid");
        gridScript = gridObject.GetComponent<Grid>();
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

        if(blockType== BlocksEnum._11) 
        {
            if (!gridCellScript.isFilled)
            {
                gridCellScript.SetImageFill(true);
            }
            else
            {
                //return piece to start
            }
        }

        else if (blockType == BlocksEnum._22)
        {
            if (!gridCellScript.isFilled)
            {
                if (!gridScript.GetCellCoBool(gridCellScript.CellNum.x, gridCellScript.CellNum.y++)) 
                {
                    if(!gridScript.GetCellCoBool(gridCellScript.CellNum.x++, gridCellScript.CellNum.y)) 
                    {
                        if (!gridScript.GetCellCoBool(gridCellScript.CellNum.x++, gridCellScript.CellNum.y++)) 
                        {
                            Debug.Log(gridCellScript.CellNum);
                            gridCellScript.SetImageFill(true);
                            gridScript.SetImageFill(gridCellScript.CellNum.x, gridCellScript.CellNum.y++, true);
                            gridScript.SetImageFill(gridCellScript.CellNum.x++, gridCellScript.CellNum.y, true);
                            gridScript.SetImageFill(gridCellScript.CellNum.x++, gridCellScript.CellNum.y++, true);
                        }
                    }
                }
            }
            else
            {
                //return piece to start
            }
        }
        Destroy(this.gameObject);
        
    }
}
