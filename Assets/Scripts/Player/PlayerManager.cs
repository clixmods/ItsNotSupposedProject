using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerManager : MonoBehaviour
{
    
    public PlayerDataObject PlayerSettings;

    float CurrentHealth = 3;
    float timePv = 5;
   
    public bool death;
    bool _isFalling;
    bool revive=false;

    bool poison = false;
    float timePoison = 1;

  


   

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
        WatchOOB();   
    }
    private void FixedUpdate() 
    {   
        // we check the poison and health here because the poison use physics from unity and for more
        // precision its safe to use fixed update 
        PoisonedCheck();
        WatchHealth();
    }
    // Watch if the player falls under the playable area, if true we respawn him in his initial spawn
    void WatchOOB()
    {
        if(LevelManager.Util.IsEndgame)
            return;

        if(transform.position.y < LevelManager.Util.GetOOBLimit() && !death )
        {
            death = true;
            _isFalling=true;
        }
    }
    // Check if the player touch a dangerouslayer (set in PlayerSettings)
    private void PoisonedCheck()
	{
		float GroundedOffset = -0.14f;
        float GroundedRadius = 0.5f;
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        poison = Physics.CheckSphere(spherePosition, GroundedRadius, PlayerSettings.DangerousLayers, QueryTriggerInteraction.Ignore);     
	}
    // This function check the health of the player
    void WatchHealth()
    {
        UIManager.OverlayBlood(CurrentHealth, PlayerSettings.StartHealth);

        if(CurrentHealth <= 0)
            death = true;
            
        if (poison)
        {
            timePoison -= Time.deltaTime;
            if (timePoison <= 0)
            {
                Debug.Log(CurrentHealth);
                CurrentHealth -= 0.50f;
                timePoison =  PlayerSettings.timePoison;
            }
        }
        if(death)
        {
            // Il faut le disabled car il empeche de faire des set de transform;
            transform.GetComponent<FirstPersonController>().enabled = false; 
            
            transform.position = LevelManager.Util.GetPlayerSpawnPoint().position; 
           
            CurrentHealth = PlayerSettings.StartHealth;
            poison = false;

            transform.GetComponent<FirstPersonController>().enabled = true; 
             death = false;

            if(_isFalling)
                AudioManager.PlaySoundAtPosition("plr_fall_"+Random.Range(0,5), transform.position);
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
        Debug.Log("f");

        if(other.gameObject.layer == 6)
        {
            Debug.Log("fuck");
            poison = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            poison = false;
            timePoison = PlayerSettings.timePoison;
        }
    }
}
