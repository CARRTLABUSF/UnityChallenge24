using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private float MovementDistanceZ = 0f;
    [SerializeField] private float MovementSpeed = 3f;

    private Vector3 _targetPosition;
    private Vector3 _rightTargetPosition;
    private Vector3 _leftTargetPosition;
    
    private void Start()
    {
        _leftTargetPosition = transform.position;
        _rightTargetPosition = _leftTargetPosition + new Vector3(0f, 0f, MovementDistanceZ);

        _targetPosition = _rightTargetPosition;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MovementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            if (_targetPosition == _rightTargetPosition)
                _targetPosition = _leftTargetPosition;
            else
                _targetPosition = _rightTargetPosition;
        }
    }

}