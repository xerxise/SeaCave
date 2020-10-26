/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;

public class AirVRSamplePlayer : MonoBehaviour {
    private const float ThrowSpeed = 12.0f;
    private const float ThrowTorqueSpeed = 20.0f;

    private Transform _thisTransform;
    private CharacterController _thisCharacterController;
    private AirVRStereoCameraRig _cameraRig;
    private float _fallingSpeed;
    private GameObject _leftHand;
    private GameObject _rightHand;
    private AudioSource _soundShot;

    public AirVRSampleCan canPrefab;
    public float gravity;
    public float speed;

    private void resetFalling() {
        _fallingSpeed = 0.0f;
    }

    private void updateFalling(float deltaTime) {
		if (_thisCharacterController.isGrounded) {
			_fallingSpeed = 0.0f;
		}
		else {
			_fallingSpeed += gravity * deltaTime;
		}
	}

    private Vector3 inputDirection {
        get {
            Vector2 result = Vector2.zero;
            if (AirVRInput.Get(_cameraRig, AirVRInput.Touch.Touchpad)) {
                result += translateTouchPositionToMoveDirection(AirVRInput.Get(_cameraRig, AirVRInput.Axis2D.Touchpad) * 5.0f);
            }
            result += AirVRInput.Get(_cameraRig, AirVRInput.Axis2D.LThumbstick);
            return new Vector3(Mathf.Clamp(result.x, -1.0f, 1.0f), 0.0f, Mathf.Clamp(result.y, -1.0f, 1.0f));
        }
    }

    private Vector2 translateTouchPositionToMoveDirection(Vector2 touchPos) {
        if (touchPos.magnitude > 0.3f) {
            if (Mathf.Abs(touchPos.x) > Mathf.Abs(touchPos.y)) {
                return Mathf.Sign(touchPos.x) * Vector2.right;
            }
            else {
                return Mathf.Sign(touchPos.y) * Vector2.up;
            }
        }
        return Vector2.zero;
    }

    private void processMovement() {
        if (_thisCharacterController != null && _thisCharacterController.enabled) {
            Vector3 moveDirection = inputDirection;
            if (moveDirection.sqrMagnitude > 1.0f) {
                moveDirection = moveDirection.normalized;
            }

            Vector3 velocity = speed * (_cameraRig as AirVRStereoCameraRig).centerEyeAnchor.TransformDirection(moveDirection);
            Vector3 horizontalDir = new Vector3(velocity.x, 0.0f, velocity.z).normalized;

            Vector3 movingDir = velocity.magnitude * horizontalDir * Time.deltaTime;
            if (_fallingSpeed > 0.0f) {
                _thisCharacterController.Move(movingDir + Mathf.Max(_fallingSpeed * Time.deltaTime, movingDir.magnitude / Mathf.Tan(_thisCharacterController.slopeLimit)) * Vector3.down);
            }
            else {
                _thisCharacterController.Move(movingDir);
            }
            updateFalling(Time.deltaTime);
        }
    }

    private void processInput() {
        if (AirVRInput.GetDown(_cameraRig, AirVRInput.Button.A) || 
            AirVRInput.GetDown(_cameraRig, AirVRInput.Button.LIndexTrigger) || 
            AirVRInput.GetDown(_cameraRig, AirVRInput.Button.RIndexTrigger)) {
            throwCan();
        }
    }

    public void throwCan() {
        Vector3 forward = _cameraRig.centerEyeAnchor.forward;

        AirVRSampleCan can = Instantiate(canPrefab, transform.position, _cameraRig.centerEyeAnchor.rotation) as AirVRSampleCan;
        can.Throw(forward * ThrowSpeed, Vector3.right * ThrowTorqueSpeed);

        _soundShot.Play();
    }

    void Awake() {
        _thisTransform = transform;
        _thisCharacterController = GetComponent<CharacterController>();
        _cameraRig = GetComponentInChildren<AirVRStereoCameraRig>();
        _soundShot = transform.Find("SoundShot").GetComponent<AudioSource>();
    }

    void Update() {
        processMovement();
        processInput();
    }

    public void SetPosition(Transform pos) {
        _thisTransform.position = pos.position;
        _thisTransform.rotation = pos.rotation;
    }

    public void EnableInteraction(bool enable) {
        if (_thisCharacterController == null) {
            return;
        }

        _thisCharacterController.enabled = enable;

        if (enable == false) {
            resetFalling();
        }
    }
}
