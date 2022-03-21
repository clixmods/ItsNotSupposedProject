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
    
    public bool isUnlocked;
    public string sceneName;

    public RoomTestObject PlayerData;

}
