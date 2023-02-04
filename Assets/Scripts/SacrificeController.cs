using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SacrificeController : MonoBehaviour
{
    [SerializeField] private Transform sacrificeSlotsParent;
    [SerializeField] private Sacrifice[] sacrifices;
    [SerializeField] private Transform[] sacrificeUISlots;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // Display the serialized sacrifice objects data in the menu
        for (var i = 0; i < sacrificeSlotsParent.childCount; i++)
        {
            if (sacrifices.Length <= i)
            {
                break;
            }
            sacrificeSlotsParent.GetChild(i).Find("NameText").GetComponent<TMP_Text>().text = sacrifices[i].sacrifice_name;
            sacrificeSlotsParent.GetChild(i).GetComponent<Image>().sprite = sacrifices[i].artwork;
            sacrificeSlotsParent.GetChild(i).GetComponent<Image>().color = Color.white;
            sacrificeSlotsParent.GetChild(i).Find("CostText").GetComponent<TMP_Text>().text = $"${sacrifices[i].cost.ToString()}";
        }
        
        
    }
}
