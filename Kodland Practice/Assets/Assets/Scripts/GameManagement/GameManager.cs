using System;
using UnityEngine;

namespace Assets.Scripts.GameManagement {
    public class GameManager : MonoBehaviour {

        [SerializeField] private GameObject Victory;
        [SerializeField] private GameObject GameOver;

        private static int EnemiesKilled { get; set; }

        private void OnEnable() {
            PlayerController.OnPlayerDeath += Lost;
            Bullet.OnAlienDeath += IncreaseDeathCount;
        }

        private void IncreaseDeathCount() => EnemiesKilled++;

        private void Start() {
            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            if (EnemiesKilled == 3) Win();
        }

        private void Win() {
            Victory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Lost() {
            GameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable() {
            PlayerController.OnPlayerDeath -= Lost;
            Bullet.OnAlienDeath -= IncreaseDeathCount;
        }

    }
}