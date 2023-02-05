using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class Intro : MonoBehaviour
{
    [SerializeField] ParticleSystem gold_ps;
    [SerializeField] ParticleSystem cash_ps;
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
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
