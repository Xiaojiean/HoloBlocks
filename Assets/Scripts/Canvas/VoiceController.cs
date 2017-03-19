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
        // Creation Commands
        keywords.Add("Block", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Ball", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        keywords.Add("Tube", () =>
        {
            this.BroadcastMessage("OnCreateCylinder");
        });

        keywords.Add("Roof", () =>
        {
            this.BroadcastMessage("OnCreatePyramid");
        });

        keywords.Add("Ramp", () =>
        {
            this.BroadcastMessage("OnCreateSlope");
        });

        // Color Commands
        keywords.Add("Red", () =>
        {
            this.BroadcastMessage("OnChangeColorToRed");
        });

        keywords.Add("Blue", () =>
        {
            this.BroadcastMessage("OnChangeColorToBlue");
        });

        keywords.Add("Green", () =>
        {
            this.BroadcastMessage("OnChangeColorToGreen");
        });

        keywords.Add("Yellow", () =>
        {
            this.BroadcastMessage("OnChangeColorToYellow");
        });

        // Resize Commands
        keywords.Add("Bigger", () =>
        {
            this.BroadcastMessage("OnIncreaseGameObjectSize");
        });

        keywords.Add("Smaller", () =>
        {
            this.BroadcastMessage("OnDecreaseGameObjectSize");
        });

        // Deletion Commands
        keywords.Add("Erase", () =>
        {
            this.BroadcastMessage("OnDelete", CanvasGazeGestureController.FocusedObject);
        });

        keywords.Add("Erase blocks", () =>
        {
            this.BroadcastMessage("OnDeleteAllCubes");
        });

        keywords.Add("Erase balls", () =>
        {
            this.BroadcastMessage("OnDeleteAllSpheres");
        });

        keywords.Add("Erase ramps", () =>
        {
            this.BroadcastMessage("OnDeleteAllSlopes");
        });

        keywords.Add("Erase roofs", () =>
        {
            this.BroadcastMessage("OnDeleteAllPyramids");
        });

        keywords.Add("Erase tubes", () =>
        {
            this.BroadcastMessage("OnDeleteAllCylinders");
        });

        keywords.Add("Erase all", () =>
        {
            this.BroadcastMessage("OnClearCanvas");
        });

        // Spin Commands
        keywords.Add("Spin", () =>
        {
            this.BroadcastMessage("ChangeToRotateModeY");
        });

        keywords.Add("Stop", () =>
        {
            this.BroadcastMessage("OnChangeToStaticMode");
        });

        // Menu Command
        keywords.Add("Start menu", () =>
        {
            SceneManager.LoadScene("StartMenu");
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
