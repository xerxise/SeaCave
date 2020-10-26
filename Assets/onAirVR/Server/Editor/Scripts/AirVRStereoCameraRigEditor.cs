/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEditor;

[CustomEditor(typeof(AirVRStereoCameraRig))]

public class AirVRStereoCameraRigEditor : Editor {
    private SerializedProperty propTrackingModel;
    private SerializedProperty propExternalTrackingOrigin;
    private SerializedProperty propExternalTracker;

    private void OnEnable() {
        propTrackingModel = serializedObject.FindProperty("trackingModel");
        propExternalTrackingOrigin = serializedObject.FindProperty("externalTrackingOrigin");
        propExternalTracker = serializedObject.FindProperty("externalTracker");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propTrackingModel);
        if (propTrackingModel.enumValueIndex == (int)AirVRStereoCameraRig.TrackingModel.ExternalTracker) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(propExternalTrackingOrigin);
            EditorGUILayout.PropertyField(propExternalTracker);
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
