using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Sacrifice", menuName = "Sacrifice")]
[Serializable]
public class Sacrifice : ScriptableObject
{
    public string sacrifice_name;
    public int cost;
    public Sprite artwork;
    public float slowdown;
    public int power;
    public string[] dialogue;
    public float multiplier;
}
