/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using System.Collections.Generic;
using UnityEngine;

public class AirVRSampleAdvancedScene : MonoBehaviour, AirVRCameraRigManager.EventHandler {
    [SerializeField] private AirVRCameraRig _primaryCameraRig = null;

    void Awake() {
        AirVRCameraRigManager.managerOnCurrentScene.Delegate = this;
    }

    // implements AirVRCameraRigMananger.EventHandler
    public void AirVRCameraRigWillBeBound(int clientHandle, AirVRClientConfig config, List<AirVRCameraRig> availables, out AirVRCameraRig selected) {
        if (availables.Contains(_primaryCameraRig)) {
            selected = _primaryCameraRig;
        }
        else if (availables.Count > 0) {
            selected = availables[0];
        }
        else {
            selected = null;
        }
    }

    public void AirVRCameraRigActivated(AirVRCameraRig cameraRig) {}
    public void AirVRCameraRigDeactivated(AirVRCameraRig cameraRig) {}
    public void AirVRCameraRigHasBeenUnbound(AirVRCameraRig cameraRig) {}
    public void AirVRCameraRigUserDataReceived(AirVRCameraRig cameraRig, byte[] data) {}
}
