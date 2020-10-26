/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PackageBuilder {
    [MenuItem("onAirVR/Export onAirVR Server...")]
    public static void ExportAirVRServer() {
        string targetPath = EditorUtility.SaveFilePanel("Export onAirVR Server...", "", "onairvr-server", "unitypackage");
        if (string.IsNullOrEmpty(targetPath)) {
            return;
        }

        if (File.Exists(targetPath)) {
            File.Delete(targetPath);
        }
        string[] folders = { "Assets/onAirVR", "Assets/StreamingAssets" };
        string[] guids = AssetDatabase.FindAssets("", folders);
        List<string> assets = new List<string>();
        foreach (string guid in guids) {
            assets.Add(AssetDatabase.GUIDToAssetPath(guid));
        }
        assets.Add("Assets/Plugins/x86_64/AudioPlugin_onAirVRServerPlugin.dll");
        assets.Add("Assets/Plugins/x86_64/onAirVRServerPlugin.dll");
        AssetDatabase.ExportPackage(assets.ToArray(), targetPath);

        EditorUtility.DisplayDialog("Congratulation!", "The package is exported successfully.", "Thanks.");
    }
}
