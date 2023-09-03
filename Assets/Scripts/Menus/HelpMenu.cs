using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    [SerializeField] GameObject normalUI;
    [SerializeField] GameObject mobileUI;
    // Start is called before the first frame update
    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            mobileUI.SetActive(true);
        }
        else
        {
            normalUI.SetActive(true);
        }
    }

}
