using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class WordleGame
{
    private int wordLength;
    private int maxGuessCount;

    private bool checkInDictionary;
    private bool allowDuplicates;

    private string gameWord;
    private HashSet<string> dictionary;

    // Working Variables
    private List<string> guessedWords;
    public bool isGameOver = false;
    public bool isPlayerWon = false;


    public WordleGame(int length, int guessCount, bool dupCheck, bool dictCheck, ref HashSet<string> dictionary) 
    {
        wordLength = length;
        maxGuessCount = guessCount;
        allowDuplicates = dupCheck;
        checkInDictionary = dictCheck;
        this.dictionary = dictionary;
        guessedWords = new List<string>();
    }


    public void Start(string word)
    {
        guessedWords.Clear();
        isGameOver = false;
        isPlayerWon= false;
        gameWord = word;
    }

    public Match SubmitWord(string word)
    {
        Match match = new Match(wordLength);
       
        // Validates the word
        if (word.Length < wordLength)
        {
            match.error = MatchError.INVALID_WORD;
            return match;
        }
        else if (checkInDictionary && !dictionary.Contains(word))
        {
            match.error = MatchError.NOT_IN_DICTIONARY;
        }
        else if (!allowDuplicates && guessedWords.Contains(word))
        {
            match.error = MatchError.DUPLICATE_WORD;
        }


        // Checks the word with respect to GameWord
        var wordFound = true;
        for (int i = 0; i < wordLength; i++)
        {
            if (word[i] == gameWord[i])
            {
                match.result[i] = MatchType.SUCCESS;
            }
            else
            {
                wordFound = false;
                bool foundLetter = false;
                for (int j = 0; j < wordLength; j++)
                {
                    if (word[i] == gameWord[j])
                    {
                        // Found Partial match
                        match.result[i] = MatchType.PARTIAL;
                        foundLetter = true;
                        break;
                    }
                }
                if (!foundLetter)
                {
                    match.result[i] = MatchType.FAILED;
                    // No Match
                }
            }
        }

        if (match.error == MatchError.NONE)
            guessedWords.Add(word);

        if (wordFound || guessedWords.Count == maxGuessCount)
        {
            isGameOver = true;
            isPlayerWon = wordFound;
        }

        return match;
    }
}

public enum MatchError
{
    NONE, INVALID_WORD, DUPLICATE_WORD, NOT_IN_DICTIONARY
}

public enum MatchType
{
    NONE, FAILED, PARTIAL, SUCCESS
}


public struct Match
{
    public MatchError error;
    public MatchType[] result;

    public Match(int length)
    {
        error = MatchError.NONE;
        result = new MatchType[length];
        for(int i = 0; i < length; i++)
        {
            result[i] = MatchType.NONE;
        }
    }

    public override string ToString()
    {
        string str = error.ToString() + ": [";
        foreach(MatchType t in result)
        {
            str += t.ToString() + ", ";
        }
        str += "]";
        return str;
    }
}
