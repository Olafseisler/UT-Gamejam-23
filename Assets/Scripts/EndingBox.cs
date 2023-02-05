using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingBox : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        // gameManager.OnGameStateChanged(GameState.Win);
        Debug.Log("Entered win area ");
        SceneManager.LoadScene(5);
    }
}
