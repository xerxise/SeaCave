/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;
using UnityEngine.Assertions;

public sealed class AirVRStereoCameraRig : AirVRCameraRig, IAirVRTrackingModelContext {
    private readonly string TrackingSpaceName = "TrackingSpace";
    private readonly string LeftEyeAnchorName = "LeftEyeAnchor";
    private readonly string RightEyeAnchorName = "RightEyeAnchor";
    private readonly string CenterEyeAnchorName = "CenterEyeAnchor";
    private readonly string LeftHandAnchorName = "LeftHandAnchor";
    private readonly string RightHandAnchorName = "RightHandAnchor";
    private readonly int CameraLeftIndex = 0;
    private readonly int CameraRightIndex = 1;

    public enum TrackingModel {
        Head,
        InterpupillaryDistanceOnly,
        ExternalTracker,
        NoPositionTracking
    }

    private Matrix4x4 _worldToHMDSpaceMatrix;

    private Camera[] _cameras;

    private AirVRTrackingModel _trackingModelObject;

    internal Transform trackingSpace { get; private set; }

    public Camera leftEyeCamera {
        get {
            return _cameras[CameraLeftIndex];
        }
    }

    public Camera rightEyeCamera {
        get {
            return _cameras[CameraRightIndex];
        }
    }

    public Transform leftEyeAnchor { get; private set; }
    public Transform centerEyeAnchor { get; private set; }
    public Transform rightEyeAnchor { get; private set; }
    public Transform leftHandAnchor { get; private set; }
    public Transform rightHandAnchor { get; private set; }

    public TrackingModel trackingModel;
    public Transform externalTrackingOrigin;
    public Transform externalTracker;

    private TrackingModel trackingModelOf(AirVRTrackingModel trackingModelObject) {
        return trackingModelObject.GetType() == typeof(AirVRHeadTrackingModel)             ? TrackingModel.Head :
               trackingModelObject.GetType() == typeof(AirVRIPDOnlyTrackingModel)          ? TrackingModel.InterpupillaryDistanceOnly :
               trackingModelObject.GetType() == typeof(AirVRExternalTrackerTrackingModel)  ? TrackingModel.ExternalTracker :
               trackingModelObject.GetType() == typeof(AirVRNoPotisionTrackingModel)       ? TrackingModel.NoPositionTracking : TrackingModel.Head;
    }

    private AirVRTrackingModel createTrackingModelObject(TrackingModel model) {
        return model == TrackingModel.InterpupillaryDistanceOnly ?  new AirVRIPDOnlyTrackingModel(this, leftEyeAnchor, centerEyeAnchor, rightEyeAnchor) :
               model == TrackingModel.ExternalTracker            ?  new AirVRExternalTrackerTrackingModel(this, leftEyeAnchor, centerEyeAnchor, rightEyeAnchor, externalTrackingOrigin, externalTracker) :
               model == TrackingModel.NoPositionTracking         ?  new AirVRNoPotisionTrackingModel(this, leftEyeAnchor, centerEyeAnchor, rightEyeAnchor) :
                                                                    new AirVRHeadTrackingModel(this, leftEyeAnchor, centerEyeAnchor, rightEyeAnchor) as AirVRTrackingModel;
    }

    private void updateTrackingModel() {
        if (_trackingModelObject == null || trackingModelOf(_trackingModelObject) != trackingModel) {
            _trackingModelObject = createTrackingModelObject(trackingModel);
        }
        if (trackingModelOf(_trackingModelObject) == TrackingModel.ExternalTracker) {
            AirVRExternalTrackerTrackingModel model = _trackingModelObject as AirVRExternalTrackerTrackingModel;
            model.trackingOrigin = externalTrackingOrigin;
            model.tracker = externalTracker;
        }
    }

    // implements AirVRCameraRig
    private bool ensureCameraObjectIntegrity(Transform xform) {
        if (xform.gameObject.GetComponent<Camera>() == null) {
            xform.gameObject.AddComponent<Camera>();
            return false;
        }
        return true;
    }

