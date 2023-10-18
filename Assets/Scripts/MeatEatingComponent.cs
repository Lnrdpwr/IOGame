using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MeatEatingComponent : MonoBehaviour, Name
{
    [SerializeField] private TMP_Text _meatAmountText;
    [SerializeField] private Transform _canvas;
    [SerializeField] private GameObject _popup;
    [SerializeField] private GameObject[] _models;
    [SerializeField] private GameObject _currentModel;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _upgrade;
    [SerializeField] private GameObject _nameObj;
    [SerializeField] private AudioClip _sound;


    private PlayerAnimator _playerAnimator;
    private Vector3 _defaultScale;
    public int _meatEaten = 0;
    private float _multiplier = 1;
    private bool _isPlayer;
    private int _level = 1;
    private Name _name;

    private void Start()
    {
        _name = _nameObj.GetComponent<Name>();
        _meatAmountText.color = Color.blue;

        _defaultScale = transform.localScale;

        if(!gameObject.CompareTag("Enemy"))
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _isPlayer = true;

            _meatEaten += PlayerPrefs.GetInt("AddMeat", 0);
            _multiplier += 0.01f * _meatEaten;
            _meatAmountText.text = _meatEaten.ToString();

            if (_multiplier > 2f)
                _multiplier = 2f;

            transform.localScale = _defaultScale * _multiplier;

            for(int i = 0; i < 5; i++)
            {
                if (_isPlayer && _meatEaten >= _level * 15 && _level <= 4)
                {
                    Destroy(_currentModel);
                    _currentModel = Instantiate(_models[_level], transform);

                    _level++;
                    _playerAnimator.SetAnimator(_currentModel.GetComponent<Animator>());
                    Instantiate(_upgrade, transform.position, Quaternion.identity);
                }
            }
        }

        _meatAmountText.color = Color.Lerp(Color.blue, Color.red, _multiplier / 2.5f);
    }

    public string GetName()
    {
        return _name.GetName();
    }

    public void AddMeat(int amount)
    {
        _meatEaten += amount;
        _multiplier += 0.01f * amount;
        _meatAmountText.text = _meatEaten.ToString();

        if(_multiplier > 2f)
            _multiplier = 2f;

        transform.localScale = _defaultScale * _multiplier;

        if(_isPlayer && _meatEaten >= _level * 15 && _level <= 4)
        {
            Destroy(_currentModel);
            _currentModel = Instantiate(_models[_level], transform);

            _level++;
            _playerAnimator.SetAnimator(_currentModel.GetComponent<Animator>());
            Instantiate(_upgrade, transform.position, Quaternion.identity);
        }

        _meatAmountText.color = Color.Lerp(Color.blue, Color.red, _multiplier / 2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meat"))
        {
            Instantiate(_effect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _meatEaten++;
            _multiplier += 0.01f;
            _meatAmountText.text = _meatEaten.ToString();
            Instantiate(_popup, _canvas);

            if (_multiplier > 2f)
                _multiplier = 2f;

            if (_isPlayer)
                SoundManager.instance.PlayOneShot(_sound);

            if (_isPlayer && _meatEaten >= _level * 15 && _level <= 4)
            {
                Destroy(_currentModel);
                _currentModel = Instantiate(_models[_level], transform);

                _level++;
                _playerAnimator.SetAnimator(_currentModel.GetComponent<Animator>());
                Instantiate(_upgrade, transform.position, Quaternion.identity);
            }
        }

        _meatAmountText.color = Color.Lerp(Color.blue, Color.red, _multiplier / 3f);
        transform.localScale = _defaultScale * _multiplier;
    }
}
