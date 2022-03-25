using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeFormeDansLesTrous : MonoBehaviour
{

    [SerializeField] GameObject[] ItemNeeded;
    [SerializeField] GameObject[] WallToDestroyed;
    
    bool dialogueExplanation;
    
    // Start is called before the first frame update
    void Start()
    {
         AudioManager.PlaySoundAtPosition("forme_beginning", transform.position);
        for(int i = 0 ; i < ItemNeeded.Length; i++)
        {
           
            //UIManager.CreateHintString(ItemNeeded[i], "Press [E] to grab." , 50);
             
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool finished = false;
        for(int i = 0 ; i < ItemNeeded.Length; i++)
        {
            if(ItemNeeded[i] == null)
            {
                finished = true;
            }
            else
            {
                finished = false;
                break;
            }
        }
        //Debug.Log("Challenge state : "+finished);
        if(finished == true && !LevelManager.Util.CanEndgame)
        {
            LevelManager.Util.CanEndgame = true;
            for(int i = 0 ; i < WallToDestroyed.Length; i++)
            {
                WallToDestroyed[i].SetActive(false);
            }
        }
            

    }
    void OnTriggerStay(Collider other)
    {
        for(int i = 0 ; i < ItemNeeded.Length; i++)
        {
            if(other.gameObject == ItemNeeded[i] && !ItemNeeded[i].GetComponent<InteractableObject>().isGrabbed)
            {
                Debug.Log("The forme is on the trigger");
                ItemNeeded[i] = null;
            }
        }   
    }
}
