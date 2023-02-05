using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
public class BadEnding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.StopMusic();
        //AudioManager.PlayMusic();
        StartCoroutine(SilenceForTheFallen());
    }
    IEnumerator SilenceForTheFallen()
    {
        yield return new WaitForSecondsRealtime(6f);
        AudioManager.StopMusic();
        SceneManager.LoadScene("MainMenu");
    }
}
