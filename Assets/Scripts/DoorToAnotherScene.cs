using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
public class DoorToAnotherScene : MonoBehaviour
{
    public GameObject fadeOut;
    public string nextScene;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (nextScene.Equals("GoodEnd")) {
            PlayerPrefs.SetInt("SecretEnd", 1);
        }
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelaySceneLoad(2f, nextScene));
        }
    }

    IEnumerator DelaySceneLoad(float delay, string scene)
    {
        AudioManager.FadeMusicOut(delay);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }
}
