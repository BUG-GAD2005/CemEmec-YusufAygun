using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchParrentCell : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragBlocks dragBlocks = dropped.GetComponent<DragBlocks>();
        dragBlocks.parentAfterDrag = transform;
    }
}
