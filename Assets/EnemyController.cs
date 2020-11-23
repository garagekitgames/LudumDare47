using BehaviorDesigner.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyRuntimeSet enemyRuntimeSet;

    public EnemyCollisionCheck myCollisionCheck;

    public GameObject bulletPrefab;

    public Transform[] muzzles;

    public BehaviorTree myBehaviorTree;

    public EnemyLaserSightController myLaserController;

    public GameObject test;
    public enum EnemyState
    {
        IDLE,
        FIRE,
        DIE,
        DEAD
    }


    

    public EnemyState enemyState = EnemyState.IDLE;

    public bool isAlive;
    public Transform target;
    private void OnEnable()
    {
        myBehaviorTree = GetComponent<BehaviorTree>();
        //myBehaviorTree.RegisterEvent<object>("StartLaserAim", StartLaserAim);
        myBehaviorTree.RegisterEvent("StartLaserFire", StartLaserFire);
        myBehaviorTree.RegisterEvent("StartLaserCharge", StartLaserCharge);
        myBehaviorTree.RegisterEvent("StopLaser", StopLaser);
        myBehaviorTree.RegisterEvent("StartLaserAim", StartLaserAim);
        enemyRuntimeSet.Add(this);
        isAlive = true;
    }

    private void StopLaser()
    {
        myLaserController.StopLaser();
        Debug.Log("StopLaser");
    }

    private void StartLaserCharge()
    {
        myLaserController.StartLaserCharge();
        Debug.Log("StartLaserCharge");
    }

    private void StartLaserFire()
    {
        myLaserController.StartLaserFire();
        Debug.Log("StartLaserCharge");
    }

    private void StartLaserAim()
    {
        myLaserController.StartLaserAim();
        Debug.Log("StartLaserAim");
    }

   

    private void OnDisable()
    {
        myBehaviorTree.UnregisterEvent("StartLaserFire", StartLaserFire);
        myBehaviorTree.UnregisterEvent("StartLaserAim", StartLaserAim);
        myBehaviorTree.UnregisterEvent("StartLaserCharge", StartLaserCharge);
        myBehaviorTree.UnregisterEvent("StopLaser", StopLaser);
       
        enemyRuntimeSet.Remove(this);
    }


    // Start is called before the first frame update
    void Start()
    {
        var myEnemyController = (SharedGameObject)myBehaviorTree.GetVariable("myEnemyController");
        test = myEnemyController.Value;
        myEnemyController.Value = this.gameObject;
        myCollisionCheck = GetComponent<EnemyCollisionCheck>();
        myLaserController = GetComponent<EnemyLaserSightController>();
    }

    public void Electrocute()
    {
        isAlive = false;
    }

    public void Fire()
    {
        foreach (var item in muzzles)
        {
            var bullet = Instantiate(bulletPrefab, item.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(item.forward);
        }
        
        
        
    }

    public void Charge()
    {

    }

    //IEnumerator ChargeShot()
    //{

    //}
    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.FIRE:
                break;
            case EnemyState.DIE:
                break;
            case EnemyState.DEAD:
                break;
            default:
                break;
        }
    }
}
