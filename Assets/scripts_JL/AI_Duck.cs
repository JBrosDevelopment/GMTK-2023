using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AI_Duck : MonoBehaviour
{
    public GameObject Duck;
    [Range(1, 20)] public int AI_Ducks;
    public float Speed;
    public float Bob_Speed;
    public float XRandomSeperation_MIN;
    public float XRandomSeperation_MAX;
    public float HeightCap;
    public float MaxWidth__Background;
    public float MinWidth__Background;
    List<GameObject> ducks = new List<GameObject>();
    public Vector2 Offset;

    public void Start()
    {
        for (int i = 0; i < ducks.Count; i++)
        {
            DestroyImmediate(ducks[i]);
        }
        ducks.Clear();
        float Xplacement = 0;
        float Yplacement = 0;
        for (int i = 0; i < AI_Ducks; i++)
        {
            ducks.Add(Instantiate(Duck,gameObject.transform));
        }
        for (int i = 0; i < ducks.Count; i++)
        {
            Xplacement += Random.Range(XRandomSeperation_MIN, XRandomSeperation_MAX);
            Yplacement = Random.Range(0, HeightCap);
            Xplacement = Mathf.Clamp(Xplacement, MinWidth__Background, MaxWidth__Background);
            Vector2 pos = new Vector2(Xplacement, Yplacement);
            pos.x -= 7f;
            pos.y -= 3f;
            ducks[i].transform.position = pos;
        }
        gameObject.transform.position = new Vector2(transform.position.x - Offset.x, transform.position.y - Offset.y);
    }
    void Update()
    {
        foreach (var duck in ducks)
        {
            int up = 0;
            Vector2 pos = duck.transform.position;
            if (pos.y < 0) up = 1;
            else if (pos.y > HeightCap) up = 2;
            float bob_Speed = Bob_Speed;
            duck.transform.position = new Vector2(pos.x + Speed, pos.y + bob_Speed);
        }
    }
}