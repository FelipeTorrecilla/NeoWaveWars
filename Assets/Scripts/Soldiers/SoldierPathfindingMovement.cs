using UnityEngine;
using UnityEngine.AI;

public class SoldierPathfindingMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        Vector3 movement = direction.normalized * movementSpeed * Time.deltaTime;
        _rigidbody.MovePosition(transform.position + movement);
    }
}

