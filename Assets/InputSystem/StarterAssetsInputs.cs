using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool rotate;
		public bool RotateUp;
		public bool RotateRight;
		public bool shoot;
		public bool pause;
		public Vector2 mousePosition;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM 
		public virtual void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}

		// New input for shoot 
		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnRotate(InputValue value)
		{
			RotateInput(value.isPressed);
		}

		public void OnRotateRight(InputValue value)
		{
			RotateRightInput(value.isPressed);
		}

		public void OnRotateUp(InputValue value)
		{
			
			RotateUpInput(value.isPressed);
		}

		public void OnMousePosition(InputValue value)
		{

			MouseInput(value.Get<Vector2>());
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void PauseInput(bool PauseInput)
		{
			pause = PauseInput;
		}
		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
			Debug.Log("Shoot state : "+shoot);
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void RotateInput(bool newRotate)
		{
			Debug.Log(newRotate);
			rotate = newRotate;
		}

		public void RotateUpInput(bool newState)
		{
			RotateUp = newState;
		}

		public void RotateRightInput(bool newState)
		{
			RotateRight = newState;
		}


		public void MouseInput(Vector2 newMousePosition)
		{
			//Debug.Log(newRotate);
			//rotate = newRotate;
			mousePosition = newMousePosition;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}