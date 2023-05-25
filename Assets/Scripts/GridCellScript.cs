using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellScript : MonoBehaviour
{
    // Start is called before the first frame update
     GameObject ImageEmpty;
     GameObject ImageFilled;

    public bool isFilled = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImageFill(bool a)
    {
        ImageEmpty = transform.Find("ImageEmpty").gameObject;
        ImageFilled = transform.Find("ImageFilled").gameObject;
        if (a)
        {
            isFilled = true;
            ImageFilled.SetActive(true);
            ImageEmpty.SetActive(false);
        }
        else
        {
            isFilled = false;
            ImageFilled.SetActive(false);
            ImageEmpty.SetActive(true);
        }
    }
}
