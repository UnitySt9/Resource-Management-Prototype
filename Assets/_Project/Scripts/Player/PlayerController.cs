using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private readonly float _stopDistance = 1.2f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Animator _animator;
        [SerializeField] private ResourceManager _resourceManager;
        
        private Vector3 _targetPosition;
        private bool _isMoving = false;
        private Building _targetBuilding;

        void Start()
        {
            _targetPosition = transform.position;
        }

        void Update()
        {
            HandleInput();
            MoveToTarget();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Building building = hit.collider.GetComponent<Building>();
                    if (building != null)
                    {
                        _targetBuilding = building;
                        _targetPosition = building.transform.position;
                        _isMoving = true;
                    }
                    else
                    {
                        _targetBuilding = null;
                        _targetPosition = hit.point;
                        _isMoving = true;
                    }
                }
            }
        }

        private void MoveToTarget()
        {
            if (!_isMoving) return;
            Vector3 direction = (_targetPosition - transform.position);
            direction.y = 0f;
            if (direction.magnitude > _stopDistance)
            {
                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
                }
                transform.position += direction.normalized * (_moveSpeed * Time.deltaTime);
                if (_animator != null) _animator.SetBool("isMoving", true);
            }
            else
            {
                transform.position = new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z);
                if (_animator != null) _animator.SetBool("isMoving", false);
                _isMoving = false;
                if (_targetBuilding != null)
                {
                    CollectFromBuilding();
                    _targetBuilding = null;
                }
            }
        }

        private void CollectFromBuilding()
        {
            if (_resourceManager != null && _targetBuilding != null)
            {
                int collected = _targetBuilding.CollectResource();
                _resourceManager.AddResource(_targetBuilding.GetResourceName(), collected);
            }
        }
    }
}