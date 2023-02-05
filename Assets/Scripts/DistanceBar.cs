using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DistanceBar : MonoBehaviour
{
    [SerializeField] private const float SAFE_DISTANCE = 200f;
    public float distance = SAFE_DISTANCE;
    private Image distanceBar;
    // Start is called before the first frame update
    void Start()
    {
        distanceBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceBar.fillAmount = distance / SAFE_DISTANCE;
    }
}
