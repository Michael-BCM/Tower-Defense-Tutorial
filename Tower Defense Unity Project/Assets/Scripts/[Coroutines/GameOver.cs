using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;
    
    public void Reload ()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu ()
    {
        sceneFader.FadeTo("MainMenu");
    }
}