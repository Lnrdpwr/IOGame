using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PanelScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerCanvas;
    [SerializeField] private MeatEatingComponent _playerMeatEat;
    [SerializeField] private TMP_Text _currentResult;
    [SerializeField] private TMP_Text _bestResult;
    [SerializeField] private MeatSpawn _meatSpawner;
    [SerializeField] private GameObject _objectsToHide;

    internal static PanelScript Instance;

    private void Start()
    {
        Instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        int record = PlayerPrefs.GetInt("Record", 0);

        if (record < _playerMeatEat._meatEaten)
        {
            PlayerPrefs.SetInt("Record", _playerMeatEat._meatEaten);
            YandexGame.NewLeaderboardScores("EatenMeat", _playerMeatEat._meatEaten);

            record = _playerMeatEat._meatEaten;
        }

        SceneManager.LoadScene("Menu");
    }
    
    public void StopGame()
    {
        _objectsToHide.SetActive(false);    
        _playerCanvas.SetActive(false);
        YandexGame.FullscreenShow();
        _meatSpawner.StopAllCoroutines();

        int record = PlayerPrefs.GetInt("Record", 0);

        if(record < _playerMeatEat._meatEaten)
        {
            PlayerPrefs.SetInt("Record", _playerMeatEat._meatEaten);
            YandexGame.NewLeaderboardScores("EatenMeat", _playerMeatEat._meatEaten);

            record = _playerMeatEat._meatEaten;
        }

        _currentResult.text = $"Съедено мяса: {_playerMeatEat._meatEaten}";
        _bestResult.text = $"Лучший результат: {record}";
    }
}
