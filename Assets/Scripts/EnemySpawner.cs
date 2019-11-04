using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    bool isLastWave = false;
    bool isLastEnemy = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    void Update()
    {
        if (isLastEnemy == true)
        {
            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemyList.Length <= 0)
            {
                FindObjectOfType<Level>().NextLevel();
            }
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            if (waveIndex == waveConfigs.Count - 1)
                isLastWave =  true;

            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            if (isLastWave == true && enemyCount == waveConfig.GetNumberOfEnemies() - 1)
                isLastEnemy = true;
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
