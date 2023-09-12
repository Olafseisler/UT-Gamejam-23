using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
            sacrificeSlotsParent.GetChild(i).Find("CostText").GetComponent<TMP_Text>().text = $"${(sacrifices[i].cost * sacrifices[i].multiplier)}";
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
        EnableInput();
 

    }

    void SwitchToProtag()
    {
        dialogue_avatar.sprite = monke_sprite;
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        name_text.text = "Monkey";
    }

    void SwitchToSacrifice(Sacrifice sacrifice)
    {
        dialogue_avatar.sprite = sacrifice.artwork;
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        name_text.text = sacrifice.sacrifice_name;
    }

    void EnableInput()
    {
        MobileUI.SetActive(true);
    }

    // shit code but i am too tired
    IEnumerator Dialogue(Sacrifice sacrifice)
    {
        float elapsedTime = 0f;
        SwitchToProtag();
        message_text.text = sacrifice.dialogue[0];
        yield return null;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (Input.anyKeyDown) break;
            else yield return null;
        }
        yield return null;
        SwitchToSacrifice(sacrifice);
        message_text.text = sacrifice.dialogue[1];
        elapsedTime = 0f;
        while (elapsedTime < 4f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (Input.anyKeyDown) break;
            else yield return null;
        }
        yield return null;
        dialogue_avatar.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameManager.HandleEnemySlowdown(selectedSlowDown);
        message_text.text = "RIP IN PEACE";
        followerBlood.ShowBlood();
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (Input.anyKeyDown) break;
            else yield return null;
        }
        yield return null;
        dialogueBoxParent.gameObject.SetActive(false);
        gameManager.OnGameStateChanged(GameState.Running);
        if (sacrifice.sacrifice_name.Equals("Nobody"))
        {
            gameManager.OnGameStateChanged(GameState.Lose);
        }
    }

}
