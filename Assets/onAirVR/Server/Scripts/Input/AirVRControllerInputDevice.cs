using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVRControllerInputDevice : AirVRInputDevice {
    private const float AxisAsButtonThreshold = 0.5f;   // refer OVRInput.cs in Oculus Utilities

    // implements AirVRInputDevice
    protected override string deviceName {
        get {
            return AirVRInputDeviceName.Controller;
        }
    }

    protected override void MakeControlList() {
        AddControlTouch((byte)AirVRControllerKey.Touchpad);

        AddControlButton((byte)AirVRControllerKey.ButtonTouchpad);
        AddControlButton((byte)AirVRControllerKey.ButtonUp);
        AddControlButton((byte)AirVRControllerKey.ButtonDown);
        AddControlButton((byte)AirVRControllerKey.ButtonLeft);
        AddControlButton((byte)AirVRControllerKey.ButtonRight);

        AddControlAxis2D((byte)AirVRControllerKey.Axis2DLThumbstick);
        AddControlAxis2D((byte)AirVRControllerKey.Axis2DRThumbstick);
        AddControlAxis((byte)AirVRControllerKey.AxisLIndexTrigger);
        AddControlAxis((byte)AirVRControllerKey.AxisRIndexTrigger);
        AddControlAxis((byte)AirVRControllerKey.AxisLHandTrigger);
        AddControlAxis((byte)AirVRControllerKey.AxisRHandTrigger);
        AddControlButton((byte)AirVRControllerKey.ButtonA);
        AddControlButton((byte)AirVRControllerKey.ButtonB);
        AddControlButton((byte)AirVRControllerKey.ButtonX);
        AddControlButton((byte)AirVRControllerKey.ButtonY);
        AddControlButton((byte)AirVRControllerKey.ButtonStart);
        AddControlButton((byte)AirVRControllerKey.ButtonBack);
        AddControlButton((byte)AirVRControllerKey.ButtonLThumbstick);
        AddControlButton((byte)AirVRControllerKey.ButtonRThumbstick);
        AddControlButton((byte)AirVRControllerKey.ButtonLShoulder);
        AddControlButton((byte)AirVRControllerKey.ButtonRShoulder);

        AddExtControlAxis2D((byte)AirVRControllerKey.ExtAxis2DTouchpad);
        AddExtControlButton((byte)AirVRControllerKey.ExtTouchTouchpad);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLIndexTrigger);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRIndexTrigger);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLHandTrigger);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRHandTrigger);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickUp);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickDown);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickLeft);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickRight);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickUp);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickDown);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickLeft);
        AddExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickRight);
    }

    protected override void UpdateExtendedControls() {
        Vector2 axis2d = Vector2.zero;
        bool down = false;

        if (GetTouch((byte)AirVRControllerKey.Touchpad, ref axis2d, ref down)) {
            SetExtControlAxis2D((byte)AirVRControllerKey.ExtAxis2DTouchpad, axis2d);
            SetExtControlButton((byte)AirVRControllerKey.ExtTouchTouchpad, down ? 1.0f : 0.0f);
        }
        else {
            SetExtControlAxis2D((byte)AirVRControllerKey.ExtAxis2DTouchpad, Vector2.zero);
            SetExtControlButton((byte)AirVRControllerKey.ExtTouchTouchpad, 0.0f);
        }

        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLIndexTrigger, GetAxis((byte)AirVRControllerKey.AxisLIndexTrigger) >= AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRIndexTrigger, GetAxis((byte)AirVRControllerKey.AxisRIndexTrigger) >= AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLHandTrigger, GetAxis((byte)AirVRControllerKey.AxisLHandTrigger) >= AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRHandTrigger, GetAxis((byte)AirVRControllerKey.AxisRHandTrigger) >= AxisAsButtonThreshold ? 1.0f : 0.0f);

        axis2d = GetAxis2D((byte)AirVRControllerKey.Axis2DLThumbstick);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickUp, axis2d.y >= AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickDown, axis2d.y <= -AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickLeft, axis2d.x <= -AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonLThumbstickRight, axis2d.x >= AxisAsButtonThreshold ? 1.0f : 0.0f);

        axis2d = GetAxis2D((byte)AirVRControllerKey.Axis2DRThumbstick);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickUp, axis2d.y >= AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickDown, axis2d.y <= -AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickLeft, axis2d.x <= -AxisAsButtonThreshold ? 1.0f : 0.0f);
        SetExtControlButton((byte)AirVRControllerKey.ExtButtonRThumbstickRight, axis2d.x >= AxisAsButtonThreshold ? 1.0f : 0.0f);
    }
}
