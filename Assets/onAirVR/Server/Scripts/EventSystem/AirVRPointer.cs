/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public abstract class AirVRPointer : MonoBehaviour {
    public static List<AirVRPointer> pointers = new List<AirVRPointer>();

    private Texture2D _cookie;

    [SerializeField] private AirVRCameraRig _cameraRig = null;
    [SerializeField] private string _cookieTextureFilename = null;
    [SerializeField] private float _depthScaleMultiplier = 0.015f;

    protected float depthScaleMultiplier {
        get {
            return _depthScaleMultiplier;
        }
    }

    private void Awake() {
        pointers.Add(this);
    }

    private IEnumerator Start() {
        if (string.IsNullOrEmpty(_cookieTextureFilename) == false) {
            var request = UnityWebRequestTexture.GetTexture("file://" + System.IO.Path.Combine(Application.streamingAssetsPath, _cookieTextureFilename));
            yield return request.SendWebRequest();

            if (!request.isNetworkError && !request.isHttpError) {
                _cookie = (request.downloadHandler as DownloadHandlerTexture).texture;
            }
        }
    }

    protected virtual void Update() {
        if (AirVRInput.IsDeviceAvailable(_cameraRig, device) && AirVRInput.IsDeviceFeedbackEnabled(_cameraRig, device) == false && cookie != null) {
            AirVRInput.EnableTrackedDeviceFeedback(_cameraRig, device, cookie, depthScaleMultiplier);
        }
    }

    private void OnDisable() {
        if (AirVRInput.IsDeviceFeedbackEnabled(_cameraRig, device)) {
            AirVRInput.DisableDeviceFeedback(_cameraRig, device);
        }
    }

    private void OnDestroy() {
        pointers.Remove(this);
    }

    protected Texture2D cookie {
        get {
            return _cookie;
        }
    }

    protected abstract AirVRInput.Device device { get; }

    public AirVRCameraRig cameraRig {
        get {
            return _cameraRig;
        }
    }

    public bool interactable {
        get {
            return AirVRInput.IsDeviceFeedbackEnabled(_cameraRig, device);
        }
    }

    public abstract bool primaryButtonPressed { get; }
    public abstract bool primaryButtonReleased { get; }

    public Ray GetWorldRay() {
        switch (device) {
            case AirVRInput.Device.HeadTracker:
                return new Ray(_cameraRig.headPose.position, _cameraRig.headPose.forward);
            case AirVRInput.Device.RightHandTracker:
                Vector3 position = Vector3.zero;
                Quaternion orientation = Quaternion.identity;
                AirVRInput.GetTrackedDevicePositionAndOrientation(_cameraRig, AirVRInput.Device.RightHandTracker, out position, out orientation);
                return new Ray(position, orientation * Vector3.forward);
        }
        return new Ray();
    }

    public void UpdateRaycastResult(Ray ray, RaycastResult raycastResult) {
        if (raycastResult.isValid) {
            AirVRInput.FeedbackTrackedDevice(_cameraRig, device, ray.origin, raycastResult.worldPosition, raycastResult.worldNormal);
        }
        else {
            AirVRInput.FeedbackTrackedDevice(_cameraRig, device, Vector3.zero, Vector3.zero, Vector3.zero);
        }
    }
}
