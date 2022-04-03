using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] bool PlaySoundInThisPosition;
    [SerializeField] string aliaseName;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlaySoundAtPosition(aliaseName,transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
