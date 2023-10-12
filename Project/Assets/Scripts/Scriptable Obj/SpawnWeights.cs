using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpawnWeights", menuName = "ScriptableObjects/SpawnWeights", order = 1)]
public class SpawnWeights : ScriptableObject
{
    [System.Serializable]
    public struct SpawnObject
    {
        public int minWave;
        public GameObject prefab;
    }

    [SerializeField]
    private List<SpawnObject> spawnList;

    // Returns a random music object
    public GameObject GetRandomEnemy() 
    {
        int index;
        while(true)
        {
            index = Random.Range(0, spawnList.Count);
            if (spawnList[index].minWave <= SpawnManager.Instance.GetWaveNum())
                break;
        }
        return spawnList[index].prefab;
    }
}
