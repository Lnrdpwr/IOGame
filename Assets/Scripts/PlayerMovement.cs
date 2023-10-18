using System.Collections;
using UnityEngine;
using YG;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Joystick _joystick;

    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;
    private bool _isDesktop;
    private float horizontalMovement;
    private float verticalMovement;

    public bool _lockMovement;

    internal static Transform Instance;

    private void Awake()
    {
        _speed += PlayerPrefs.GetInt("SpeedLvl", 1);

        Instance = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<PlayerAnimator>();

        _isDesktop = YandexGame.EnvironmentData.isDesktop;


        if (_isDesktop)
            _joystick.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_lockMovement)
            return;

        if (_isDesktop)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalMovement = _joystick.Horizontal;
            verticalMovement = _joystick.Vertical;
        }
        

        Vector3 velocity = new Vector3(horizontalMovement, 0, verticalMovement).normalized * _speed;
        _rigidbody.velocity = velocity;

        if (_rigidbody.velocity.magnitude != 0)
        {
            transform.localRotation = Quaternion.LookRotation(velocity);
            _playerAnimator.SetRunningAnimation(true);
        }
        else
            _playerAnimator.SetRunningAnimation(false);
    }
}
