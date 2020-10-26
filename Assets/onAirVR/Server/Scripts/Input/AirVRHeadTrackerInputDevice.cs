/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

public class AirVRHeadTrackerInputDevice : AirVRInputDevice {
    // implements AirVRInputDevice
    protected override string deviceName {
        get {
            return AirVRInputDeviceName.HeadTracker;
        }
    }

    protected override void MakeControlList() {
        AddControlTransform((byte)AirVRHeadTrackerKey.Transform);
    }

    protected override void UpdateExtendedControls() {}
}
