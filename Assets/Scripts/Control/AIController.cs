using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        Transform target;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;    
        }

        private void Update()
        {
            if (DistanceToTarget() <= chaseDistance)
            {
                print(name + " Should chase");
            }
        }

        private float DistanceToTarget()
        {
            return Vector3.Distance(transform.position, target.position);
        }
    }
}
