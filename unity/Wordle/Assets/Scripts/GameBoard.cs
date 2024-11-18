using System;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public TextMeshProUGUI Message;

    public GameObject PlayerWords;

    public GameManager Manager;

    public Keyboard Keyboard;

    public WordRow[] Rows;

    private int currentRow = 0;
    private int currentColumn = 0;

    private readonly Dictionary<MatchError, string> errorMessages = new Dictionary<MatchError, string>()
    {
        { MatchError.NONE, "" },
        { MatchError.INVALID_WORD, "INVALID WORD" },
        { MatchError.NOT_IN_DICTIONARY, "NOT IN DICTIONARY" },
        { MatchError.DUPLICATE_WORD, "DUPLICATE WORD" }
    };


    public void Start()
    {
        currentRow = 0;
        currentColumn = 0;
        Message.text = string.Empty;
        foreach(WordRow row in Rows)
        {
            row.Reset();
        }
        Keyboard.EnableInput(true);
    }

    public void ClearKey()
    {
        print("dddd");
        if (currentColumn > 0)
        {
            currentColumn--;
            Rows[currentRow].ClearText(currentColumn);
            Message.text = string.Empty;
        }
    }

    public void KeyPressed(string text)
    {
        if (currentColumn >= 0 && currentColumn < GameManager.WORD_LENGTH)
        {
            Rows[currentRow].SetText(text, currentColumn);
            currentColumn++;
        }
    }

    public void SubmitWord()
    {
        if (currentRow >= 0 && currentRow < Rows.Length)
        {
            if (currentColumn == GameManager.WORD_LENGTH)
            {
                Manager.SubmitWord(Rows[currentRow].GetWord());
            }
        }
    }

    public void HandleWordMatch(Match match)
    {
        Message.text = errorMessages[match.error];
        if (match.error != MatchError.NONE)
        {
            print(currentColumn);
            return;
        }
        Rows[currentRow].HighLight(match.result);
        currentColumn = 0;
        currentRow++;
    }

    public void HandleGameOver()
    {
        Keyboard.EnableInput(false);
    }
}
