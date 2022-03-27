using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

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

    // Start is called before the first frame update
    void Start()
    {
        // Permet de récuperer le component en static
        if(Util == null)
        {
            Util = this;
        }

        GameObject Player = Instantiate(LevelData.PlayerPrefab);
        PlayerManager playerManager = Player.GetComponentInChildren<PlayerManager>();
        FirstPersonController playerController = Player.GetComponentInChildren<FirstPersonController>();
        playerController.JumpHeight = LevelData.LevelPlayerSetting.JumpHeight;
        playerController.JumpMax = LevelData.LevelPlayerSetting.MaxJump;
        _player = playerManager.transform;
        playerManager.PlayerSettings = LevelData.LevelPlayerSetting;

        // On desactive le playerController car empeche de faire un set positions
        playerController.enabled = false;
        playerController.transform.position = PlayerSpawnPoint.transform.position;
        playerController.enabled = true;

        // Crée une boite de dialogue sur le monde 3D
        UIManager.CreateHintString(PlayerEndgamePoint.gameObject, LevelData.Description ,6666 );
        // Joue le dialogue de début de partie
        Aliase snd = AudioManager.PlaySoundAtPosition(LevelData.StartingDialogue, transform.position);
        if(LevelData.ExplanationDialogue != null)
            durationToNextExplanatation = snd.audio[0].length;

        Aliase sndEndgame = AudioManager.GetSoundByAliase(LevelData.EndgameDialogue);
        if(sndEndgame != null)
            endgameCooldown = sndEndgame.audio[0].length+1;

    }

    // Permet de savoir sur le joueur peut sortir du niveau
    public bool CanEndgame
    {
        get{ return _canEndgame;}
        set{ _canEndgame = value;}
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
    // Renvoi au menu principal
    public void Endgame()
    {
        AudioManager.PlaySoundAtPosition(LevelData.EndgameDialogue, transform.position);
        endgameTriggered = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        
        if(endgameTriggered && endgameCooldown >= 0)
        {
            endgameCooldown -= Time.deltaTime;
        }
        else
        {
             SceneManager.LoadScene("MenuStart");
        }
            
    }
}
