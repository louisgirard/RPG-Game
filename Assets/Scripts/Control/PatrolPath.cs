using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            DrawPatrolPath();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            DrawPatrolPath();
        }

        private void DrawPatrolPath()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = (i + 1) % transform.childCount;
                Gizmos.DrawSphere(GetWaypoint(i), 0.2f);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int NextWaypointIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }
    }
}