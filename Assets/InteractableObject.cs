using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

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
    [SerializeField]  bool _isGrabbed;


    protected Rigidbody _rb;
    MeshCollider _collider;
    protected Vector3 _initialPos;
    protected Quaternion _initialRotation;
    protected Outline _outline;

    public static GameObject _objectIsGrabbed;

    protected HintstringProperty _hintstringProperty;

   [SerializeField] protected string _hintText = "Press [E] to grab";
    
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
        _outline = GetComponent<Outline>();
        _collider.convex = true;
        _initialPos = transform.position;
        _initialRotation = transform.rotation;
        _outline.OutlineWidth = 10;

        StartSpecific();


    }
    
    public virtual void StartSpecific()
    {
         // Crée une boite de dialogue sur le monde 3D
        _hintstringProperty = UIManager.CreateHintString(gameObject, _hintText , 5 );
    }

    public virtual void Triggered()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(_objectIsGrabbed != null && _objectIsGrabbed != gameObject)
        {
            _outline.OutlineWidth = 0;
        }
        else
            _outline.OutlineWidth = 10;

        UpdateSpecific();
            
    }
    void LateUpdate()
    {
         LateUpdateSpecific();
            
    }
    public virtual void UpdateSpecific()
    {
        if(transform.position.y < LevelManager.Util.GetOOBLimit())
        {
            transform.position = _initialPos;
            transform.rotation = _initialRotation;
        }   
    }

    public virtual void LateUpdateSpecific()
    {

    }
    public virtual void RotateBehavior(Transform Player, StarterAssetsInputs _input)
    {
            _rb.WakeUp();
        	_rb.freezeRotation = false;
			Vector3 rot = transform.position - Player.transform.position;
            Vector3 oof = Player.transform.rotation.eulerAngles;
            Debug.Log(_input.look);
            _rb.angularVelocity = new Vector3((_input.look.y *  rot.y * 10 ), (_input.look.x* rot.x * 10), (_input.look.x/2 + _input.look.y/2)* rot.z );
			//_rb.AddTorque(   new Vector3((_input.look.y *  rot.y * 10 ), (_input.look.x* rot.x * 10), 0), ForceMode.VelocityChange);

              //transform.Rotate(Vector3.down, _input.look.x*10, Space.World);
	            //transform.Rotate(Vector3.right, _input.look.y*10, Space.World);
				//Vector3 oof = objRig.rotation.eulerAngles + new Vector3( (_input.look.x * 10 ), (_input.look.y * 10), 0)  ;
				//objRig.MoveRotation( Quaternion.LookRotation(oof) ) ;
				// select the axis by which you want to rotate the GameObject
				//objRig.transform.RotateAround (Vector3.down, _input.look.x);
				//objRig.transform.RotateAround (Vector3.right, _input.look.y);



				/*
					float rotationSpeed = 0.2f;
 
            void OnMouseDrag()
            {
                float XaxisRotation = Input.GetAxis("Mouse X")*rotationSpeed;
                float YaxisRotation = Input.GetAxis("Mouse Y")*rotationSpeed;
                // select the axis by which you want to rotate the GameObject
                transform.RotateAround (Vector3.down, XaxisRotation);
                transform.RotateAround (Vector3.right, YaxisRotation);
            }
                        */
    }

    public virtual void PickupBehavior()
    {
        _objectIsGrabbed = gameObject;
        _outline.OutlineColor = Color.green;
        _rb.Sleep();
        isGrabbed = true;
		_rb.useGravity = false;
		_rb.drag = 10;
        gameObject.layer = 8;

        _rb.freezeRotation = true;
        _hintstringProperty.enable = false;
    }
    public virtual void DropBehavior()
    {
        _objectIsGrabbed = null;

		isGrabbed = false;
		_rb.useGravity = true;
		_rb.drag = 1;		
        gameObject.layer = 6;
        _rb.freezeRotation = false;
        _hintstringProperty.enable = true;
    }
    public virtual void MoveBehavior()
    {
        
    }

}
