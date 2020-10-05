using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
using SO;
using WrappingRopeLibrary.Scripts;

public class RopeMaterialController : MonoBehaviour
{
    public MeshRenderer[] pieceMeshRenderers;
    public Material defaultMateriial;
    public Material electricMaterial;

    public BoolVariable isRopeInvalid;
    public RopePieceRuntimeSet ropePieceRuntimeSet;
    public Rope rope;
    private int obstacleWrappedCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        obstacleWrappedCount = 0;
        foreach (var item in ropePieceRuntimeSet.Items)
        {
            if(item.isWrappedOnObstacle)
            {
                obstacleWrappedCount++;
            }
        }
        if(obstacleWrappedCount >= 1)
        {
            isRopeInvalid.value = true;
        }
        else
        {
            isRopeInvalid.value = false;
        }
    }

    public void Electrocute()
    {
        pieceMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var item in pieceMeshRenderers)
        {
            Material[] matArray = new Material[2];
            matArray[0] = item.material;
            matArray[1] = electricMaterial;
            item.materials = matArray;
        }
    }
}
