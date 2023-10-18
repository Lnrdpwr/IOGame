using TMPro;
using UnityEngine;

public class Enemyname : MonoBehaviour, Name
{
    [SerializeField] private string[] _names;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = _names[Random.Range(0, _names.Length)] + Random.Range(1, 1000).ToString();
    }

    public string GetName()
    {
        return _text.text;
    }
}
