using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class VoiceController : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        // Create a cube
        keywords.Add("Make block", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Make box", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Make square", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        // Create a sphere
        keywords.Add("Make ball", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        keywords.Add("Make circle", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        keywords.Add("Make sphere", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        // Create a cylinder
        keywords.Add("Make tube", () =>
        {
            this.BroadcastMessage("OnCreateCylinder");
        });

        keywords.Add("Make cylinder", () =>
        {
            this.BroadcastMessage("OnCreateCylinder");
        });

        // Create a pyramid
        keywords.Add("Make triangle", () =>
        {
            this.BroadcastMessage("OnCreatePyramid");
        });

        keywords.Add("Make pyramid", () =>
        {
            this.BroadcastMessage("OnCreatePyramid");
        });

        // Create a slope
        keywords.Add("Make ramp", () =>
        {
            this.BroadcastMessage("OnCreateSlope");
        });

        keywords.Add("Make slope", () =>
        {
            this.BroadcastMessage("OnCreateSlope");
        });

        // Change color to red
        keywords.Add("Color red", () =>
        {
            this.BroadcastMessage("OnChangeColorToRed", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make red", () =>
        {
            this.BroadcastMessage("OnChangeColorToRed", CanvasGazeGestureController.FocusedObject);
        });

        // Change color to blue
        keywords.Add("Color blue", () =>
        {
            this.BroadcastMessage("OnChangeColorToBlue", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make blue", () =>
        {
            this.BroadcastMessage("OnChangeColorToBlue", CanvasGazeGestureController.FocusedObject);
        });

        // Change color to green
        keywords.Add("Color green", () =>
        {
            this.BroadcastMessage("OnChangeColorToGreen", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make green", () =>
        {
            this.BroadcastMessage("OnChangeColorToGreen", CanvasGazeGestureController.FocusedObject);
        });

        // Change color to yellow
        keywords.Add("Color yellow", () =>
        {
            this.BroadcastMessage("OnChangeColorToYellow", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make yellow", () =>
        {
            this.BroadcastMessage("OnChangeColorToYellow", CanvasGazeGestureController.FocusedObject);
        });

        // Change color to white
        keywords.Add("Color white", () =>
        {
            this.BroadcastMessage("OnChangeColorToWhite", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make white", () =>
        {
            this.BroadcastMessage("OnChangeColorToWhite", CanvasGazeGestureController.FocusedObject);
        });

        // Increase size
        keywords.Add("Bigger", () =>
        {
            this.BroadcastMessage("OnIncreaseGameObjectSize", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make bigger", () =>
        {
            this.BroadcastMessage("OnIncreaseGameObjectSize", CanvasGazeGestureController.FocusedObject);
        });

        // Decrease size
        keywords.Add("Smaller", () =>
        {
            this.BroadcastMessage("OnDecreaseGameObjectSize", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Make smaller", () =>
        {
            this.BroadcastMessage("OnDecreaseGameObjectSize", CanvasGazeGestureController.FocusedObject);
        });

        // Delete focused object
        keywords.Add("Erase", () =>
        {
            this.BroadcastMessage("OnDelete", CanvasGazeGestureController.FocusedObject);
        });

        // Delete all objects
        keywords.Add("Erase all", () =>
        {
            this.BroadcastMessage("OnClearCanvas");
        });

        // Rotate the focused object around the y axis clockwise by 45 degrees.
        keywords.Add("Turn", () =>
        {
            this.BroadcastMessage("OnRotateY", CanvasGazeGestureController.FocusedObject);
        });

        // Rotate the focused object around the z axis clockwise by 45 degrees.
        keywords.Add("Flip", () =>
        {
            this.BroadcastMessage("OnRotateZ", CanvasGazeGestureController.FocusedObject);
        });

        // Copy focused object
        keywords.Add("Copy", () =>
        {
            this.BroadcastMessage("OnCopy", CanvasGazeGestureController.FocusedObject);
        });

        // Paste
        keywords.Add("Paste", () =>
        {
            this.BroadcastMessage("OnPaste");
        });

        // Turn physics on
        keywords.Add("Gravity on", () =>
        {
            this.BroadcastMessage("TurnOnPhysics");
        });
        
        // Turn physics off
        keywords.Add("Gravity off", () =>
        {
            this.BroadcastMessage("TurnOffPhysics");
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
