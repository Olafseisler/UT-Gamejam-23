using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
public class AudioWrapper : MonoBehaviour
{
    public void ClickSound()
    {
        AudioManager.PlaySound(Sounds.click);
    }

    public void DeathSound()
    {
        AudioManager.PlaySound(Sounds.death);
    }
}
