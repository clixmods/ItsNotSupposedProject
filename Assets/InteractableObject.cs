using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeInteracbleObject
{
    Trigger,
    Grabable,
    Rotation
}

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] TypeInteracbleObject Type;

    public bool Grabable
    {
        get{    if(Type == TypeInteracbleObject.Grabable )
                    return true;
                return false; }
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
