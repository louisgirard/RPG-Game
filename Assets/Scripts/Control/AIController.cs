using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Transform target;
        Fighter fighter;
        Health health;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
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
                fighter.Cancel();
            }
        }

        private bool InAttackRange()
        {
            return Vector3.Distance(transform.position, target.position) <= chaseDistance;
        }
    }
}
