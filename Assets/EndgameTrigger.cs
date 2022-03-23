using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    BoxCollider trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.Util.CanEndgame)    
        {
            trigger.enabled = false;
        }    
        else
            trigger.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.Util.Endgame();
        }   
    }
}
