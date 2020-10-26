/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;

public class AirVRServerInitParams : MonoBehaviour {
    public string licenseFilePath       = AirVRServerParams.DefaultLicense;
    public int maxClientCount           = AirVRServerParams.DefaultMaxClientCount;
    public int port                     = AirVRServerParams.DefaultPort;
    public int videoBitrate             = AirVRServerParams.DefaultDefaultVideoBitrate;
    public float maxFrameRate           = AirVRServerParams.DefaultMaxFrameRate;
    public float defaultFrameRate       = AirVRServerParams.DefaultDefaultFrameRate;
    
    void Start() {
        Destroy(gameObject);
    }
}
