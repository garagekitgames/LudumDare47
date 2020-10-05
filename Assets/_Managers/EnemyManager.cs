using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{

    public EnemyRuntimeSet enemyRuntimeSet;
    private int enemiesWrappedCount;
    public UnityEvent OnGameWon;
    bool gameWonFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemiesWrappedCount = 0;
        foreach (var item in enemyRuntimeSet.Items)
        {
            if(item.myCollisionCheck.isWrapped)
            {
                enemiesWrappedCount++;
            }
        }
        if(enemiesWrappedCount >= enemyRuntimeSet.Items.Count)
        {
            //Debug.Log("Game Won !");
            if(!gameWonFlag)
            {
                StartCoroutine(GameWon(0.25f));
                gameWonFlag = true;
            }
        }
    }

    IEnumerator GameWon(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnGameWon.Invoke();

    }
}
