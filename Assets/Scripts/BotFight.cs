using TMPro;
using System.Collections;
using UnityEngine;

public class BotFight : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;

    private MeatEatingComponent _meatEater;
    public bool _canFight = true;

    private void Start()
    {
        _meatEater = GetComponent<MeatEatingComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touch");
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
            _canFight = false;
            GetComponent<Bot>().LockMovement(enemy.transform);
            enemy.GetComponent<Bot>().LockMovement(transform);

            int meatToAdd = enemyMeatComp._meatEaten / 4;

            if (_meatEater._meatEaten > enemyMeatComp._meatEaten)
            {
                enemy.GetComponent<BotFight>()._canFight = false;
                Vector3 effectpos = enemy.transform.position;

                GetComponent<PlayerAnimator>().AttackAnimation();
                enemy.GetComponent<PlayerAnimator>().DeathAnimation();
                yield return new WaitForSeconds(2f);

                Instantiate(_deathEffect, effectpos, Quaternion.identity);

                _meatEater.AddMeat(meatToAdd);
                enemy.GetComponent<Bot>().SelfDestroy();

                BotSpawner.Instance.Spawn();
            }
            _canFight = true;
        }
    }
}
