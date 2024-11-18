using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class WordRow : MonoBehaviour
{
    public WordTile[] WordTiles;


    public void SetText(string text, int position)
    {
        WordTiles[position].SetText(text);   
    }

    public void ClearText(int position)
    {
        WordTiles[position].ClearText();
    }

    public string GetWord()
    {
        var word = "";
        foreach (WordTile tile in WordTiles)
        {
            word += tile.GetText();
        }
        return word;
    }

    public void HighLight(MatchType[] result)
    {
        for (int i = 0; i < WordTiles.Length; i++)
        {
            WordTiles[i].Mark(result[i]);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < WordTiles.Length; i++)
        {
            WordTiles[i].Mark(MatchType.NONE);
            WordTiles[i].ClearText();
        }
    }


}
