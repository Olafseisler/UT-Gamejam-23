using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;
public class Credits : MonoBehaviour
{
    private void Start()
    {
        AudioManager.PlayMusic(Music.credits_song);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCreditsEnd();
        }
    }
    // Start is called before the first frame update
    public void OnCreditsEnd()
    {
        StartCoroutine(CreditsEnd());
    }

    IEnumerator CreditsEnd()
    {
        AudioManager.FadeMusicOut(1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
