using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StarterAssets;


public class InteractablePendule : InteractableObject 
{
    enum TypePendule
    {
        Hour,
        Minute,
        Second,
    }
    public bool _oof = false;
    float _timer = 1;
    [SerializeField] TypePendule _typePendule;
    [SerializeField] float _speedRotation;
    [SerializeField] float rotationAccumulate;
    [SerializeField] float rotationToAccumulate = 360;
    static bool triggered;
    
    static bool secDone;
    static bool minDone;
    static bool HourDone;

    public override void StartSpecific()
    {
        triggered = false;
        HourDone = false;
        minDone = false;
        secDone = false;
        _rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        gameObject.layer = 8;

        _outline.OutlineWidth = 50;
    }
    public override void LateUpdateSpecific()
    {
       
        
    }
    public override void UpdateSpecific()
    {
        if(!triggered)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x ,  transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+Time.deltaTime*_speedRotation);
            rotationAccumulate += Time.deltaTime;
        }
        if(!LevelManager.Util.CanEndgame && HourDone && minDone && secDone)
            LevelManager.Util.CanEndgame = true;
    }

    public override void RotateBehavior(Transform Player, StarterAssetsInputs _input)
    {
        if(rotationAccumulate <= rotationToAccumulate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x ,  transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z+_input.look.x);
            rotationAccumulate += _input.look.x;
            triggered = true;
        }
        else
        {
            _outline.OutlineMode = Outline.Mode.OutlineHidden;
            switch(_typePendule) 
            {
                case TypePendule.Hour :
                    HourDone = true;
                break;
                case TypePendule.Minute :
                    minDone = true;
                break;
                case TypePendule.Second :
                    secDone = true;
                break;
            }
        }
        
    }

    public override void PickupBehavior()
    {
        isGrabbed = true;
    }
    public override void DropBehavior()
    {
		isGrabbed = false;	
    }

    public override void Triggered()
    {
        
        base.Triggered();
    }
}
