using System.Collections;
using UnityEngine;

public class HuntPlayer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    public GameObject player;
    public float speed;
    private float distance;

    private bool isChasing = true;
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            return;
        }
            
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (Time.time - timer > 1.0f)
        {
            speed *= 1.1f;
            timer = Time.time;
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public float GetDistanceFromPlayer()
    {
        return distance;
    }

    public void SlowEnemyDown(float slowDown)
    {
        transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
        speed -= (speed * slowDown / 50.0f);
        speed = Mathf.Clamp(speed, 0.5f, 20.0f);
        StartCoroutine(HaltForSomeTime());
    }

    IEnumerator HaltForSomeTime()
    {
        isChasing = false;
        yield return new WaitForSecondsRealtime(3f);
        isChasing = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _gameManager.OnGameStateChanged(GameState.Sacrifice);
        }
        
        
    }
    
}
