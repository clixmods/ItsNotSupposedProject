using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    BoxCollider trigger;
    [SerializeField] GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        WatcherEndgame();
      
    }

    void WatcherEndgame()
    {
        if(trigger != null)
        {
            if(!LevelManager.Util.CanEndgame)    
            {
                flag.SetActive(false);
                trigger.enabled = false;
            }    
            else
            {
                trigger.enabled = true;
                flag.SetActive(true);
            }
                
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.Util.Endgame();
            Destroy(trigger);
        }   
    }
}
