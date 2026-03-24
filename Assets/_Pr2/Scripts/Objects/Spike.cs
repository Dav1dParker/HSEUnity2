using UnityEngine;

namespace _Pr2.Scripts.Objects
{
    public sealed class Spike : ScrollingObject
    {
        [SerializeField] private int damage = 20;

        public float Damage => damage;
    }
}
