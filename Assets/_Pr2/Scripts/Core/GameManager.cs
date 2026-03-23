using _Pr2.Scripts.UI;
using UnityEngine;

namespace _Pr2.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private HealthBar healthBar;

        private float currentHealth;

        private void Awake()
        {
            Instance = this;
            currentHealth = maxHealth;
            healthBar?.UpdateBar(currentHealth, maxHealth);
        }

        public void ApplyDamage(float damage)
        {
            currentHealth = Mathf.Max(0f, currentHealth - damage);
            healthBar?.UpdateBar(currentHealth, maxHealth);
        }
    }
}
