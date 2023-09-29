using System.Collections;
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
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("SecretEnd", 0) == 1)
        {
            moneyBarText.text = "TRUE END";
        }
        else
        {
            score = PlayerPrefs.GetInt("Score", 0);
            if (score >= trueEndThreshold)
            {
                playerGood.SetActive(true);
            }
            else
            {
                playerOK.SetActive(true);
            }
            moneyBarText.text = "" + score;
        }
        StartCoroutine(Victory());
    }

    IEnumerator Victory()
    {
        yield return new WaitForSecondsRealtime(4f);
        fadeOut.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.StopMusic();
        SceneManager.LoadScene("Credits");
    }
}
