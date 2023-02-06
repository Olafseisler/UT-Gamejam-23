using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
public class BloodParticle : MonoBehaviour
{

    public void ShowBlood()
    {
        transform.gameObject.SetActive(true);
    }
    void OnStart()
    {
        AudioManager.PlaySound(Sounds.death);
    }
    void OnEnd()
    {
        transform.gameObject.SetActive(false);
    }
}
