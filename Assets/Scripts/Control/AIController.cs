using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Transform target;
        Fighter fighter;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
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
