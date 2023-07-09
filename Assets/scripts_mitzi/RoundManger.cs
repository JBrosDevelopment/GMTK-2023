using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManger : MonoBehaviour
{
    public GameObject Player;
    public round[] rounds;
    public Text timer;
    public GameObject MusicPlayer;
    AudioSource source;
    public Text Text;
    public AudioClip WON;
    public AudioClip LOST;
    public AudioClip NEXTROUND;
    bool lost = false;

    void Start()
    {
        source = MusicPlayer.GetComponent<AudioSource>();
        Instantiate(Player);
        StartCoroutine(waves());
    }
    IEnumerator waves()
    {
        int j = 0;
        foreach(round r in rounds)
        {
            if (lost) yield return null;
            j++;
            Text.text = "READY?";
            yield return new WaitForSeconds(1f);
            Text.text = "";
            source.clip = r.Music;
            source.Play();
            GameObject man = Instantiate(r.man, transform.position, Quaternion.identity);
            for (int i = 0; i < r.time; i++)
            {
                yield return new WaitForSeconds(1);
                timer.text = (r.time - i).ToString();
            }
            Destroy(man);
            source.Stop();

            if (j < rounds.Length - 1)
            {
                source.clip = NEXTROUND;
                source.Play();
                Text.text = "NEXT ROUND!";
            }
            yield return new WaitForSeconds(3f);
        }
        source.clip = WON;
        source.Play();
        Text.text = "YOU WON!";
    }
    public void Lost()
    {
        lost = true;
        Text.text = "YOU LOST";
        source.Stop();
        source.clip = LOST;
        source.Play();
    }
}
