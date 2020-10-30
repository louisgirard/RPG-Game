using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 20f;
        bool isDead = false;
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if(health == 0 && !isDead)
            {
                animator.SetTrigger("die");
                isDead = true;
            }
        }
    }
}