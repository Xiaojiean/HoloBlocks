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
    void Start ()
    {
        // Creation Commands
        keywords.Add("Create cube", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Create sphere", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        keywords.Add("Create block", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Create ball", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

        keywords.Add("Create ramp", () =>
        {
            this.BroadcastMessage("OnCreateRamp");
        });

        keywords.Add("Create slope", () =>
        {
            this.BroadcastMessage("OnCreateSlope");
        });

        keywords.Add("Create cylinder", () =>
        {
            this.BroadcastMessage("OnCreateCylinder");
        });

        keywords.Add("Create pyramid", () =>
        {
            this.BroadcastMessage("OnCreatePyramid");
        });

        // Deletion Commands
        keywords.Add("Delete all cubes", () =>
        {
            this.BroadcastMessage("OnDeleteAllCubes");
        });

        keywords.Add("Delete all spheres", () =>
        {
            this.BroadcastMessage("OnDeleteAllSpheres");
        });

        keywords.Add("Delete all slopes", () =>
        {
            this.BroadcastMessage("OnDeleteAllSlopes");
        });

        keywords.Add("Delete all ramps", () =>
        {
            this.BroadcastMessage("OnDeleteAllSlopes");
        });

        keywords.Add("Delete all pyramids", () =>
        {
            this.BroadcastMessage("OnDeleteAllPyramids");
        });

        keywords.Add("Delete all cylinders", () =>
        {
            this.BroadcastMessage("OnDeleteAllCylinders");
        });

        keywords.Add("Delete all blocks", () =>
        {
            this.BroadcastMessage("OnDeleteAllCubes");
        });

        keywords.Add("Delete all balls", () =>
        {
            this.BroadcastMessage("OnDeleteAllSpheres");
        });

        keywords.Add("Clear canvas", () =>
        {
            this.BroadcastMessage("OnClearCanvas");
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
