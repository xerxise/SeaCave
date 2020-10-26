/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

public class AirVRAutoSelectPointer : AirVRPointer {
    private AirVRInput.Device _currentDevice = AirVRInput.Device.HeadTracker;

    protected override void Update() {
        AirVRInput.Device dev = AirVRInput.IsDeviceAvailable(cameraRig, AirVRInput.Device.RightHandTracker) ? AirVRInput.Device.RightHandTracker : AirVRInput.Device.HeadTracker;
        if (dev != _currentDevice) {
            if (AirVRInput.IsDeviceFeedbackEnabled(cameraRig, _currentDevice)) {
                AirVRInput.DisableDeviceFeedback(cameraRig, _currentDevice);
            }
            _currentDevice = dev;
        }

        base.Update();
    }

    // implements AirVRAutoSelectPointer
    protected override AirVRInput.Device device {
        get {
            return _currentDevice;
        }
    }

    public override bool primaryButtonPressed {
        get {
            switch (device) {
                case AirVRInput.Device.HeadTracker:
                    return AirVRInput.GetDown(cameraRig, AirVRInput.Button.A) || AirVRInput.GetDown(cameraRig, AirVRInput.Touch.Touchpad);
                case AirVRInput.Device.RightHandTracker:
                    return AirVRInput.GetDown(cameraRig, AirVRInput.Button.A) || AirVRInput.GetDown(cameraRig, AirVRInput.Button.RIndexTrigger);
            }
            return false;
        }
    }

    public override bool primaryButtonReleased {
        get {
            switch (device) {
                case AirVRInput.Device.HeadTracker:
                    return AirVRInput.GetUp(cameraRig, AirVRInput.Button.A) || AirVRInput.GetUp(cameraRig, AirVRInput.Touch.Touchpad);
                case AirVRInput.Device.RightHandTracker:
                    return AirVRInput.GetUp(cameraRig, AirVRInput.Button.A) || AirVRInput.GetUp(cameraRig, AirVRInput.Button.RIndexTrigger);
            }
            return false;
        }
    }
}
