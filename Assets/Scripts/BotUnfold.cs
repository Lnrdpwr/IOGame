using UnityEngine;

public class BotUnfold : MonoBehaviour
{
    [SerializeField] private Transform _bot;
    [SerializeField] private Transform _canvas;

    private void Awake()
    {
        _bot.parent = null;
        _canvas.parent = null;
        Destroy(gameObject);
    }
}
