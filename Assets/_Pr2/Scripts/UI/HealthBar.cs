using UnityEngine;
using UnityEngine.UI;

namespace _Pr2.Scripts.UI
{
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBar;

        public void UpdateBar(float currentHealth, float maxHealth)
        {
            if (!healthBar)
            {
                return;
            }

            healthBar.fillAmount = maxHealth <= 0f ? 0f : currentHealth / maxHealth;
        }
    }
}
