using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System.IO;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 2f;
         
        ActionScheduler actionScheduler;
        Fighter fighter;
        Mover mover;
        Health health;

        Transform player;

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int waypointIndex = 0;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            actionScheduler = GetComponent<ActionScheduler>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fighter.CanAttack(player.gameObject))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspiciousTime)
            {
                SuspiciousBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player.gameObject);
        }

        private void SuspiciousBehaviour()
        {
            actionScheduler.CancelAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;

            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    waypointIndex = patrolPath.NextWaypointIndex(waypointIndex);
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(waypointIndex);
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) <= waypointTolerance;
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, player.position) <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
