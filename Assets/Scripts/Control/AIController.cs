using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspiciousTime = 3f;
        Transform target;
        ActionScheduler actionScheduler;
        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            actionScheduler = GetComponent<ActionScheduler>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRange() && fighter.CanAttack(target.gameObject))
            {
                timeSinceLastSawPlayer = 0;
                fighter.Attack(target.gameObject);
            }
            else if (timeSinceLastSawPlayer < suspiciousTime)
            {
                //Suspicious state, just wait
                actionScheduler.CancelAction();
            }
            else
            {
                mover.StartMoveAction(guardLocation);
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, target.position) <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
