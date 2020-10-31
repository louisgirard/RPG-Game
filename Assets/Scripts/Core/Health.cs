using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 20f;
        bool isDead = false;
        Animator animator;
        ActionScheduler actionScheduler;

        private void Start()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0 && !isDead)
            {
                Die();
            }
        }

        private void Die()
        {
            animator.SetTrigger("die");
            isDead = true;
            actionScheduler.CancelAction();
        }

        public bool IsDead()
        {
            return isDead;
        }
    }
}