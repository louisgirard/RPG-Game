using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position;
    }
}
