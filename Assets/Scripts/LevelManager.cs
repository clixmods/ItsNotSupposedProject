using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerDataObject LevelPlayerSettings;
    public static LevelManager Util;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform PlayerSpawnPoint;
    [SerializeField] Transform PlayerEndgamePoint;
   

    // Start is called before the first frame update
    void Start()
    {
        if(Util == null)
        {
            Util = this;
        }

        GameObject Player = Instantiate(playerPrefab);
        PlayerManager playerManager = Player.GetComponentInChildren<PlayerManager>();
        playerManager.PlayerSettings = LevelPlayerSettings;

        
    }

    public Transform GetPlayerSpawnPoint()
    {
        return PlayerSpawnPoint;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
