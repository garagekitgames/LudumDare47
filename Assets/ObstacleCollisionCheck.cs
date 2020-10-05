using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA;
using SO;

public class ObstacleCollisionCheck : MonoBehaviour
{

    public int wrapCount;
    public bool isWrapped;

    public BoolVariable isRopeInvalid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piece"))
        {
            isWrapped = true;
            other.GetComponent<PieceController>().isWrappedOnObstacle = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Triggered By : "+other.name);
        // myMeshRenderer.material = highLightMaterail;
        if (other.CompareTag("Piece"))
        {
            isWrapped = true;
            other.GetComponent<PieceController>().isWrappedOnObstacle = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        // myMeshRenderer.material = defaultMaterial;
        if (other.CompareTag("Piece"))
        {
            isWrapped = false;
            other.GetComponent<PieceController>().isWrappedOnObstacle = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (isWrapped)
        //{
        //    isRopeInvalid.value = true;
        //}
        //else
        //{
        //    isRopeInvalid.value = false;
        //}
    }
}
