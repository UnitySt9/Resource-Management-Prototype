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
#if UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
#endif
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

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    _lastMousePosition = touch.position;
                    _isDragging = true;
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _isDragging = false;
                }
                else if (touch.phase == TouchPhase.Moved && _isDragging)
                {
                    Vector3 delta = (Vector3)touch.position - _lastMousePosition;
                    Vector3 move = new Vector3(-delta.x, 0, -delta.y) * (_dragSpeed * Time.deltaTime);
                    MoveCamera(move);
                    _lastMousePosition = touch.position;
                }
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