using _Pr2.Scripts.UI;
using UnityEngine;

namespace _Pr2.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private int healCost = 10;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private ScoreUI scoreUI;
        [SerializeField] private HealButtonUI healButtonUI;

        private float currentHealth;
        private int score;

        private void Awake()
        {
            Instance = this;
            currentHealth = maxHealth;
            RefreshUI();
        }

        public void ApplyDamage(float damage)
        {
            currentHealth = Mathf.Max(0f, currentHealth - damage);
            healthBar?.UpdateBar(currentHealth, maxHealth);
        }

        public void AddScore(int scoreAmount)
        {
            score += scoreAmount;
            RefreshScoreUI();
        }

        public void TryRestoreHealth()
        {
            if (score < healCost)
            {
                return;
            }

            score -= healCost;
            currentHealth = maxHealth;
            RefreshUI();
        }

        private void RefreshUI()
        {
            healthBar?.UpdateBar(currentHealth, maxHealth);
            RefreshScoreUI();
        }

        private void RefreshScoreUI()
        {
            scoreUI?.UpdateScore(score);
            healButtonUI?.SetInteractable(score >= healCost);
        }
    }
}
