using UnityEngine;

public class CanvasFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _offset;

    private void Start()
    {
        if (!_target.gameObject.CompareTag("Enemy"))
            _offset = new Vector3(0, 8f, -3);
        else
            _offset = new Vector3(0, 3.5f, 0);
    }

    private void Update()
    {
        transform.position = _offset + _target.position;
    }
}
