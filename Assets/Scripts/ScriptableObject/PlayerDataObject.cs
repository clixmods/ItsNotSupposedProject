using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player/Setting", order = 1)]
public class PlayerDataObject : ScriptableObject
{
    //[Tooltip("Name of the level, it will be used for menu etc")]

    //[Tooltip("Description of the level, it will be used for menu etc")]

    //[Tooltip("PreviewImage of the level, it will be used for menu etc")]


    [Tooltip("The Number of point of health of the player")]
    public int StartHealth=3;
    [Tooltip("The time before the player regain his missing health, one by one")]
    public float timeRegenHealth = 5;
    [Tooltip("The time before the player lose his pv in the lava, one by one")]
    public float timePoison = 1;

    public int MaxJump = 1;
    public float JumpHeight = 1;

    public float maxDistanceToGrab = 5;

    [Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;
    public LayerMask DangerousLayers;
}
