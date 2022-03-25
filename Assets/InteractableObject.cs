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
[RequireComponent(typeof(MeshCollider))]

public class InteractableObject : MonoBehaviour
{
    [SerializeField] TypeInteracbleObject Type;
    bool _isGrabbed;
    Rigidbody _rb;
    MeshCollider _collider;
    Vector3 _initialPos;
    Quaternion _initialRotation;

    HintstringProperty _hintstringProperty;
    
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
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<MeshCollider>();
        _collider.convex = true;
        _initialPos = transform.position;
        _initialRotation = transform.rotation;
    }


    public virtual void Triggered()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
            gameObject.layer = 8;
            _rb.freezeRotation = true;
        }
        else
        {
            gameObject.layer = 6;
            _rb.freezeRotation = false;
        }
        if(transform.position.y < LevelManager.Util.GetOOBLimit())
        {
            transform.position = _initialPos;
            transform.rotation = _initialRotation;
        }    
            
    }
}
