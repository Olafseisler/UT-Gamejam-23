using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToAnotherScene : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Loading Scene with index {nextSceneIndex.ToString()}");
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
