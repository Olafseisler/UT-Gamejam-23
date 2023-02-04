using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public GameObject[] followers;

    private float distance;
    
    // Start is called before the first frame update
    void Start()
    {
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
            if (distance > 2)
            {
                follower.transform.position =
                    Vector2.MoveTowards(follower.transform.position, new Vector2(toFollow.transform.position.x, transform.position.y)   , speed * Time.deltaTime);
            }

            toFollow = follower;
        }
        
    }
}
