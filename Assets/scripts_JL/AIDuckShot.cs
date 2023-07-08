using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuckShot : MonoBehaviour
{
    // WHEN AI DUCK GETS SHOT, PLAY ANIMATION
    public GameObject gunshot;

    public void Shot(Vector2 pos)
    {
        Instantiate(gunshot, pos, Quaternion.identity, transform);
    }
}
