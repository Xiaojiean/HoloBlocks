using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class StartMenuGazeGestureController : GazeGestureController
{
    public override void OnTappedEvent()
    {
        FocusedObject.SendMessageUpwards("OnSelect");
    }
}