    protected override void ensureGameObjectIntegrity() {
        if (trackingSpace == null) {
            trackingSpace = getOrCreateGameObject(TrackingSpaceName, transform);
        }
        if (leftEyeAnchor == null) {
            leftEyeAnchor = getOrCreateGameObject(LeftEyeAnchorName, trackingSpace);
        }
        if (centerEyeAnchor == null) {
            centerEyeAnchor = getOrCreateGameObject(CenterEyeAnchorName, trackingSpace);
        }
        if (rightEyeAnchor == null) {
            rightEyeAnchor = getOrCreateGameObject(RightEyeAnchorName, trackingSpace);
        }
        if (leftHandAnchor == null) {
            leftHandAnchor = getOrCreateGameObject(LeftHandAnchorName, trackingSpace);
        }
        if (rightHandAnchor == null) {
            rightHandAnchor = getOrCreateGameObject(RightHandAnchorName, trackingSpace);
        }

        bool updateCamera = false;
        if (_cameras == null) {
            _cameras = new Camera[2];
            updateCamera = true;
        }

        if (ensureCameraObjectIntegrity(leftEyeAnchor) == false || updateCamera) {
            _cameras[CameraLeftIndex] = leftEyeAnchor.GetComponent<Camera>();
        }
        if (ensureCameraObjectIntegrity(rightEyeAnchor) == false || updateCamera) {
            _cameras[CameraRightIndex] = rightEyeAnchor.GetComponent<Camera>();
        }
    }

    protected override void init() {
        if (_trackingModelObject == null) {
            _trackingModelObject = createTrackingModelObject(trackingModel);
        }
    }

    protected override void setupCamerasOnBound(AirVRClientConfig config) {
        leftEyeCamera.projectionMatrix = config.GetLeftEyeCameraProjection(leftEyeCamera.nearClipPlane, leftEyeCamera.farClipPlane);
        rightEyeCamera.projectionMatrix = config.GetRightEyeCameraProjection(rightEyeCamera.nearClipPlane, rightEyeCamera.farClipPlane);
    }

    protected override void onStartRender() {
        updateTrackingModel();
        _trackingModelObject.StartTracking();
    }

    protected override void onStopRender() {
        updateTrackingModel();
        _trackingModelObject.StopTracking();
    }

    protected override void updateCameraTransforms(AirVRClientConfig config, Vector3 centerEyePosition, Quaternion centerEyeOrientation) {
        updateTrackingModel();
        _trackingModelObject.UpdateEyePose(config, centerEyePosition, centerEyeOrientation);
    }

    protected override void updateControllerTransforms(AirVRClientConfig config) {
        Vector3 position = Vector3.zero;
        Quaternion orientation = Quaternion.identity;

        if (inputStream.GetTransform(AirVRInputDeviceName.LeftHandTracker, (byte)AirVRLeftHandTrackerKey.Transform, ref position, ref orientation)) {
            leftHandAnchor.localPosition = position;
            leftHandAnchor.localRotation = orientation;
        }
        if (inputStream.GetTransform(AirVRInputDeviceName.RightHandTracker, (byte)AirVRRightHandTrackerKey.Transform, ref position, ref orientation)) {
            rightHandAnchor.localPosition = position;
            rightHandAnchor.localRotation = orientation;
        }
    }

    internal override Matrix4x4 clientSpaceToWorldMatrix {
        get {
            Assert.IsNotNull(_trackingModelObject);
            return _trackingModelObject.HMDSpaceToWorldMatrix;
        }
    }

    internal override Transform headPose {
        get {
            return centerEyeAnchor;
        }
    }

    internal override Camera[] cameras {
        get {
            return _cameras;
        }
    }

    // implements IAirVRTrackingModelContext
    void IAirVRTrackingModelContext.RecenterCameraRigPose() {
        RecenterPose();
    }
}
