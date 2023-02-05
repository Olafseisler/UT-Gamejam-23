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

    // Update is called once per frame
    void Update()
    {
        money = gameManager.GetMoney();
        text.SetText("" + money);
    }
}
