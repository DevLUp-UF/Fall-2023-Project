using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float spawnTimer;
    [SerializeField]
    SpawnWeights weights;
    bool isWaiting = false;
    void OnEnable()
    {
        StartCoroutine(WaitTillNextSpawn());
    }

    private void Update()
    {
        if (!isWaiting && !SpawnManager.Instance.IsActiveWave())
        {
            Debug.Log("Start Waiting");
            StopAllCoroutines();
            isWaiting = true;
        }
        else if(isWaiting && SpawnManager.Instance.IsActiveWave())
        {
            Debug.Log("End Waiting");
            StartCoroutine(WaitTillNextSpawn());
            isWaiting=false;
        }
    }

    public void SpawnEnemy()
    {
        var temp = weights.GetRandomEnemy();
        GameObject enemy = Instantiate(temp);
        enemy.transform.position = transform.position;
        SpawnManager.Instance.ReportSpawn();
    }

    IEnumerator WaitTillNextSpawn()
    {
        Debug.Log("Waiting till Next Spawn");
        yield return new WaitForSeconds(spawnTimer);
        if (SpawnManager.Instance.IsActiveWave() && !isWaiting)
        {
            Debug.Log("Spawning...");
            SpawnEnemy();
            StartCoroutine(WaitTillNextSpawn());
            Debug.Log("Spawned!");
        }
    }
}
