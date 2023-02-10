using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private DustCloud dustCloud;
    [SerializeField] private float maxDistFromPlayer = 1f;
    public GameObject player;
    public float speed;
    public GameObject[] followers;

    private float distance;
    private Vector3 dustcloudDefaultScale;

    // Start is called before the first frame update
    void Start()
    {
        dustcloudDefaultScale = dustCloud.transform.localScale;
    }

    void UpdateDustCloud()
    {
        if (player.transform.GetComponent<Rigidbody2D>().velocity.magnitude > 0)
        {
            dustCloud.ShowCloud();
        }
        else
        {
            dustCloud.HideCloud();
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject toFollow = player;
        foreach (var follower in followers)
        {
            distance = Vector2.Distance(follower.transform.position, toFollow.transform.position);
            Vector2 direction = toFollow.transform.position - follower.transform.position;
            Vector2 hover = new Vector2(follower.transform.position.x, follower.transform.position.y + Mathf.Sin(8f * Time.time));
            follower.transform.position =
                Vector2.MoveTowards(follower.transform.position, hover, speed * Time.deltaTime/10);
            if (distance > maxDistFromPlayer)
            {
                follower.transform.position =
                    Vector2.MoveTowards(follower.transform.position, new Vector2(toFollow.transform.position.x, transform.position.y), speed * Time.deltaTime);
            }

            toFollow = follower;
        }
        UpdateDustCloud();
    }
}