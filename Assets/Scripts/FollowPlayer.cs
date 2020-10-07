using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform player;

    Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + cameraOffset;
    }
}
