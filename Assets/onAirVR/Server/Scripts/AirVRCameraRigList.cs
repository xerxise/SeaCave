/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using System.Collections.Generic;

public class AirVRCameraRigList {
    private Dictionary<AirVRClientType, List<AirVRCameraRig>> _cameraRigsAvailable;
    private Dictionary<AirVRClientType, List<AirVRCameraRig>> _cameraRigsRetained;

    public AirVRCameraRigList() {
        _cameraRigsAvailable = new Dictionary<AirVRClientType, List<AirVRCameraRig>>();
        _cameraRigsRetained = new Dictionary<AirVRClientType, List<AirVRCameraRig>>();
    }

    private AirVRCameraRig getBoundCameraRig(AirVRClientType type, int playerID) {
        if (_cameraRigsRetained.ContainsKey(type)) {
            foreach (var cameraRig in _cameraRigsRetained[type]) {
                if (cameraRig.playerID == playerID) {
                    return cameraRig;
                }
            }
        }
        return null;
    }

    public void GetAllCameraRigs(List<AirVRCameraRig> result) {
        foreach (var key in _cameraRigsRetained.Keys) {
            result.AddRange(_cameraRigsRetained[key]);
        }
        foreach (var key in _cameraRigsAvailable.Keys) {
            result.AddRange(_cameraRigsAvailable[key]);
        }
    }

    public void GetAvailableCameraRigs(AirVRClientType type, List<AirVRCameraRig> result) {
        if (_cameraRigsAvailable.ContainsKey(type)) {
            result.AddRange(_cameraRigsAvailable[type]);
        }
    }

    public void GetAllRetainedCameraRigs(List<AirVRCameraRig> result) {
        foreach (var key in _cameraRigsRetained.Keys) {
            result.AddRange(_cameraRigsRetained[key]);
        }
    }

    public AirVRCameraRig GetBoundCameraRig(int playerID) {
        if (playerID >= 0) {
            return getBoundCameraRig(AirVRClientType.Stereoscopic, playerID) ?? 
                   getBoundCameraRig(AirVRClientType.Monoscopic, playerID);
        }
        return null;
    }

    public void AddUnboundCameraRig(AirVRCameraRig cameraRig) {
        if (_cameraRigsAvailable.ContainsKey(cameraRig.type) == false) {
            _cameraRigsAvailable.Add(cameraRig.type, new List<AirVRCameraRig>());
        }
        if (_cameraRigsRetained.ContainsKey(cameraRig.type) == false) {
            _cameraRigsRetained.Add(cameraRig.type, new List<AirVRCameraRig>());
        }
        if (_cameraRigsAvailable[cameraRig.type].Contains(cameraRig) == false &&
            _cameraRigsRetained[cameraRig.type].Contains(cameraRig) == false) {
            _cameraRigsAvailable[cameraRig.type].Add(cameraRig);
        }
    }

    public void RemoveCameraRig(AirVRCameraRig cameraRig) {
        if (_cameraRigsAvailable.ContainsKey(cameraRig.type) == false ||
            _cameraRigsRetained.ContainsKey(cameraRig.type) == false) {
            return;
        }

        if (_cameraRigsAvailable[cameraRig.type].Contains(cameraRig)) {
            _cameraRigsAvailable[cameraRig.type].Remove(cameraRig);
        }
        else if (_cameraRigsRetained[cameraRig.type].Contains(cameraRig)) {
            _cameraRigsRetained[cameraRig.type].Remove(cameraRig);
        }
    }

    public AirVRCameraRig RetainCameraRig(AirVRCameraRig cameraRig) {
        if (_cameraRigsAvailable.ContainsKey(cameraRig.type) && _cameraRigsRetained.ContainsKey(cameraRig.type)) {
            if (_cameraRigsAvailable[cameraRig.type].Contains(cameraRig)) {
                _cameraRigsAvailable[cameraRig.type].Remove(cameraRig);
                _cameraRigsRetained[cameraRig.type].Add(cameraRig);
                return cameraRig;
            }
        }
        return null;
    }

    public void ReleaseCameraRig(AirVRCameraRig cameraRig) {
        if (_cameraRigsAvailable.ContainsKey(cameraRig.type) && _cameraRigsRetained.ContainsKey(cameraRig.type)) {
            if (_cameraRigsRetained[cameraRig.type].Contains(cameraRig)) {
                _cameraRigsRetained[cameraRig.type].Remove(cameraRig);
                _cameraRigsAvailable[cameraRig.type].Add(cameraRig);
            }
        }
    }
}
