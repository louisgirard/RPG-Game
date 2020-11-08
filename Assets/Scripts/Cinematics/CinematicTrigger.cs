using RPG.Movement;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool played = false;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player") && !played)
            {
                GetComponent<PlayableDirector>().Play();
                played = true;
            }
        }
    }
}
