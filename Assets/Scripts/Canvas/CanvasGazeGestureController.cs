using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGazeGestureController : GazeGestureController
{
    public override void OnTappedEvent()
    {
        this.BroadcastMessage("ChangeToDragMode", FocusedObject);
    }
}
