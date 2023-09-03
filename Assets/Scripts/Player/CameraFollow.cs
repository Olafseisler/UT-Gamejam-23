using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] private float smoothDampSpeed = 2.0f;
    private Vector3 _velocity = Vector3.zero;
    
 
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), ref _velocity, smoothDampSpeed);
    }
}
