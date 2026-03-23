using UnityEngine;

namespace _Pr2.Scripts.Objects
{
    public abstract class ScrollingObject : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6.5f;

        private float despawnX = float.NegativeInfinity;

        public void SetDespawnX(float newDespawnX)
        {
            despawnX = newDespawnX;
        }

        public void SetMoveSpeed(float newMoveSpeed)
        {
            moveSpeed = newMoveSpeed;
        }

        protected virtual void FixedUpdate()
        {
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

            if (transform.position.x <= despawnX)
            {
                Destroy(gameObject);
            }
        }
    }
}
