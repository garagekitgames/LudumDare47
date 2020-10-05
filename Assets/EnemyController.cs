using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyRuntimeSet enemyRuntimeSet;

    public EnemyCollisionCheck myCollisionCheck;

    public GameObject bulletPrefab;

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
        enemyRuntimeSet.Add(this);
        isAlive = true;
    }

    private void OnDisable()
    {
        enemyRuntimeSet.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        myCollisionCheck = GetComponent<EnemyCollisionCheck>();
    }

    public void Electrocute()
    {
        isAlive = false;
    }

    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(transform.forward);
        
        
    }
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
