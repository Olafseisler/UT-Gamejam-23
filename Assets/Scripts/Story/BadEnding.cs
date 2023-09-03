using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
public class BadEnding : MonoBehaviour
{
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
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
