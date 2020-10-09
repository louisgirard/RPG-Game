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

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (target)
            {
                bool isInRange = Vector3.Distance(transform.position, target.position) <= weaponRange;
                if (!isInRange)
                {
                    mover.MoveTo(target.position, weaponRange);
                }
                else
                {

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
            target = null;
        }
    }
}