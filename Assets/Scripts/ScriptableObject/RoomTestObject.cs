using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level Data", menuName = "RoomTest/Level", order = 1)]
public class RoomTestObject : ScriptableObject
{
    [Tooltip("Name of the level, it will be used for menu etc")]
    public string Name;
    [Tooltip("Description of the level, it will be used for menu etc")]
    public string Description;
    [Tooltip("PreviewImage of the level, it will be used for menu etc")]
    public Sprite PreviewImage;
    [Tooltip("Bool to see if the player can start this level")]
    public bool isUnlocked;
    [Tooltip("Scene name of the level")]
    public string sceneName;

    [Tooltip("Player setting used for this level")]
    public PlayerDataObject LevelPlayerSetting;
    [Tooltip("Prefab used to spawn the player, that can be usefull if a level need a specific composition")]
    public GameObject PlayerPrefab;
    [Tooltip("Limit of the map, before the player is teleported on his initial spawn")]
    public float OOBLimit = -3;
    [Tooltip("Bool to see if the player can trigger the endgame trigger at the beginning or he need to resolve some condition")]
    public bool CanEndgame;

    [Header("Aliases")]
    [Tooltip("Starting dialogue when the player start the level")]
    public string StartingDialogue;
    [Tooltip("Explanation dialogue after the starting dialogue")]
    public string ExplanationDialogue;
     [Tooltip("Endgame dialogue")]
    public string EndgameDialogue;

}
