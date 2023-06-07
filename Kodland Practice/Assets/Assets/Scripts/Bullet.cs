using System;
using Assets.Scripts.GameManagement;
using UnityEngine;

namespace Assets.Scripts {
    public class Bullet : MonoBehaviour {

        private const float SPEED = 3;
        private Vector3 _direction;
        private GameObject _mainCamera;
        private Rigidbody _rb;

        public static Action OnAlienDeath;

        private void ExecuteAlienDeath() {
            Destroy(gameObject);
        }

        private void Awake() {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            _rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            Destroy(gameObject, 3);
        }

        private void FixedUpdate() {
            _rb.velocity += _mainCamera.transform.forward * (SPEED * Time.fixedTime);
        }

        private void OnTriggerEnter(Collider other) {
            //Debug.Log($"HIT: {other.name}");
            if (!other.CompareTag("Enemy")) return;
            OnAlienDeath.Invoke();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}