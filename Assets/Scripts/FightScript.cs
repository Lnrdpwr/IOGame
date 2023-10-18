using System.Collections;
using TMPro;
using UnityEngine;

public class FightScript : MonoBehaviour
{
    [SerializeField] private PanelScript _panel;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private GameObject _deathEffect;

    private int _coins;
    private MeatEatingComponent _meatEater;
    private PlayerMovement _player;
    private bool _canFight = true;

    private void Start()
    {
        _meatEater = GetComponent<MeatEatingComponent>();
        _player = GetComponent<PlayerMovement>();
        _coins = PlayerPrefs.GetInt("Coins", 0);
        _coinsText.text = _coins.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            MeatEatingComponent enemyMeatComp = other.GetComponent<MeatEatingComponent>();

            if (enemyMeatComp._meatEaten != _meatEater._meatEaten)
                StartCoroutine(Fight(other.gameObject, enemyMeatComp));
        }
    }

    IEnumerator Fight(GameObject enemy, MeatEatingComponent enemyMeatComp)
    {
        if (_canFight && enemy.GetComponent<BotFight>()._canFight)
        {
            enemy.GetComponent<BotFight>()._canFight = false;

            _canFight = false;
            _player._lockMovement = true;
            _player.transform.LookAt(enemy.transform.position);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            enemy.GetComponent<Bot>().LockMovement(transform);

            int meatToAdd = enemyMeatComp._meatEaten / 2;

            if (_meatEater._meatEaten > enemyMeatComp._meatEaten)
            {
                GetComponent<PlayerAnimator>().AttackAnimation();
                GetComponent<PlayerAnimator>().SetRunningAnimation(false);
                enemy.GetComponent<PlayerAnimator>().DeathAnimation();
                yield return new WaitForSeconds(2f);

                Instantiate(_deathEffect, enemy.transform.position, Quaternion.identity);

                _meatEater.AddMeat(meatToAdd);
                enemy.GetComponent<Bot>().SelfDestroy();
                _player._lockMovement = false;

                BotSpawner.Instance.Spawn();
                BotSpawner.Instance._addMeat += Mathf.FloorToInt(meatToAdd * 1.25f);

                int coins = PlayerPrefs.GetInt("Coins", 0);
                PlayerPrefs.SetInt("Coins", coins + 10);

                _coinsText.text = (coins + 10).ToString();

                yield return new WaitForSeconds(0.1f);
                //BotSpawner.Instance.AutoLevel(meatToAdd);
            }
            else if (_meatEater._meatEaten < enemyMeatComp._meatEaten)
            {
                GetComponent<PlayerAnimator>().DeathAnimation();
                enemy.GetComponent<PlayerAnimator>().AttackAnimation();
                yield return new WaitForSeconds(2f);

                Instantiate(_deathEffect, transform.position, Quaternion.identity);
                enemy.GetComponent<BotFight>()._canFight = true;
                
                _panel.gameObject.SetActive(true);
                _panel.StopGame();
                Debug.Log("done");
                gameObject.SetActive(false);
            }
            _canFight = true;
        }
    }
}
