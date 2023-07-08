using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SoundEffect
{
    [SerializeField] private AudioSource sfx;
    [SerializeField] private string name = "";
    public string GetName() { return name; }
    public AudioSource GetAoudioS() { return sfx; }
}
