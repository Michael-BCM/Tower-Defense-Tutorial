using UnityEngine;

/// <summary>
/// Contains functionality for losing the game, and winning the current level.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Has the game ended yet?
    /// </summary>
    public static bool hasGameEnded { get; private set; }

    /// <summary>
    /// A GameObject containing the canvas elements for the Game Over screen. 
    /// </summary>
    [SerializeField]
    private GameObject gameOverUI;

    /// <summary>
    /// A GameObject containing the canvas elements for the Level Complete screen. 
    /// </summary>
    [SerializeField]
    private GameObject completeLevelUI;

    private bool _canWinLevel;
    public bool canWinLevel { get { return _canWinLevel; } }
    
    private void Start()
    {
        hasGameEnded = false;
        _canWinLevel = true;
    }

    private void Update()
    {
        if (hasGameEnded)
            return;
        
        if (PlayerStats.lives <= 0)
        {
            _canWinLevel = false;
            LoseGame();
        }
    }

    /// <summary>
    /// Ends the game. 
    /// </summary>
    private void EndGame()
    {
        hasGameEnded = true;
    }

    /// <summary>
    /// Ends the game and enables the canvas elements for the Game Over screen. 
    /// </summary>
    private void LoseGame ()
    {
        EndGame();
        gameOverUI.SetActive(true);
    }

    /// <summary>
    /// Ends the game and enables the canvas elements for the Level Complete screen. 
    /// </summary>
    public void WinLevel()
    {
        EndGame();
        completeLevelUI.SetActive(true);
    }
}