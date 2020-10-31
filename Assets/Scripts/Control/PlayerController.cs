using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using TMPro;

namespace RPG.Control
{    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        // Start is called before the first frame update
        void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            if(InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }
            print("nothing to do");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (fighter.CanAttack(target))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        fighter.Attack(target);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}