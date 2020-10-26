using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVRRightHandTrackerInputDevice : AirVRInputDevice {
    // implements AirVRInputDevice
    protected override string deviceName {
        get {
            return AirVRInputDeviceName.RightHandTracker;
        }
    }

    protected override void MakeControlList() {
        AddControlTransform((byte)AirVRRightHandTrackerKey.Transform);
    }
}
