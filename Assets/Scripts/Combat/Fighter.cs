using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;

        float timeSinceLastAttack = 0;
        Transform target;
        Mover mover;
        ActionScheduler actionScheduler;
        Animator animator;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target)
            {
                bool isInRange = Vector3.Distance(transform.position, target.position) <= weaponRange;
                if (!isInRange)
                {
                    // Move to target
                    mover.MoveTo(target.position, weaponRange);
                }
                else
                {
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // Attack target, this will trigger the Hit() event
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Animation event
        private void Hit()
        {
            target.GetComponent<Health>().TakeDamage(5);
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }
    }
}