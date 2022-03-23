using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public PlayerDataObject LevelPlayerSettings;
    public static LevelManager Util;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform PlayerSpawnPoint;
    [SerializeField] Transform PlayerEndgamePoint;

    [SerializeField] float OOBLimit = -50;

    [SerializeField] bool _canEndgame;
   
    Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        if(Util == null)
        {
            Util = this;
        }

        GameObject Player = Instantiate(playerPrefab);
        //s_player = Player.transform;
        PlayerManager playerManager = Player.GetComponentInChildren<PlayerManager>();
        _player = playerManager.transform;
        playerManager.PlayerSettings = LevelPlayerSettings;
        Player.transform.position = PlayerSpawnPoint.transform.position;

        
    }
    public bool CanEndgame
    {
        get{ return _canEndgame;}
        set{ _canEndgame = value;}
    }
    public Transform GetPlayerSpawnPoint()
    {
        return PlayerSpawnPoint;
    }

    public float GetOOBLimit()
    {
        return OOBLimit;
    }

    public void Endgame()
    {
        Debug.Log("Endgame");
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if(_player.position.y < OOBLimit)
            _player.GetComponent<PlayerManager>().death = true;
    }
}
