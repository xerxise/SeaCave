/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AirVRSamplePointerScene : MonoBehaviour {
    private const string BasicSampleSceneName = "A. Basic";

    private bool _loadingBasicScene;

    private IEnumerator loadScene(string sceneName) {
        yield return StartCoroutine(AirVRCameraFade.FadeAllCameras(this, false, 0.5f));
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Start() {
        yield return StartCoroutine(AirVRCameraFade.FadeAllCameras(this, true, 0.5f));
    }

    public void GoToBasicScene() {
        if (_loadingBasicScene == false) {
            _loadingBasicScene = true;

            StartCoroutine(loadScene(BasicSampleSceneName));
        }
    }
}
