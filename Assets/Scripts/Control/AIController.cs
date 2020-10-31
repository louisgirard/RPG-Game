using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Transform target;
        Fighter fighter;
        Mover mover;
        Health health;

        Vector3 guardLocation;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
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
                fighter.Attack(target.gameObject);
            }
            else
            {
                mover.StartMoveAction(guardLocation);
            }
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
