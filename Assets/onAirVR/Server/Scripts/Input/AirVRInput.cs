/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;
using UnityEngine.Assertions;

public static class AirVRInput {
    public enum Device {
        HeadTracker,
        LeftHandTracker,
        RightHandTracker,
        Controller
    }

    public enum Axis2D {
        Touchpad,
        LThumbstick,
        RThumbstick
    }

    public enum Axis {
        LIndexTrigger,
        RIndexTrigger,
        LHandTrigger,
        RHandTrigger
    }

    public enum Touch {
        Touchpad
    }

    public enum Button {
        Touchpad,
        Up,
        Down,
        Left,
        Right,
        A,
        B,
        X,
        Y,
        Start,
        Back,
        LThumbstick,
        RThumbstick,
        LShoulder,
        RShoulder,
        LIndexTrigger,
        RIndexTrigger,
        LHandTrigger,
        RHandTrigger,
        LThumbstickUp,
        LThumbstickDown,
        LThumbstickLeft,
        LThumbstickRight,
        RThumbstickUp,
        RThumbstickDown,
        RThumbstickLeft,
        RThumbstickRight
    }

    private static byte parseControlID(Axis2D axis) {
        switch (axis) {
            case Axis2D.Touchpad:
                return (byte)AirVRControllerKey.ExtAxis2DTouchpad;
            case Axis2D.LThumbstick:
                return (byte)AirVRControllerKey.Axis2DLThumbstick;
            case Axis2D.RThumbstick:
                return (byte)AirVRControllerKey.Axis2DRThumbstick;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static byte parseControlID(Axis axis) {
        switch (axis) {
            case Axis.LIndexTrigger:
                return (byte)AirVRControllerKey.AxisLIndexTrigger;
            case Axis.RIndexTrigger:
                return (byte)AirVRControllerKey.AxisRIndexTrigger;
            case Axis.LHandTrigger:
                return (byte)AirVRControllerKey.AxisLHandTrigger;
            case Axis.RHandTrigger:
                return (byte)AirVRControllerKey.AxisRHandTrigger;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static byte parseControlID(Touch touch) {
        switch (touch) {
            case Touch.Touchpad:
                return (byte)AirVRControllerKey.ExtTouchTouchpad;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static byte parseControlID(Button button) {
        switch (button) {
            case Button.Touchpad:
                return (byte)AirVRControllerKey.ButtonTouchpad;
            case Button.Up:
                return (byte)AirVRControllerKey.ButtonUp;
            case Button.Down:
                return (byte)AirVRControllerKey.ButtonDown;
            case Button.Left:
                return (byte)AirVRControllerKey.ButtonLeft;
            case Button.Right:
                return (byte)AirVRControllerKey.ButtonRight;
            case Button.A:
                return (byte)AirVRControllerKey.ButtonA;
            case Button.B:
                return (byte)AirVRControllerKey.ButtonB;
            case Button.X:
                return (byte)AirVRControllerKey.ButtonX;
            case Button.Y:
                return (byte)AirVRControllerKey.ButtonY;
            case Button.Start:
                return (byte)AirVRControllerKey.ButtonStart;
            case Button.Back:
                return (byte)AirVRControllerKey.ButtonBack;
            case Button.LThumbstick:
                return (byte)AirVRControllerKey.ButtonLThumbstick;
            case Button.RThumbstick:
                return (byte)AirVRControllerKey.ButtonRThumbstick;
            case Button.LShoulder:
                return (byte)AirVRControllerKey.ButtonLShoulder;
            case Button.RShoulder:
                return (byte)AirVRControllerKey.ButtonRShoulder;
            case Button.LIndexTrigger:
                return (byte)AirVRControllerKey.ExtButtonLIndexTrigger;
            case Button.RIndexTrigger:
                return (byte)AirVRControllerKey.ExtButtonRIndexTrigger;
            case Button.LHandTrigger:
                return (byte)AirVRControllerKey.ExtButtonLHandTrigger;
            case Button.RHandTrigger:
                return (byte)AirVRControllerKey.ExtButtonRHandTrigger;
            case Button.LThumbstickUp:
                return (byte)AirVRControllerKey.ExtButtonLThumbstickUp;
            case Button.LThumbstickDown:
                return (byte)AirVRControllerKey.ExtButtonLThumbstickDown;
            case Button.LThumbstickLeft:
                return (byte)AirVRControllerKey.ExtButtonLThumbstickLeft;
            case Button.LThumbstickRight:
                return (byte)AirVRControllerKey.ExtButtonLThumbstickRight;
            case Button.RThumbstickUp:
                return (byte)AirVRControllerKey.ExtButtonRThumbstickUp;
            case Button.RThumbstickDown:
                return (byte)AirVRControllerKey.ExtButtonRThumbstickDown;
            case Button.RThumbstickLeft:
                return (byte)AirVRControllerKey.ExtButtonRThumbstickLeft;
            case Button.RThumbstickRight:
                return (byte)AirVRControllerKey.ExtButtonRThumbstickRight;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static string deviceName(Device device) {
        switch (device) {
            case Device.HeadTracker:
                return AirVRInputDeviceName.HeadTracker;
            case Device.LeftHandTracker:
                return AirVRInputDeviceName.LeftHandTracker;
            case Device.RightHandTracker:
                return AirVRInputDeviceName.RightHandTracker;
            case Device.Controller:
                return AirVRInputDeviceName.Controller;
        }
        Assert.IsTrue(false);
        return "";
    }

    private static bool isTrackedDevice(Device device) {
        return device == Device.HeadTracker || device == Device.LeftHandTracker || device == Device.RightHandTracker;
    }

    private static byte transformControlID(Device device) {
        switch (device) {
            case Device.HeadTracker:
                return (byte)AirVRHeadTrackerKey.Transform;
            case Device.LeftHandTracker:
                return (byte)AirVRLeftHandTrackerKey.Transform;
            case Device.RightHandTracker:
                return (byte)AirVRRightHandTrackerKey.Transform;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static byte feedbackRaycastHitResultControlID(Device device) {
        switch (device) {
            case Device.HeadTracker:
                return (byte)AirVRHeadTrackerKey.RaycastHitResult;
            case Device.LeftHandTracker:
                return (byte)AirVRLeftHandTrackerKey.RaycastHitResult;
            case Device.RightHandTracker:
                return (byte)AirVRRightHandTrackerKey.RaycastHitResult;
        }
        Assert.IsTrue(false);
        return 0;
    }

    private static void GetTransform(AirVRCameraRig cameraRig, string deviceName, byte controlID, ref Vector3 worldPosition, ref Quaternion worldOrientation) {
        Vector3 position = Vector3.zero;
        Quaternion orientation = Quaternion.identity;

        cameraRig.inputStream.GetTransform(deviceName, controlID, ref position, ref orientation);

        worldPosition = cameraRig.clientSpaceToWorldMatrix.MultiplyPoint(position);
        worldOrientation = Quaternion.LookRotation(cameraRig.clientSpaceToWorldMatrix.GetColumn(2), cameraRig.clientSpaceToWorldMatrix.GetColumn(1)) * orientation;
    }

    private static Vector2 GetAxis2D(AirVRCameraRig cameraRig, string deviceName, byte controlID) {
        return cameraRig.inputStream.GetAxis2D(deviceName, controlID);
    }

    private static float GetAxis(AirVRCameraRig cameraRig, string deviceName, byte controlID) {
        return cameraRig.inputStream.GetAxis(deviceName, controlID);
    }

    private static bool GetButton(AirVRCameraRig cameraRig, string deviceName, byte controlID) {
        return cameraRig.inputStream.GetButton(deviceName, controlID);
    }

    private static bool GetButtonDown(AirVRCameraRig cameraRig, string deviceName, byte controlID) {
        return cameraRig.inputStream.GetButtonDown(deviceName, controlID);
    }

    private static bool GetButtonUp(AirVRCameraRig cameraRig, string deviceName, byte controlID) {
        return cameraRig.inputStream.GetButtonUp(deviceName, controlID);
    }

    public static bool IsDeviceAvailable(AirVRCameraRig cameraRig, Device device) {
        return cameraRig.inputStream.CheckIfInputDeviceAvailable(deviceName(device));
    }

    public static Vector2 Get(AirVRCameraRig cameraRig, Axis2D axis) {
        return GetAxis2D(cameraRig, AirVRInputDeviceName.Controller, parseControlID(axis));
    }

    public static float Get(AirVRCameraRig cameraRig, Axis axis) {
        return GetAxis(cameraRig, AirVRInputDeviceName.Controller, parseControlID(axis));
    }

    public static bool Get(AirVRCameraRig cameraRig, Touch touch) {
        return GetButton(cameraRig, AirVRInputDeviceName.Controller, parseControlID(touch));
    }

    public static bool Get(AirVRCameraRig cameraRig, Button button) {
        return GetButton(cameraRig, AirVRInputDeviceName.Controller, parseControlID(button));
    }

    public static bool GetDown(AirVRCameraRig cameraRig, Touch touch) {
        return GetButtonDown(cameraRig, AirVRInputDeviceName.Controller, parseControlID(touch));
    }

    public static bool GetDown(AirVRCameraRig cameraRig, Button button) {
        return GetButtonDown(cameraRig, AirVRInputDeviceName.Controller, parseControlID(button));
    }

    public static bool GetUp(AirVRCameraRig cameraRig, Touch touch) {
        return GetButtonUp(cameraRig, AirVRInputDeviceName.Controller, parseControlID(touch));
    }

    public static bool GetUp(AirVRCameraRig cameraRig, Button button) {
        return GetButtonUp(cameraRig, AirVRInputDeviceName.Controller, parseControlID(button));
    }
    
    public static bool IsDeviceFeedbackEnabled(AirVRCameraRig cameraRig, Device device) {
        return cameraRig.inputStream.IsDeviceFeedbackEnabled(deviceName(device));
    }

    public static void EnableTrackedDeviceFeedback(AirVRCameraRig cameraRig, Device device, Texture2D cookieTexture, float depthScaleMultiplier) {
        if (isTrackedDevice(device) == false) {
            return;
        }
        
        cameraRig.inputStream.EnableTrackedDeviceFeedback(deviceName(device), cookieTexture, depthScaleMultiplier);
    }

    public static void DisableDeviceFeedback(AirVRCameraRig cameraRig, Device device) {
        cameraRig.inputStream.DisableDeviceFeedback(deviceName(device));
    }

    public static void GetTrackedDevicePositionAndOrientation(AirVRCameraRig cameraRig, Device device, out Vector3 worldPosition, out Quaternion worldOrientation) {
        if (isTrackedDevice(device) == false) {
            worldPosition = cameraRig.headPose.position;
            worldOrientation = cameraRig.headPose.rotation; 
            return;
        }

        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        GetTransform(cameraRig, deviceName(device), transformControlID(device), ref pos, ref rot);

        worldPosition = pos;
        worldOrientation = rot;
    }

    public static void FeedbackTrackedDevice(AirVRCameraRig cameraRig, Device device, Vector3 worldRayOrigin, Vector3 worldHitPosition, Vector3 worldHitNormal) {
        if (isTrackedDevice(device) == false) {
            return;
        }

        cameraRig.inputStream.FeedbackTrackedDevice(deviceName(device), feedbackRaycastHitResultControlID(device),
                                                    cameraRig.clientSpaceToWorldMatrix.inverse.MultiplyPoint(worldRayOrigin),
                                                    cameraRig.clientSpaceToWorldMatrix.inverse.MultiplyPoint(worldHitPosition),
                                                    cameraRig.clientSpaceToWorldMatrix.inverse.MultiplyVector(worldHitNormal));
    }
}
