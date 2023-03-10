using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using JSAM;
public class SacrificeController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform sacrificeSlotsParent;
    [SerializeField] private Sacrifice[] sacrifices;
    [SerializeField] private Transform[] sacrificeUISlots;
    [SerializeField] private Transform dialogueBoxParent;
    [SerializeField] private Sprite monke_sprite;
    [SerializeField] private BloodParticle followerBlood;
    [SerializeField] private GameObject MobileUI;
    private Image dialogue_avatar;
    private TMP_Text name_text;
    private TMP_Text message_text;

    private float selectedSlowDown;
    
    // Start is called before the first frame update
    void Start()
    {

        // Display the serialized sacrifice objects data in the menu
        for (var i = 0; i < sacrificeSlotsParent.childCount; i++) // reset all multipliers on game start
        {
            if (sacrifices.Length <= i)
            {
                break;
            }
            sacrifices[i].multiplier = 1;
        }
        dialogue_avatar = dialogueBoxParent.GetChild(0).Find("AvatarPicture").gameObject.GetComponent<Image>();
        name_text = dialogueBoxParent.GetChild(0).Find("Name").gameObject.GetComponent<TMP_Text>();
        message_text = dialogueBoxParent.GetChild(0).Find("Message").gameObject.GetComponent<TMP_Text>();
    }
    public void UpdatePrices()
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
            sacrificeSlotsParent.GetChild(i).Find("CostText").GetComponent<TMP_Text>().text = $"${(sacrifices[i].cost * sacrifices[i].multiplier).ToString()}";
        }
    }
    public void SelectSacrifice(SacrificeSlot sacrificeSlot)
    {
        
        Sacrifice sacrificed = sacrificeSlot.GetSacrifice();
        
        if (sacrificeSlot.GetSacrifice() != null)
            sacrificed.multiplier = gameManager.HandleCommitSacrifice(sacrificed.cost, sacrificed.multiplier);
        selectedSlowDown = sacrificed.slowdown;
        
        if (gameManager.GetMoney() > 0)
        {
            // i know this code is shit
            dialogueBoxParent.gameObject.SetActive(true);
            //gameManager.OnGameStateChanged(GameState.Running);
            StartCoroutine(Dialogue(sacrificed));
        }
        enableInput();
 

    }

    void character_to_monke()
    {
        dialogue_avatar.sprite = monke_sprite;
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        name_text.text = "Monkey";
    }

    void character_to_chosen(Sacrifice sacrifice)
    {
        dialogue_avatar.sprite = sacrifice.artwork;
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        name_text.text = sacrifice.sacrifice_name;
    }

    void enableInput()
    {
        if (Application.isMobilePlatform)
        {
            Debug.Log("mobile!");
            MobileUI.SetActive(true);
        }
    }
    // shit code but i am too tired
    IEnumerator Dialogue(Sacrifice sacrifice)
    {
        character_to_monke();
        message_text.text = sacrifice.dialogue[0];;
        yield return new WaitForSecondsRealtime(5f);
        character_to_chosen(sacrifice);
        message_text.text = sacrifice.dialogue[1];
        yield return new WaitForSecondsRealtime(4f);
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameManager.handleEnemySlowdown(selectedSlowDown);
        message_text.text = "RIP IN PEACE";
        followerBlood.ShowBlood();
        yield return new WaitForSecondsRealtime(1f);
        dialogueBoxParent.gameObject.SetActive(false);
        gameManager.OnGameStateChanged(GameState.Running);
        if (sacrifice.sacrifice_name.Equals("Nobody"))
        {
            gameManager.OnGameStateChanged(GameState.Lose);
        }
    }

}
