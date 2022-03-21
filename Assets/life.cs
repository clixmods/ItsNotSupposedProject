using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life : MonoBehaviour
{
    public int pv = 3;
    bool poison = false;
    public float timePv = 5;
    public float timePoison = 1;
    public bool death;
    public GameObject respawn;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (!poison && pv < 3)
        {
            timePv -= Time.deltaTime;
            if (timePv <= 0)
            {
                pv++;
                timePv = 3;
            }
        }
        if (poison)
        {
            timePoison -= Time.deltaTime;
            if (timePoison <= 0)
            {
                Debug.Log(pv);
                pv--;
                timePoison = 1;
            }
        }
        if (pv <= 0)
        {
            death = true;
        }
        if(death)
        {
            Debug.Log("is mort");
            player.transform.position = respawn.transform.position;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("coli lave");
            poison = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("plus coli lave");
            poison = false;
        }
    }
}