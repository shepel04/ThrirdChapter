using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public float LookSpeed = 2f;
        public Camera PlayerCamera;

        private float _xRotation = 0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;  
        }

        void Update()
        {
            MovePlayer();
            LookAround();
        }

        void MovePlayer()
        {
            float moveX = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            transform.Translate(move, Space.World);
        }

        void LookAround()
        {
            float mouseX = Input.GetAxis("Mouse X") * LookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * LookSpeed;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            PlayerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}


