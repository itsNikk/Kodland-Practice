using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {
    public class PlayerController : MonoBehaviour {

        [SerializeField] private GameObject bullet;
        [SerializeField] private Transform rifleStart;
        [SerializeField] private Text HpText;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private float health;
        [SerializeField] private float playerSpeed;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private int jumpHeight = 3;

        private CharacterController _controller;
        private Vector3 _velocity;
        private bool _isGrounded;

        public static Action OnPlayerDeath;

        private void Awake() {
            _controller = GetComponent<CharacterController>();
        }

        private void Start() {
            HpText.text = health.ToString(CultureInfo.InvariantCulture);
        }

        private void ChangeHealth(int hp) {
            health += hp;

            switch (health) {
                case > 100:
                    health = 100;
                    break;
                case <= 0:
                    OnPlayerDeath.Invoke();
                    break;
            }

            HpText.text = health.ToString(CultureInfo.InvariantCulture);
        }

        private void Update() {
            MovePlayer();
            ShootBullet();

            /*Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets) {
            if (item.tag == "Heal") {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }

            if (item.tag == "Finish") {
                Win();
            }

            if (item.tag == "Enemy") {
                Lost();
            }
        }*/
        }

        private void ShootBullet() {
            if (!Input.GetMouseButtonDown(0)) return;

            Instantiate(bullet, rifleStart.position, transform.rotation);
        }

        private void MovePlayer() {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 moveVector = transform.right * x + transform.forward * z;

            _controller.Move(moveVector * (playerSpeed * Time.deltaTime));

            if (Input.GetButtonDown("Jump") && _isGrounded)
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            _velocity.y += gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Enemy")) ChangeHealth(-20);
        }

    }
}