using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class Intro : MonoBehaviour
{
    [SerializeField] ParticleSystem gold_ps;
    [SerializeField] ParticleSystem cash_ps;
    public GameObject fadeOut;
    private void Start()
    {
        
    }
    void OnIntroEnd()
    {
        StartCoroutine(IntroEnd());
    }


    IEnumerator IntroEnd()
    {
        gold_ps.Play();
        cash_ps.Play();
        AudioManager.PlaySound(Sounds.intro_boom);
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }
}
