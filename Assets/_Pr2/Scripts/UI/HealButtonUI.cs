using _Pr2.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Pr2.Scripts.UI
{
    public sealed class HealButtonUI : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            if (button)
            {
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnDestroy()
        {
            if (button)
            {
                button.onClick.RemoveListener(OnClick);
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            if (!button)
            {
                return;
            }

            button.interactable = isInteractable;
        }

        private void OnClick()
        {
            if (GameManager.Instance)
            {
                GameManager.Instance.TryFullHealFromButton();
            }
        }
    }
}
