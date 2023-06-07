using UnityEngine;

namespace Assets.Scripts {
    public class PlayerLook : MonoBehaviour {

        [SerializeField] private float mouseSense = 100f;
        [SerializeField] private Transform player;

        private float _xAxisClamp;

        private void Start() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            float mouseRotationX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
            float mouseRotationY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

            _xAxisClamp -= mouseRotationY;

            _xAxisClamp = Mathf.Clamp(_xAxisClamp, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xAxisClamp, 0f, 0f);
            player.Rotate(Vector3.up * mouseRotationX);
        }

    }
}