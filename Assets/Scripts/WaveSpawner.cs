using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
	public GameObject[] EnemyPrefabs;
	public float SpawnTime = 1;
    public int MinAmount = 1;
    public int MaxAmount = 3;
    public float WaveDuration;
    public int IncreaseAmountToCreate;
}

public class WaveSpawner : MonoBehaviour {

    public Wave[] Waves;
    public float MaxPositionOffset = 3f;
    public float MinPositionOffset = -3f;
    public int _waveIndex = 0;

    void Start()
    {
        StartSimpleWaveGame();
    }

	void StartSimpleWaveGame()
    {
        var wave = Waves[_waveIndex];
        StartWave(wave);
        ObjectPool.Instance.IncreaseAmountToCreate(wave.IncreaseAmountToCreate);
        if (_waveIndex + 1 < Waves.Length )
            _waveIndex++;
        Invoke("StartSimpleWaveGame", wave.WaveDuration);
    }
	
    void StartWave(Wave wave)
    {
        int amountToSpawn = Random.Range(wave.MinAmount, wave.MaxAmount);
        for (int i = 0; i < amountToSpawn; i++)
        {
            Invoke("Spawn", Random.Range(0,wave.SpawnTime));
        }
    }

    void Spawn()
    {
        
        var currentWave = Waves[_waveIndex];
        if (currentWave.EnemyPrefabs.Length > 0)
        {
            int index = Random.Range(0, currentWave.EnemyPrefabs.Length);
            //TODO: Elejir que enemigos spawnear en cada wave
            GameObject enemyPrefab = currentWave.EnemyPrefabs[index];
			GameObject enemy = ObjectPool.Instance.GetFromPool (enemyPrefab);
			if(enemy != null)
				enemy.transform.position = transform.position + Vector3.up * Random.Range (MinPositionOffset, MaxPositionOffset);
			//Instantiate(enemyPrefab, transform.position + Vector3.up * Random.Range(MinPositionOffset, MaxPositionOffset), Quaternion.identity);
        
		}
        else
        {
            Debug.LogError("Wave Spawner :: There are no enemies in the Enemy Prefab Array!!");
        }
    }

}
