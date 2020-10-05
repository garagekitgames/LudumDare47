using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterialController : MonoBehaviour
{
    public Renderer myMesh;

    public Material blinkMaterial;
    public Color highlightColor;
    public Material electricMaterial;

    private EnemyCollisionCheck myCollisionCheck;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        myCollisionCheck = GetComponent<EnemyCollisionCheck>();
        defaultColor = myMesh.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(myCollisionCheck.isWrapped)
        {
            myMesh.material.color = highlightColor;
        }
        else
        {
            myMesh.material.color = defaultColor;
        }
    }

    public void Electrocute()
    {
        Material[] matArray = new Material[2];
        matArray[0] = blinkMaterial;
        matArray[1] = electricMaterial;
        myMesh.materials = matArray;
    }

    public void Highlight()
    {

    }
}
