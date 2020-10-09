using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;
        NavMeshAgent navMeshAgent;

        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination, float stoppingDistance = 2f)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.stoppingDistance = stoppingDistance;
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
        }
    }
}