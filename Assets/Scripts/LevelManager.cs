using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    public RoomTestObject LevelData;

    public static LevelManager Util;
    [SerializeField] Transform PlayerSpawnPoint;
    [SerializeField] Transform PlayerEndgamePoint;

    float durationToNextExplanatation = -1;
    float timer;

    float endgameCooldown = 0;

    bool _canEndgame;
    bool endgameTriggered;
    Transform _player;
    bool _firstSpawn;
    bool _isPaused;

    
    [Tooltip("undefined")]
    public UnityAction Test;
      public UnityAction action;
    public UnityEvent myEvent;
    void Awake()
    {
        // Permet de récuperer le component en static
        if(Util == null)
        {
            Util = this;
        }
        if(LevelData == null)
        {
            Debug.LogError("Attention le LevelData dans le levelManager de la scène n'a pas été configuré !");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        

        

        GameObject Player = Instantiate(LevelData.PlayerPrefab,PlayerSpawnPoint.position, Quaternion.identity);
        PlayerManager playerManager = Player.GetComponentInChildren<PlayerManager>();
        FirstPersonController playerController = Player.GetComponentInChildren<FirstPersonController>();


        playerController.JumpHeight = LevelData.LevelPlayerSetting.JumpHeight;
        playerController.JumpMax = LevelData.LevelPlayerSetting.MaxJump;
        _player = playerManager.transform;
        playerManager.PlayerSettings = LevelData.LevelPlayerSetting;
        playerController.PlayerSettings= LevelData.LevelPlayerSetting;

        // On desactive le playerController car empeche de faire un set positions
        playerController.enabled = false;
        playerController.transform.position = PlayerSpawnPoint.transform.position;
        playerController.enabled = true;

        // Joue le dialogue de début de partie
        if(LevelData.StartingDialogue != "")
        {
            Aliase snd = AudioManager.PlaySoundAtPosition(LevelData.StartingDialogue, transform.position);

            if(LevelData.ExplanationDialogue != "")
                durationToNextExplanatation = snd.audio[0].length;

            Aliase sndEndgame = AudioManager.GetSoundByAliase(LevelData.EndgameDialogue);
            if(sndEndgame != null)
                endgameCooldown = sndEndgame.audio[0].length+1;
        }
        

        _canEndgame = LevelData.CanEndgame;

        LevelData.myEvent.Invoke();

        // On desactive le playerController car empeche de faire un set positions
        playerController.enabled = false;
        playerController.transform.position = PlayerSpawnPoint.transform.position;
        playerController.enabled = true;
    }

    // Permet de savoir sur le joueur peut sortir du niveau
    public bool CanEndgame
    {
        get{ return _canEndgame;}
        set{ _canEndgame = value;}
    }
    // Help to know if we are in endgame phase, useful to block dialogue when the player fall
    public bool IsEndgame
    {
        get{return endgameTriggered;}
        set{endgameTriggered = value;}
    }

    // Help to know if we are in pause phase
    public bool IsPaused
    {
        get
        {
            // Remove the pause when the endgame is on
            if(IsEndgame)
                _isPaused = false;



            return _isPaused;
        }

        set{

            _isPaused = value;
            
            }
    }
    // Renvoi le point de spawn initial du joueur
    public Transform GetPlayerSpawnPoint()
    {
        return PlayerSpawnPoint;
    }
    // Renvoi les limits du hors map 
    public float GetOOBLimit()
    {
        return LevelData.OOBLimit;
    }
    // Start the endgame to return on main menu
    public void Endgame()
    {
        AudioManager.PlaySoundAtPosition(LevelData.EndgameDialogue, transform.position);
        endgameTriggered = true;
    }

    void WatchPause()
    {
        if(IsPaused)
        {
            Time.timeScale = 0;
            AudioManager.Util.IsPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            AudioManager.Util.IsPaused = false;
                        Cursor.visible = false;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    // Update is called once per frame
    void Update()
    {
        WatchPause();

        if( durationToNextExplanatation != -1)
        {
            if(timer <= durationToNextExplanatation)
            {
                timer += Time.deltaTime; 
            }
            else
            {
                AudioManager.PlaySoundAtPosition(LevelData.ExplanationDialogue, transform.position);
                durationToNextExplanatation = -1;
            }
        }
        if(endgameTriggered){
             if(endgameCooldown >= 0)
            {
                endgameCooldown -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("MenuStart");
            }
        }
       
            
    }
}
