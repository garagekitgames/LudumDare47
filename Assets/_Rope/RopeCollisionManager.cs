using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WrappingRopeLibrary.Enums;
using WrappingRopeLibrary.Events;
using WrappingRopeLibrary.Scripts;

public class RopeCollisionManager : MonoBehaviour
{

    public GameObject RopeInstance;
    private Rope _sourceRope;
    public GameObject wrapPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (RopeInstance != null)
        {
            _sourceRope = RopeInstance.GetComponent<Rope>();
            _sourceRope.ObjectWrap += Rope_ObjectWrapping;
            //_sourceRope.


        }
    }

    public void Rope_ObjectWrapping(RopeBase sender, ObjectWrapEventArgs args)
    {
        //Debug.Log(args.Target.name);
        

        foreach (var item in args.WrapPoints)
        {
            //var temp = Instantiate(wrapPoints, item, Quaternion.identity);
            //Debug.Log(item.);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
