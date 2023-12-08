using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Transform _myTransform;

    private void Start()
    {
        // store the reference of the GameObject’s transform
        _myTransform = transform;
    }

    private void Update()
    {
        // increment the _myTransform position vector by _moveSpeed and Time.deltaTime
        _myTransform.position += _myTransform.forward * (Time.deltaTime * moveSpeed);
    }
}
