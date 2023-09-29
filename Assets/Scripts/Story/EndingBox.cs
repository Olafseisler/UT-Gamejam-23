using UnityEngine;

public class EndingBox : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        gameManager.OnGameStateChanged(GameState.Win);
        Debug.Log("Entered win area ");
    }
}
