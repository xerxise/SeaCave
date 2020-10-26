/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;
using UnityEngine.EventSystems;

internal class AirVRPointerEventData : PointerEventData {
    public AirVRPointerEventData(EventSystem eventSystem) : base(eventSystem) { }

    public Ray worldSpaceRay;
}

internal static class PointerEventDataExtension {
    public static bool IsVRPointer(this PointerEventData pointerEventData) {
        return pointerEventData is AirVRPointerEventData;
    }

    public static Ray GetRay(this PointerEventData pointerEventData) {
        return (pointerEventData as AirVRPointerEventData).worldSpaceRay;
    }
}