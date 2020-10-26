/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

public class AirVRTrackedControllerPointer : AirVRPointer {
    // implements AirVRPointer
    protected override AirVRInput.Device device {
        get {
            return AirVRInput.Device.RightHandTracker;
        }
    }

    public override bool primaryButtonPressed {
        get {
            return AirVRInput.GetDown(cameraRig, AirVRInput.Button.A) || AirVRInput.GetDown(cameraRig, AirVRInput.Button.RIndexTrigger);
        }
    }

    public override bool primaryButtonReleased {
        get {
            return AirVRInput.GetUp(cameraRig, AirVRInput.Button.A) || AirVRInput.GetUp(cameraRig, AirVRInput.Button.RIndexTrigger);
        }
    }
}
