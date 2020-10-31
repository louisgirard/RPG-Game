﻿using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 3f;
        [SerializeField] float timeBetweenAttacks = 1f;

        float timeSinceLastAttack = 0;
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
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        // Animation event
        private void Hit()
        {
            if (target == null) return;
            target.TakeDamage(5);
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionScheduler.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3);
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            return (combatTarget != null && !combatTarget.GetComponent<Health>().IsDead());
        }
    }
}