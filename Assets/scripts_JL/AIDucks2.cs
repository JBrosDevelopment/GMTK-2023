using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDucks2 : MonoBehaviour
{
    public List<GameObject> Ducks;
    public float speed = 5;
    public float SpeedRange = 1;
    public float bob_speed = 2;
    public float Min_Height = 0.1f;
    public float Max_Height = 1.1f;
    List<int> states = new List<int>();
    public float Starts = -3.75f;
    public float Finishes = 3.75f; 

    private void Start()
    {
        GameObject[] ducks = GameObject.FindGameObjectsWithTag("bgDuck");
        foreach(GameObject d in ducks)
        {
            Ducks.Add(d);
        }
        for (int i = 0; i < Ducks.Count; i++)
        {
            states.Add(1);
        }
    }

    int update = 0;
    float sp = 0;
    void Update()
    {
        update++;
        if(update > 100) sp = Random.Range(speed - SpeedRange, speed + SpeedRange);
        
        for (int i = 0; i < Ducks.Count; i++)
        {
            int state = states[i];
            GameObject duck = Ducks[i];      
            Vector2 duckPos = duck.transform.position;
            if (duckPos.y < Min_Height) state = 1;
            if (duckPos.y > Max_Height) state = 2;
            float bob = state == 1 ? bob_speed : -bob_speed;
            if (duckPos.x > Finishes)
            {
                //golden duck chance;
                int c = Random.Range(0, 10);
                if (c == 0) duck.GetComponent<AIDuckShot>().ChangeGold();
                //teleport back
                duckPos = new Vector2(Starts, duckPos.y);
            }
            Vector2 NewPos = NewPos = new Vector2(duckPos.x + sp * Time.deltaTime, duckPos.y + bob * Time.deltaTime);
            states[i] = state;
            duck.transform.position = NewPos;
        }
    }
}
