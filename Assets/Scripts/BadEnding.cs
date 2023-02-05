using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
using UnityEngine.InputSystem;
public class BadEnding : MonoBehaviour
{
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.StopMusic();
        //AudioManager.PlayMusic();
        StartCoroutine(SilenceForTheFallen());
    }


    IEnumerator SilenceForTheFallen()
    {
        yield return new WaitForSecondsRealtime(5f);
        fadeOut.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.StopMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
