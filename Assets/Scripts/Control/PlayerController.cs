using UnityEngine;
using RPG.Movement;
using RPG.Combat;

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
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
                foreach (RaycastHit hit in hits)
                {
                    CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                    if (target != null)
                    {
                        fighter.Attack(target);
                    }
                }
            }
        }

        private void InteractWithMovement()
        {
            // Move to click
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                mover.MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}