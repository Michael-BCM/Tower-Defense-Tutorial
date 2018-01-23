using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField]
    private Text roundsText;

    private void OnEnable() //Called on the frame that a GameObject is enabled. 
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText ()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.7f);

        while (round < PlayerStats.rounds)
        {
            round++;
            roundsText.text = round.ToString();
            yield return new WaitForSeconds(.1f);
        }
    }
}