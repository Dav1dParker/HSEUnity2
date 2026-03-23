using UnityEngine;

namespace _Pr2.Scripts.Objects
{
    public sealed class Spike : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6.5f;
        [SerializeField] private float damage = 20f;

        private float despawnX = float.NegativeInfinity;

        public float Damage => damage;

        public void SetDespawnX(float newDespawnX)
        {
            despawnX = newDespawnX;
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

            if (transform.position.x <= despawnX)
            {
                Destroy(gameObject);
            }
        }
    }
}
