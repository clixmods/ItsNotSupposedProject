using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Debug.Log("dfghjkl");
            LevelManager.Util.Endgame();
        }
    }
}
