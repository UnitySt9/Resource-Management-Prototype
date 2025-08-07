using UnityEngine;

namespace _Project.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _dragSpeed = 0.5f;
        [SerializeField] private float _minX = -20f, _maxX = 20f, _minZ = -20f, _maxZ = 20f;

        private Vector3 _lastMousePosition;
        private bool _isDragging = false;

        void Update()
        {
            HandleKeyboardInput();
            HandleMouseDrag();
        }

        private void HandleKeyboardInput()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(h, 0, v) * (_moveSpeed * Time.deltaTime);
            MoveCamera(move);
        }

        private void HandleMouseDrag()
        {
            if (Input.GetMouseButtonDown(2))
            {
                _lastMousePosition = Input.mousePosition;
                _isDragging = true;
            }
            if (Input.GetMouseButtonUp(2))
            {
                _isDragging = false;
            }
            if (_isDragging)
            {
                Vector3 delta = Input.mousePosition - _lastMousePosition;
                Vector3 move = new Vector3(-delta.x, 0, -delta.y) * (_dragSpeed * Time.deltaTime);
                MoveCamera(move);
                _lastMousePosition = Input.mousePosition;
            }
        }

        private void MoveCamera(Vector3 move)
        {
            Vector3 newPos = transform.position + move;
            newPos.x = Mathf.Clamp(newPos.x, _minX, _maxX);
            newPos.z = Mathf.Clamp(newPos.z, _minZ, _maxZ);
            transform.position = newPos;
        }
    }
}