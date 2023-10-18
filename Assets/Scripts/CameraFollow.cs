using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _velocity = Vector3.zero;
    private float _yOffset;

    private void Start()
    {
        _yOffset = transform.position.y;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_target.position.x, _yOffset, _target.position.z - 15), ref _velocity, 0.25f);
    }
}
