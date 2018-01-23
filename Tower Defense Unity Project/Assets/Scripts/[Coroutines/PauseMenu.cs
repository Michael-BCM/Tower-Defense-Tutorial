using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class controls the state of the user interface for the Pause Menu. 
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The Pause Menu UI. 
    /// </summary>
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private SceneFader sceneFader;

    private string menuSceneName = "MainMenu";

	private void Update ()
    {
		if(Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
	}
    
    /// <summary>
    /// Toggles the active state of the UI, and sets the TimeScale accordingly. 
    /// </summary>
    public void Toggle ()
    {
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
            Time.timeScale = 0F;
        else
            Time.timeScale = 1F;
    }
    
    /// <summary>
    /// The 'retry' button. Reloads the current scene. 
    /// </summary>
    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu ()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }    
}