using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlocks : MonoBehaviour
{
    [SerializeField] private GameObject spawnGridTop;
    [SerializeField] private GameObject spawnGridMid;
    [SerializeField] private GameObject spawnGridBottom;

    [SerializeField] List<GameObject> prefabs;

    private int prefabIndexTop;
    private int prefabIndexMid;
    private int prefabIndexBottom;
    private GameObject blockTop;
    private GameObject blockMid;
    private GameObject blockBottom;

     public int spawnCheck = 0;

    void Start()
    {
        SpawnThreeBlocks();
    }

    public void SpawnThreeBlocks() 
    {
        prefabIndexTop = Random.Range(0, prefabs.Count);
        prefabIndexMid = Random.Range(0, prefabs.Count);
        prefabIndexBottom = Random.Range(0, prefabs.Count);

        blockTop = Instantiate(prefabs[prefabIndexTop], spawnGridTop.transform);
        blockMid = Instantiate(prefabs[prefabIndexMid], spawnGridMid.transform);
        blockBottom = Instantiate(prefabs[prefabIndexBottom], spawnGridBottom.transform);
    }

    public void ReturnBlocks() 
    {
        if (blockTop != null) 
            blockTop.transform.SetParent(spawnGridTop.transform);
        if (blockMid != null) 
            blockMid.transform.SetParent(spawnGridMid.transform);
        if (blockBottom != null) 
            blockBottom.transform.SetParent(spawnGridBottom.transform);
    }

    public void Forget(GameObject anobject)
    {
        if (blockBottom == anobject)
            blockBottom = null;

        if(blockTop == anobject)   
            blockTop = null;
        if (blockMid == anobject)
            blockMid = null;
    }
    public List<GameObject> RemainingObjects()
    {
        List<GameObject> ObjectList = new List<GameObject>();
        if(blockBottom!=null) 
            ObjectList.Add(blockBottom);
        if(blockMid!=null)
            ObjectList.Add(blockMid);
        if(blockTop!=null)
            ObjectList.Add(blockTop);
        
        return ObjectList;
    }
}
