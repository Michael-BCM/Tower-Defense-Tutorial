using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    /// <summary>
    /// The index number of the next level. This should change depending on the current level.
    /// Consider using JSON instead of PlayerPrefs.
    /// </summary>
    private int nextLevelIndex = 2;

    /// <summary>
    /// A field to hold the object that fades the game between scenes. 
    /// </summary>
    [SerializeField]
    private SceneFader sceneFader;
    
    /// <summary>
    /// Executed through Unity's 'On Click' event, 
    /// when the user clicks the 'Continue' button on the 'Level Complete' screen. 
    /// Sets 'levelReached' and advances the game to the next level. 
    /// </summary>
    public void Continue ()
    {
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        sceneFader.FadeTo("Level02");
    }

    /// <summary>
    /// Executed through Unity's 'On Click' event, 
    /// when the user clicks the 'Continue' button on the 'Level Complete' screen. 
    /// Returns the user to the Main Menu.
    /// </summary>
    public void Menu ()
    {
        sceneFader.FadeTo("MainMenu");
    }
}