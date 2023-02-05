using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SacrificeController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform sacrificeSlotsParent;
    [SerializeField] private Sacrifice[] sacrifices;
    [SerializeField] private Transform[] sacrificeUISlots;
    [SerializeField] private Transform dialogueBoxParent;
    [SerializeField] private Sprite monke_sprite;
    private Image dialogue_avatar;
    private TMP_Text name_text;
    private TMP_Text message_text;
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
            // sacrificeSlotsParent.GetChild(i).Find("NameText").GetComponent<TMP_Text>().text = sacrifices[i].sacrifice_name;
            sacrificeSlotsParent.GetChild(i).GetComponent<Image>().sprite = sacrifices[i].artwork;
            sacrificeSlotsParent.GetChild(i).GetComponent<Image>().color = Color.white;
            sacrificeSlotsParent.GetChild(i).GetComponent<SacrificeSlot>().SetSacrifice(sacrifices[i]);
            sacrificeSlotsParent.GetChild(i).Find("CostText").GetComponent<TMP_Text>().text = $"${sacrifices[i].cost.ToString()}";
        }

        dialogue_avatar = dialogueBoxParent.GetChild(0).Find("AvatarPicture").gameObject.GetComponent<Image>();
        name_text = dialogueBoxParent.GetChild(0).Find("Name").gameObject.GetComponent<TMP_Text>();
        message_text = dialogueBoxParent.GetChild(0).Find("Message").gameObject.GetComponent<TMP_Text>();
    }
    
    public void SelectSacrifice(SacrificeSlot sacrifice)
    {

        Sacrifice sacrificed = sacrifice.GetSacrifice();
        if (sacrifice.GetSacrifice() != null)
            gameManager.RemoveMoney(sacrifice.GetSacrifice().cost);

        // i know this code is shit
        dialogueBoxParent.gameObject.SetActive(true);
        //gameManager.OnGameStateChanged(GameState.Running);
        StartCoroutine(Dialogue(sacrificed));

    }

    void character_to_monke()
    {
        dialogue_avatar.sprite = monke_sprite;
        name_text.text = "Monkey";
    }

    void character_to_chosen(Sacrifice sacrifice)
    {
        dialogue_avatar.sprite = sacrifice.artwork;
        name_text.text = sacrifice.sacrifice_name;
    }

    // shit code but i am too tired
    IEnumerator Dialogue(Sacrifice sacrifice)
    {
        character_to_monke();
        message_text.text = sacrifice.dialogue[0];
        Debug.Log("entering the funny");
        yield return new WaitForSecondsRealtime(5f);
        character_to_chosen(sacrifice);
        message_text.text = sacrifice.dialogue[1];
        yield return new WaitForSecondsRealtime(5f);
        dialogueBoxParent.gameObject.SetActive(false);
        gameManager.OnGameStateChanged(GameState.Running);
    }

}
