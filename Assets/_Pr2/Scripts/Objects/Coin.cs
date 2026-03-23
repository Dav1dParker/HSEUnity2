using UnityEngine;

namespace _Pr2.Scripts.Objects
{
    public sealed class Coin : ScrollingObject
    {
        [SerializeField] private int scoreValue = 1;

        public int ScoreValue => scoreValue;
    }
}
