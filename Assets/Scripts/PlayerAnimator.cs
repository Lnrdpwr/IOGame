using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _running = false;

    public void SetRunningAnimation(bool value)
    {
        if (_running != value)
        {
            _animator.SetBool("Running", value);
            _running = value;
        }
    }

    public void DeathAnimation()
    {
        _animator.SetTrigger("Death");
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    public void SetAnimator(Animator newAnimator)
    {
        _animator = newAnimator;
        SetRunningAnimation(false);
    }
}
