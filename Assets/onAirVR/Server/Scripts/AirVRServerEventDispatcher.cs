/***********************************************************

  Copyright (c) 2017-present Clicked, Inc.

  Licensed under the MIT license found in the LICENSE file 
  in the Docs folder of the distributed package.

 ***********************************************************/

using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class AirVRServerEventDispatcher : AirVREventDispatcher {
    [DllImport(AirVRServerPlugin.Name)]
    private static extern bool onairvr_CheckMessageQueue(out IntPtr source, out IntPtr data, out int length);

    [DllImport(AirVRServerPlugin.Name)]
    private static extern void onairvr_RemoveFirstMessage();

    protected override AirVRMessage ParseMessageImpl(IntPtr source, string message) {
        return AirVRServerMessage.Parse(source, message);
    }

    protected override bool CheckMessageQueueImpl(out IntPtr source, out IntPtr data, out int length) {
        return onairvr_CheckMessageQueue(out source, out data, out length);
    }

    protected override void RemoveFirstMessageFromQueueImpl() {
        onairvr_RemoveFirstMessage();
    }
}
