using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonBehaviour<SpawnManager>
{
    [SerializeField]
    private float waveDelay;
    [SerializeField]
    private float delayLessFactor;
    [SerializeField]
    private int enemiesAddedEachWave;

    private int waveNum;
    private int currSpawnsLeft;
    private bool activeWave = false;

    private void StartWave(int count)
    {
        waveNum = count;
        currSpawnsLeft = 1 + count * enemiesAddedEachWave;
        activeWave = true;
        Debug.Log($"Starting Wave #{waveNum} and spawning {currSpawnsLeft} enemies!");
    }

    private void Update()
    {
        if(activeWave && currSpawnsLeft <= 0)
        {
            activeWave = false;
            StartCoroutine(CountDownToNextWave());
        }
    }

    public int GetWaveNum() { return waveNum; }

    public bool IsActiveWave() { return activeWave; }

    public void ReportSpawn() { currSpawnsLeft--; }

    public void StartGame()
    {
        StartWave(waveNum);
    }

    IEnumerator CountDownToNextWave()
    {
        float time = waveDelay - waveNum * delayLessFactor;
        yield return new WaitForSeconds((time < 0 ? 1 : time));
        StartWave(++waveNum);
    }
}
