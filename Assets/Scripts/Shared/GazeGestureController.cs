﻿using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureController : MonoBehaviour
{
    // Represents the hologram that is currently being gazed at.
    public static GameObject FocusedObject { get; private set; }

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Call OnTappedEvent when the user selects a hologram
            if (FocusedObject != null)
            {
                OnTappedEvent();
            }
        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        // Layer Mask that masks out the spatial mapping layer. This is to ensure that raycast
        // doesn't hit the spatial mapping prefab.
        int layerMask = 1 << LayerMask.NameToLayer("SpatialMapping");
        layerMask = ~layerMask;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo, Mathf.Infinity, layerMask))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }

    public virtual void OnTappedEvent() { }
}
