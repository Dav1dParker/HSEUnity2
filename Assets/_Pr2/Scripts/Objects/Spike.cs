using UnityEngine;

namespace _Pr2.Scripts.Objects
{
    public sealed class Spike : ScrollingObject
    {
        [SerializeField] private float damage = 20f;

        public float Damage => damage;
    }
}
