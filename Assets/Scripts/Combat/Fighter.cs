using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 3f;
        Transform target;
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>(); 
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
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}