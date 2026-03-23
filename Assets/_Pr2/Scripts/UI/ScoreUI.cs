using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Pr2.Scripts.UI
{
    public sealed class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private string prefix = "Score: ";

        public void UpdateScore(int score)
        {
            if (!scoreText)
            {
                return;
            }

            scoreText.text = prefix + score;
        }
    }
}
