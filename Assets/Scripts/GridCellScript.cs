using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ImageEmpty;
    public GameObject ImageFilled;
    
    void Start()
    {
        ImageEmpty = transform.Find("ImageEmpty").gameObject;
        ImageFilled = transform.Find("ImageFilled").gameObject;
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
            ImageFilled.SetActive(true);
            ImageEmpty.SetActive(false);
        }
        else
        {
            ImageFilled.SetActive(false);
            ImageEmpty.SetActive(true);
        }
    }
}
