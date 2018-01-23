using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Retrieves the UnityEngine.UI.Text component of the object it is placed on, 
/// and sets the text to the desired text set by the user.
/// </summary>
public class TextUI : MonoBehaviour
{
    private Text text;
    
    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = DisplayText();
    }
    
    private string DisplayText ()
    {
        /// Set up this class for new GameObjects by adding new cases here. 
        switch (gameObject.name)
        {
            case "MoneyText": return "$" + PlayerStats.money;
            case "LivesText": return "LIVES: " + PlayerStats.lives;
            case "Enemies Remaining Text": return WaveSpawner.enemiesAlive.ToString();
            default: return "";
        }
    }
}