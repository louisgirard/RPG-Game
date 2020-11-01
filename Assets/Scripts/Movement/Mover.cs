using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent navMeshAgent;
        float maxSpeed;

        Animator animator;
        ActionScheduler actionScheduler;
        Health health;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            maxSpeed = navMeshAgent.speed;
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedCoef = 1f)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination, 2f, speedCoef);
        }

        public void MoveTo(Vector3 destination, float stoppingDistance = 2f, float speedCoef = 1f)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.speed = maxSpeed * speedCoef;
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