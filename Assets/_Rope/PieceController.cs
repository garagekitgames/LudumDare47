using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

[Serializable]
public class PieceController : MonoBehaviour
{
    public RopePieceRuntimeSet ropePieceRuntimeSet;
    public BoolVariable isRopeInvalid;
    //public int wrapCount;
    public bool isWrappedOnObstacle;
    public MeshRenderer myMesh;
    public Color inValidColor = Color.red;
    private Color initColor;
    private void OnEnable()
    {
        ropePieceRuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        ropePieceRuntimeSet.Remove(this);
    }

    private void OnDestroy()
    {
        ropePieceRuntimeSet.Remove(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
        initColor = myMesh.sharedMaterial.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRopeInvalid.value)
        {
            myMesh.sharedMaterial.color = inValidColor;
        }
        else
        {
            myMesh.sharedMaterial.color = initColor;
        }
    }
}
