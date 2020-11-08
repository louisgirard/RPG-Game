using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        float timeSinceLastAttack = Mathf.Infinity;
        Health target;
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

            if (target == null) return;
            if (target.IsDead()) return;

            if (!IsInRange())
            {
                // Move to target
                mover.MoveTo(target.transform.position, weaponRange);
            }
            else
            {
                AttackBehaviour();
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) <= weaponRange;
        }

        private void AttackBehaviour()
        {
            FaceTarget();
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                // Attack target, this will trigger the Hit() event
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
        }

        // Animation event
        private void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }

        public void Attack(GameObject combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            return (combatTarget != null && !combatTarget.GetComponent<Health>().IsDead());
        }

        public void Cancel()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
            GetComponent<Mover>().Cancel();
            target = null;
        }
    }
}