using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordTile : MonoBehaviour
{

    public TextMeshProUGUI Text;

    public Sprite EmptySprite;

    public Sprite PartialSprite;

    public Sprite SuccessSprite;

    public Sprite FailedSprite;

    private Image image;

    void Awake()
    {
        ClearText();
        image = GetComponent<Image>();
        image.sprite = EmptySprite;
    }

    public void SetText(string text)
    {
        Text.text = text;
    }

    public string GetText()
    {
        return Text.text;
    }

    public void ClearText()
    {
        Text.text = string.Empty;
    }

    public void Mark(MatchType matchType)
    {
        switch(matchType)
        {
            case MatchType.NONE:
                image.sprite = EmptySprite;
                break;
            case MatchType.PARTIAL:
                image.sprite = PartialSprite;
                break;
            case MatchType.FAILED:
                image.sprite = FailedSprite;
                break;
            case MatchType.SUCCESS:
                image.sprite = SuccessSprite;
                break;
        }
    }
    
}
