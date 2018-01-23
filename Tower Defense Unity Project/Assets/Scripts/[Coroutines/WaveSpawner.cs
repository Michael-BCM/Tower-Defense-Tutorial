using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Spawns waves of enemies into the scene from spawn points dotted around the map.
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    /// <summary>
    /// The number of enemies remaining in the scene. 
    /// </summary>
    public static int enemiesAlive { get; private set; }
        
    /// <summary>
    /// The waves of enemies that will be spawned this round. 
    /// </summary>
    [SerializeField]
    private Wave[] waves;
    
    [SerializeField]
    private int currentWaveIndex = 0;
    
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private float countdown;

    [SerializeField]
    private Text[] waveCountdownTimerText;

    private void Start()
    {
        countdown = waves[0].timeToNextWave;
    }

    private void Update()
    {        
        if(enemiesAlive > 0)
        {
            return;
        }

        if (currentWaveIndex == waves.Length && gameManager.canWinLevel)
        {
            gameManager.WinLevel();
            enabled = false;
            return;
        }

        if (countdown <= 0F)
        {
            StartCoroutine(SpawnWave());

            countdown = waves[currentWaveIndex].timeToNextWave;
            
            return;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0F, Mathf.Infinity);

        waveCountdownTimerText[currentWaveIndex].text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave ()
    {
        PlayerStats.GoToNextRound();

        Wave currentWave = waves[currentWaveIndex];

        for(int i = 0; i < currentWave.count; i++)
        {
            Spawn_Enemy(currentWave.enemy, currentWave.spawnPoint.transform.position);
            enemiesAlive++;
            yield return new WaitForSeconds(1F / currentWave.enemiesPerSecond);
        }

        currentWaveIndex++;
    }

    private void Spawn_Enemy(GameObject enemy, Vector3 position)
    {
        GameObject newEnemy = Instantiate(enemy, position, transform.rotation);
        
        newEnemy.GetComponent<EnemyMovement>().SetOrigin(waves[currentWaveIndex].spawnPoint.GetComponent<Waypoint>());
    }    

    public static void ReduceEnemyCount ()
    {
        enemiesAlive--;
    }
}