using UnityEngine;

namespace RPG.Core
{
    public class FollowPlayer : MonoBehaviour
    {
        Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = player.position;
        }
    }
}