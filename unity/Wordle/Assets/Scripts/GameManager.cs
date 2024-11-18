using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public const int MAX_GUESS = 6;
    public const int WORD_LENGTH = 5;

    // If true, it will use QWERTY else uses ABCDE
    public bool UseStandardKeyboard = false;

    // if true dictinary checks will happen else not
    public bool CanVerifyWordInDictionary = false;

    // if true allow Duplicate Words
    public bool AllowDuplicateWords = true;

    public GameBoard Board;

    public WordleGame Game;

    public GameOver GameOver;

    public string GameWord;

    internal HashSet<string> dictionary;
    internal List<string> gameWords;


    private void Start()
    {
        // Select a random Game word from the list of Words 
        LoadDictionary();

        Game = new WordleGame(WORD_LENGTH, MAX_GUESS, !AllowDuplicateWords, CanVerifyWordInDictionary, ref dictionary);
        
        StartNewGame();
    }

    
    public void SubmitWord(string word)
    {
        Match wordMatch = Game.SubmitWord(word);

        print(wordMatch);
        Board.HandleWordMatch(wordMatch);

        if (Game.isGameOver)
        {
            GameOver.Show(Game.isPlayerWon);
            GameOver.gameObject.SetActive(true);
            Board.HandleGameOver();
        }
    }

    private void LoadDictionary()
    {
        if (dictionary == null) {

            var asset = Resources.Load<TextAsset>("dict5");
            dictionary = new HashSet<string>();
            foreach (string word in asset.text.Split("\r\n"))
            {
                dictionary.Add(word);
            }
        }
    }

    private string GetRandomGameWord()
    {
        if (gameWords == null)
        {
            gameWords = new List<string>();
            var asset = Resources.Load<TextAsset>("game_words");
            foreach (string word in asset.text.Split("\r\n"))
            {
                gameWords.Add(word);
            }
        }
        if (gameWords.Count > 0)
        {
            int index = Random.Range(0, gameWords.Count);
            return gameWords[index];
        }
        return "ERROR";
    }

    public void StartNewGame()
    {
        GameWord = GetRandomGameWord();
        print("Game Word:" + GameWord);
        Game.Start(GameWord);
        Board.Start();
        
        GameOver.gameObject.SetActive(false);
    }

    public void RetryGame()
    {
        GameOver.gameObject.SetActive(false);
        Board.Start();
        Game.Start(GameWord);
    }
}
