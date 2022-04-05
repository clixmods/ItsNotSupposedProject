using UnityEngine;
using System;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		public PlayerDataObject PlayerSettings;

		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 6.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;
		[Tooltip("Number of Jumps")]
		public float JumpMax = 1;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		[SerializeField] float _cinemachineTargetPitch;

		// player
		protected float _speed;
		private float _rotationVelocity;
		protected float _verticalVelocity;
		private float _terminalVelocity = 53.0f;
		private int jumpCount = 0;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		private PlayerInput _playerInput;
		protected CharacterController _controller;
		[SerializeField] protected StarterAssetsInputs _input;
		private GameObject _mainCamera;

		private const float _threshold = 0.01f;
		
		private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";


		// Pickup property
		[Tooltip("How far the player can take the object")]
		[SerializeField] float maxDistanceToPickupObject = 5000; 
		[Tooltip("Where the object will be move, when the player take it")]
		public Transform pickedObjectStartPos;
		private GameObject heldObj;
		[Tooltip("Movement force applied on the object when the player held it while he move.")]
		public float moveForce = 250;

		GameObject aimed;

		[SerializeField] Animation anim;
		// Player property
		public StarterAssetsInputs Input
		{
			get{ return _input ;}
			set{ _input = value;}
		}


		// Watch if the visor aim a object etc
		void WatchPickup()
		{	
			if(InteractableObject._objectIsGrabbed == null  && aimed != null)
			{
				if(aimed.transform.TryGetComponent<Outline>(out Outline outlin))
				{
					outlin.OutlineColor = Color.white;
				}
				aimed = null;
			}	
			

			Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * maxDistanceToPickupObject, Color.yellow);
			RaycastHit Aim;
			if(InteractableObject._objectIsGrabbed == null && Physics.Raycast(Camera.main.transform.position , Camera.main.transform.TransformDirection(Vector3.forward)*maxDistanceToPickupObject , out Aim , maxDistanceToPickupObject  ))
			{
				if(Aim.transform.TryGetComponent<Outline>(out Outline obj))
				{
					if(obj.OutlineColor != Color.green)
						obj.OutlineColor = new Color(1, 0.6f,0,1);
					aimed = obj.gameObject;
				}
			}

			if(_input.interact)
			{

				anim.Play("plr_Interact");
				if(heldObj == null)
				{
					RaycastHit hit;
					if(Physics.Raycast(Camera.main.transform.position , Camera.main.transform.TransformDirection(Vector3.forward)*maxDistanceToPickupObject , out hit , maxDistanceToPickupObject  ))
					{
						Debug.Log("Objectt Attempt");
						if(hit.transform.TryGetComponent<InteractableObject>(out InteractableObject obj))
						{

							
								PickupObject(hit.transform.gameObject);
									if(obj.Grabable)
										heldObj = hit.transform.gameObject;	

						}
					}
				}
				else
				{
					Debug.Log("Objectt dropped");
					DropObject();
				}
			}
			if(heldObj != null)
			{
				MoveObject();
			}
			_input.interact = false;
		}


		void MoveObject()
		{
			// On check si l'objet est trop loin lorsque le joueur le tien, si c'est trop loin on le drop
			float maxDistance = maxDistanceToPickupObject + 5f;
			if( Vector3.Distance(transform.position, heldObj.transform.position) > maxDistance)
			{
				DropObject();
				return;
			}	

			if(Vector3.Distance(heldObj.transform.position, pickedObjectStartPos.transform.position) > 0.1f)
			{
				Vector3 moveDirection = (pickedObjectStartPos.position - heldObj.transform.position);
				heldObj.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
				
			}
		}
		void PickupObject(GameObject pickObj)
		{
			pickObj.GetComponent<InteractableObject>().PickupBehavior();
		}
		void DropObject()
		{
			heldObj.GetComponent<InteractableObject>().DropBehavior();
			heldObj = null;
		}

		// When the Gameobject is waked
		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			if(_input == null) // si jamais on l'a setup avant
				_input = GetComponent<StarterAssetsInputs>();
			_playerInput = GetComponent<PlayerInput>();

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;

			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			if(PlayerSettings != null)
			{
				GroundLayers = PlayerSettings.GroundLayers;
				maxDistanceToPickupObject = PlayerSettings.maxDistanceToGrab;
			}

		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
			if(_input.pause)
			{
				LevelManager.Util.IsPaused = !LevelManager.Util.IsPaused;
				_input.pause = false;
			}
		}
		private void FixedUpdate()
		{
			WatchPickup();
			if(heldObj != null)
			{
				if (_input.rotate ) //we only want to begin this process on the initial click
				{
					heldObj.GetComponent<InteractableObject>().RotateBehavior(transform, _input);
				}
				else
					heldObj.GetComponent<Rigidbody>().freezeRotation = true;
			}
			
		}

		private void LateUpdate()
		{
			if (!_input.rotate && !LevelManager.Util.IsPaused)
				CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		public virtual void Move()
		{
			
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				anim.Play("plr_walking");
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}
			else if(!anim.IsPlaying("plr_Interact"))
				anim.Play("plr_Idle");

			// move the player
				_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
			if(_verticalVelocity > 0 )
				anim.Play("plr_JumpLoop");

		}

		private void JumpAndGravity()
		{
			
			// if(Grounded && jumpCount >= JumpMax)
			// {
			// 	jumpCount = 0;
			// }

			if (Grounded  )
			{
				
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					jumpCount++;
					// if( ( !Grounded && jumpCount < JumpMax))
					// 	_input.jump = false;

					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				
				// if we are not grounded, do not jump
				_input.jump = false;
				
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}
