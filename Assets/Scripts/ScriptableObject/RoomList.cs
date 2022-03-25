using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level List", menuName = "RoomTest/LevelList", order = 2)]
public class RoomList: ScriptableObject
{
    [Tooltip("Map list available, the order is important for the progression")]
    public RoomTestObject[] ListRooms;

}
