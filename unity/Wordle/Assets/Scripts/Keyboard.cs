using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{

    const string ALPHABET_KEYS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string STANDARD_KEYS = "QWERTYUIOPASDFGHJKLZXCVBNM";

    public GameManager Manager;

    public GameBoard Board;

    public GameObject SubmitButton;

    public GameObject ClearButton;

    public Key[] Keys = new Key[26];

    private bool inputEnabled = false;

    private void Start()
    {
        SetupKeyboard(
            Manager.UseStandardKeyboard ? STANDARD_KEYS : ALPHABET_KEYS);
    }

    public void EnableInput(bool enable)
    {
        inputEnabled = enable;
    }

    private void Update()
    {
        if (Input.anyKeyDown && inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitPressed();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
            {
                ClearPressed();
            }
            else {

                if (Input.inputString.Length > 0 && char.IsLetter(Input.inputString[0]))
                {
                    KeyPressed(Input.inputString[0].ToString());
                }
            }
        }
    }

    public void SetupKeyboard(string keymap)
    {
        for (int i = 0; i < Keys.Length; i++)
        {
            var key = Keys[i];
            key.SetText(keymap[i].ToString());
            key.keyboard = this;
        }
    }

    public void SubmitPressed()
    {
        if (inputEnabled)
        {
            Board.SubmitWord();
        }
    }
    public void ClearPressed()
    {
        if (inputEnabled)
        {
            Board.ClearKey();
        }
    }

    public void KeyPressed(string text)
    {   
        if (inputEnabled)
        {
            text = text.ToUpper();
            Board.KeyPressed(text);
        }
    }
}
