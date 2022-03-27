using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeFormeDansLesTrous : MonoBehaviour
{

    [SerializeField] GameObject[] ItemNeeded;
    [SerializeField] GameObject[] WallToDestroyed;
    
    [Header("Event Dialogue")]
    [SerializeField]bool dialogueExplanation;
    [SerializeField]bool cubePosed;
    [SerializeField]bool formPosed;
    [SerializeField]bool formPosedB;
    [SerializeField] float timeAnnoying = 30;
    [SerializeField] float timer;

    // Start is called before the first frame update
    void Start()
    {
         
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
        for(int i = 0 ; i < ItemNeeded.Length; i++)
        {
            if(ItemNeeded[i] == null)
            { 
                if(i == 0 && !cubePosed)
                {
                    AudioManager.PlaySoundAtPosition("forme_cube_to_cube",transform.position);
                    cubePosed = true;
                }   
                if(i != 0 && !formPosed) 
                {
                    AudioManager.PlaySoundAtPosition("forme_first_to_wrong_hole",transform.position);
                    formPosed = true;
                }
                if(i != 0 && formPosed && !formPosedB)
                {
                    AudioManager.PlaySoundAtPosition("forme_are_u_sure", transform.position);
                    formPosedB = true;
                }
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
            
        if(timer <= timeAnnoying)
        {
            timer += Time.deltaTime;
        }
        else
        {
            AudioManager.PlaySoundAtPosition("forme_take_your_time", transform.position);
            timer = 0;
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
