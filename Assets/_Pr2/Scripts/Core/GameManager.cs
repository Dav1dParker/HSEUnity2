using _Pr2.Scripts.UI;
using UnityEngine;

namespace _Pr2.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float healPerTick = 10f;
        [SerializeField] private int costPerTick = 1;
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private ScoreUI scoreUI;

        private float currentHealth;
        private int score;
        private bool isGameOver;

        private void Awake()
        {
            Instance = this;
            Time.timeScale = 1f;
            currentHealth = maxHealth;
            isGameOver = false;

            if (gameOverObject)
            {
                gameOverObject.SetActive(false);
            }

            RefreshUI();
        }

        public void ApplyDamage(float damage)
        {
            if (isGameOver)
            {
                return;
            }

            currentHealth = Mathf.Max(0f, currentHealth - damage);
            healthBar?.UpdateBar(currentHealth, maxHealth);

            if (currentHealth <= 0f)
            {
                TriggerGameOver();
            }
        }

        public void AddScore(int scoreAmount)
        {
            if (isGameOver)
            {
                return;
            }

            score += scoreAmount;
            scoreUI?.UpdateScore(score);
        }

        public bool TryHealTick()
        {
            if (isGameOver || score < healPerTick || currentHealth >= maxHealth)
            {
                return false;
            }

            score -= costPerTick;
            currentHealth = Mathf.Min(maxHealth, currentHealth + healPerTick);
            RefreshUI();
            return true;
        }

        private void TriggerGameOver()
        {
            isGameOver = true;
            Time.timeScale = 0f;

            if (gameOverObject)
            {
                gameOverObject.SetActive(true);
            }
        }

        private void RefreshUI()
        {
            healthBar?.UpdateBar(currentHealth, maxHealth);
            scoreUI?.UpdateScore(score);
        }
    }
}
