using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
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
                    // Attack target
                    animator.SetTrigger("attack");
                }
            }
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

        // Animation event
        private void Hit()
        {

        }
    }
}