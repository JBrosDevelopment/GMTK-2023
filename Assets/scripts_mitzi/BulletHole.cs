using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] sprites;

    void Awake()
    {
        int i = Random.Range(0, sprites.Length);
        sr.sprite = sprites[i];
    }

}
