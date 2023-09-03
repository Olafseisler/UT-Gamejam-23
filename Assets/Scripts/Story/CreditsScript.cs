using JSAM;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    public GameObject fadeOut;
    public void OnCreditsEnd()
    {
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        fadeOut.SetActive(true);
        AudioManager.FadeMusicOut(1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
