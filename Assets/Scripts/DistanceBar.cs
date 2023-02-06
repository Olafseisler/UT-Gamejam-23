using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DistanceBar : MonoBehaviour
{
    [SerializeField] private float SAFE_DISTANCE = 40f;
    [SerializeField] private HuntPlayer hunter;
    Color green = new Color32(93, 184, 39, 255);
    Color red = new Color32(235, 30, 30, 255);
    private float distance;
    private Image distanceBar;
    // Start is called before the first frame update
    void Start()
    {
        distance = SAFE_DISTANCE;
        distanceBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = hunter.GetDistanceFromPlayer();
        distanceBar.fillAmount = distance / SAFE_DISTANCE;
        if (distanceBar.fillAmount > 0.5)
        {
            distanceBar.color = green;
        }
        else
        {
            distanceBar.color = red;
        }
    }
}
