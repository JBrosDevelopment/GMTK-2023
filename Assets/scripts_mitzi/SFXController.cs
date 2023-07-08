using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour
{
    public SoundEffect[] sfx;
    public float maxHearingD;
    bool outOfRange;
    [SerializeField] bool mute;
    private void Update()
    {
        if (maxHearingD > 0)
            if (Vector2.Distance(transform.position, GameObject.Find("player").transform.position) > maxHearingD) outOfRange = true;
            else outOfRange = false;
    }
    public void Play(string name)
    {
        if (!outOfRange && !mute) GetSFX(name).Play();
    }
    public void Stop(string name)
    {
        GetSFX(name).Stop();
    }
    public void Pause(string name)
    {
        GetSFX(name).Pause();
    }
    public AudioSource GetSFX(string name)
    {
        List<AudioSource> sfxs = new List<AudioSource>();
        for (int i = 0; i < sfx.Length; i++)
        {
            if (sfx[i].GetName() == name) sfxs.Add(sfx[i].GetAoudioS());
        }
        if (sfxs.Count == 0) return sfx[0].GetAoudioS();
        else return sfxs[Random.Range(0, sfxs.Count)];
    }
    public IEnumerator PlayFor_(AudioSource sfx, float sec)
    {
        sfx.Play();
        yield return new WaitForSeconds(sec);
        sfx.Stop();
    }
    public IEnumerator PlayFor_(string sfx, float sec)
    {
        GetSFX(sfx).Play();
        yield return new WaitForSeconds(sec);
        GetSFX(sfx).Stop();
    }
}
