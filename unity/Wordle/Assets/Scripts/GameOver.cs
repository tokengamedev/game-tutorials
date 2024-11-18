using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI Message;

    public GameObject TryAgainButton;

    
    public void Show(bool isPalyerWon)
    {
        if (isPalyerWon)
        {
            TryAgainButton.SetActive(false);
            Message.text = "You have found the Word";
        }
        else
        {
            TryAgainButton.SetActive(true);
            Message.text = "You could not found the Word";
        }
    }


}
