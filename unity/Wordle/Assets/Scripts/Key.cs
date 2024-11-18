using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Keyboard keyboard;

    private void Awake()
    {
        Text.text = "";
    }

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void KeyPressed()
    {
        keyboard.KeyPressed(Text.text);
    }
}
