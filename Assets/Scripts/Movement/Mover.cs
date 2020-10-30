using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;

        Animator animator;
        ActionScheduler actionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination, float stoppingDistance = 2f)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.stoppingDistance = stoppingDistance;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
    }
}