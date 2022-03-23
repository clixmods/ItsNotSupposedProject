using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerManager : MonoBehaviour
{
    
    public PlayerDataObject PlayerSettings;

    public int CurrentHealth = 3;
    public float timePv = 5;
   
    public bool death;
    public bool revive=false;

    public bool poison = false;
    public float timePoison = 1;
   

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = PlayerSettings.StartHealth;
        timePv = PlayerSettings.timeRegenHealth;
        timePoison = PlayerSettings.timePoison;
        transform.position = LevelManager.Util.GetPlayerSpawnPoint().position; 
    }

    // Update is called once per frame
    void Update()
    {
        WatchHealth();
    }

    void WatchHealth()
    {
        if (poison)
        {
            timePoison -= Time.deltaTime;
            if (timePoison <= 0)
            {
                Debug.Log(CurrentHealth);
                CurrentHealth--;
                timePoison =  PlayerSettings.timePoison;
            }
        }
        if(death)
        {
            // Il faut le disabled car il empeche de faire des set de transform;
            transform.GetComponent<FirstPersonController>().enabled = false; 
            
            transform.position = LevelManager.Util.GetPlayerSpawnPoint().position; 
            death = false;
            CurrentHealth = PlayerSettings.StartHealth;
            poison = false;

            transform.GetComponent<FirstPersonController>().enabled = true; 
           
        }
    

        if ( CurrentHealth < PlayerSettings.StartHealth)
        {
            timePv -= Time.deltaTime;
            if (timePv <= 0)
            {
                CurrentHealth++;
                timePv = PlayerSettings.timeRegenHealth;
            }
        }
    }

    void DoDamage(int amount)
    {
        if(CurrentHealth > 0)
            CurrentHealth -= amount;
    
    }

   

         // On check si il est dedans, c'est plus sur car si il sort sans qu'il passe dans 
    // le triggerexit et beh le poison reste true
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Debug.Log("Player touch the trigger");
            poison = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log("Player dont touch the trigger");
            poison = false;
            timePoison = PlayerSettings.timePoison;
        }
    }
}