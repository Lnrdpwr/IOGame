using TMPro;
using UnityEngine;
using YG;

public class PlayerName : MonoBehaviour, Name
{
    private TMP_Text _text;

    private void Start()
    {
        _text= GetComponent<TMP_Text>();
        string name = YandexGame.playerName;
        _text.text = name != "unauthorized" ? name : "";
    }

    public string GetName()
    {
        return _text.text;
    }
}
