using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyBar : MonoBehaviour
{
    public int money = 100000;
    [SerializeField] TMP_Text text;
    [SerializeField] private GameManager gameManager;
    Color green = new Color32(93, 184, 39, 255);
    Color red = new Color32(235, 30, 30, 255);

    // Update is called once per frame
    void Update()
    {
        money = gameManager.GetMoney();
        text.SetText("" + money);
        if (money > 5000)
        {
            text.color = green;
        }
        else
        {
            text.color = red;
        }
    }
}
