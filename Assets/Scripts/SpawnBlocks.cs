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
        blockTop.transform.SetParent(spawnGridTop.transform);
        blockMid.transform.SetParent(spawnGridMid.transform);
        blockBottom.transform.SetParent(spawnGridBottom.transform);
    }
}
