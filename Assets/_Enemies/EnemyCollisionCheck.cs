using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

public class EnemyCollisionCheck : MonoBehaviour
{
    //public MeshRenderer myMeshRenderer;
    //public Material defaultMaterial;
    //public Material highLightMaterail;

    public int wrapCount;
    public bool isWrapped;
    public BoolVariable isRopeInvalid;

    // Start is called before the first frame update
    void Start()
    {
        //myMeshRenderer = GetComponent<MeshRenderer>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
        //if(isWrapped)
        //{
        //    myMeshRenderer.material = highLightMaterail;
        //}
        //else
        //{
        //    myMeshRenderer.material = defaultMaterial;
        //}
        //isWrapped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("Collided");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isRopeInvalid.value)
        {
            if (other.CompareTag("Piece"))
            {
                isWrapped = true;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Triggered By : "+other.name);
        // myMeshRenderer.material = highLightMaterail;
        if (!isRopeInvalid.value)
        {
            if (other.CompareTag("Piece"))
            {
                isWrapped = true;
            }
        }
            
       
        
    }

    private void OnTriggerExit(Collider other)
    {
        // myMeshRenderer.material = defaultMaterial;
        if (!isRopeInvalid.value)
        {
            if (other.CompareTag("Piece"))
            {
                isWrapped = false;
            }
        }
    }
            
}
