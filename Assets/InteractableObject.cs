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
[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] TypeInteracbleObject Type;
    bool _isGrabbed;


    public bool Grabable
    {
        get{    if(Type == TypeInteracbleObject.Grabable )
                    return true;
                return false; }
    }

    public bool isGrabbed
    {
        get{ return _isGrabbed;}
        set{ _isGrabbed = value;}
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
