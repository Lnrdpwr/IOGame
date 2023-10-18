using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    [SerializeField] private TMP_Text _meatPrice;
    [SerializeField] private TMP_Text _speedPrice;
    [SerializeField] private TMP_Text _meatLvl;
    [SerializeField] private TMP_Text _speedLvl;
    [SerializeField] private TMP_Text _coinsText;

    [SerializeField] private Button _meatButton;
    [SerializeField] private Button _speedButton;

    private int _coins;

    private void Start()
    {
        SetText();
    }

    public void Play()
    {
        SceneManager.LoadScene("Demo");
        SetText();
    }

    public void PlayMap2()
    {
        SceneManager.LoadScene("SampleScene");
        SetText();
    }

    private void SetText()
    {
        int speedLvl = PlayerPrefs.GetInt("SpeedLvl", 1);
        _speedLvl.text = $"Уровень: {speedLvl}";

        int meatLvl = PlayerPrefs.GetInt("MeatLvl", 1);
        _meatLvl.text = $"Уровень: {meatLvl}";

        int speedPrice = speedLvl * 15;
        int meatPrice = meatLvl * 15;
        _meatPrice.text = $"Цена: {meatPrice}";
        _speedPrice.text = $"Цена: {speedPrice}";

        _coins = PlayerPrefs.GetInt("Coins", 0);
        _coinsText.text = $"{_coins}";

        _speedButton.interactable = _coins >= speedPrice ? true : false;
        _meatButton.interactable = _coins >= meatPrice ? true : false;
    }

    public void UpgradeSpeed()
    {
        int price = PlayerPrefs.GetInt("SpeedLvl", 1) * 15;
        if (_coins >= price)
        {
            _coins -= price;
            PlayerPrefs.SetInt("SpeedLvl", price / 15 + 1);
            PlayerPrefs.SetInt("Coins", _coins);
            SetText();
        }
    }

    public void UpgradeMeat()
    {
        int price = PlayerPrefs.GetInt("MeatLvl", 1) * 15;
        if (_coins >= price)
        {
            _coins -= price;
            PlayerPrefs.SetInt("MeatLvl", price / 15 + 1);
            PlayerPrefs.SetInt("AddMeat", PlayerPrefs.GetInt("AddMeat", 0) + 1);
            PlayerPrefs.SetInt("Coins", _coins);
            SetText();
        }
    }

    public void OpenTG()
    {
        Application.OpenURL("https://t.me/infinityrequiemstudio");
    }
}
