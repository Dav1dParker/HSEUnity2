using _Pr2.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Pr2.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int healPerTick = 10;
        [SerializeField] private int minHealCost = 1;
        [SerializeField] private int costPerTick = 1;
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private ScoreUI scoreUI;
        [SerializeField] private HealButtonUI healButtonUI;

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
            RefreshHealButton();

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
            RefreshHealButton();
        }

        public bool TryHealTick()
        {
            if (isGameOver || score < minHealCost || currentHealth >= maxHealth)
            {
                return false;
            }

            score -= costPerTick;
            currentHealth = Mathf.Min(maxHealth, currentHealth + healPerTick);
            RefreshUI();
            return true;
        }

        public bool TryFullHealFromButton()
        {
            if (isGameOver || score < minHealCost || currentHealth >= maxHealth)
            {
                return false;
            }

            score -= minHealCost;
            currentHealth = maxHealth;
            RefreshUI();
            return true;
        }

        private void TriggerGameOver()
        {
            isGameOver = true;
            Time.timeScale = 0f;
            RefreshHealButton();

            if (gameOverObject)
            {
                gameOverObject.SetActive(true);
            }
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void RefreshUI()
        {
            healthBar?.UpdateBar(currentHealth, maxHealth);
            scoreUI?.UpdateScore(score);
            RefreshHealButton();
        }

        private void RefreshHealButton()
        {
            healButtonUI?.SetInteractable(!isGameOver && score >= minHealCost && currentHealth < maxHealth);
        }
    }
}
