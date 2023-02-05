using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JSAM;
using UnityEngine.SceneManagement;
public class GoodEndController : MonoBehaviour
{
    [SerializeField] GameObject playerGood;
    [SerializeField] GameObject playerOK;
    [SerializeField] TMP_Text moneyBarText;
    int score;
    [SerializeField] int trueEndThreshold;
    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        if (score >= trueEndThreshold)
        {
            playerGood.gameObject.SetActive(true);
        }
        else
        {
            playerOK.gameObject.SetActive(true);
        }
        moneyBarText.text = "" + score;
        StartCoroutine(Victory());
    }

    IEnumerator Victory()
    {
        yield return new WaitForSecondsRealtime(6f);
        AudioManager.StopMusic();
        SceneManager.LoadScene("Credits");
    }
}
