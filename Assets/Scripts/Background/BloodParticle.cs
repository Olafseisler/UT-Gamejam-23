using UnityEngine;
using JSAM;
public class BloodParticle : MonoBehaviour
{

    public void ShowBlood()
    {
        transform.gameObject.SetActive(true);
    }
    public void OnStart()
    {
        AudioManager.PlaySound(Sounds.death);
    }
    public void OnEnd()
    {
        transform.gameObject.SetActive(false);
    }
}
