using UnityEngine;

/// <summary>
/// Functionality for a single wave of enemies. 
/// </summary>
[System.Serializable]
public class Wave
{
    [SerializeField]
    private GameObject _enemy;
    /// <summary>
    /// The type of enemy.
    /// </summary>
    public GameObject enemy { get { return _enemy; } }

    [SerializeField]
    private int _count;
    /// <summary>
    /// The number of enemies to spawn. 
    /// </summary>
    public int count { get { return _count; } }

    [SerializeField]
    private float _enemiesPerSecond;
    /// <summary>
    /// The number of enemies to spawn per second.
    /// </summary>
    public float enemiesPerSecond { get { return _enemiesPerSecond; } }

    [SerializeField]
    private GameObject _spawnPoint;

    /// <summary>
    /// Where this wave will spawn. 
    /// </summary>
    public GameObject spawnPoint { get { return _spawnPoint; } }

    [SerializeField]
    private float _timeToNextWave;

    /// <summary>
    /// TIme remaining until the next wave begins. 
    /// </summary>
    public float timeToNextWave { get { return _timeToNextWave; } }
}