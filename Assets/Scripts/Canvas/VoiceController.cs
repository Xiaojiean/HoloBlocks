using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceController : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start ()
    {

		// Create
        keywords.Add("Create cube", () =>
        {
            this.BroadcastMessage("OnCreateCube");
        });

        keywords.Add("Create sphere", () =>
        {
            this.BroadcastMessage("OnCreateSphere");
        });

		// Delete
        keywords.Add("Delete all cubes", () =>
        {
            this.BroadcastMessage("OnDeleteAllCubes");
        });

        keywords.Add("Delete all spheres", () =>
        {
            this.BroadcastMessage("OnDeleteAllSpheres");
        });

        keywords.Add("Clear canvas", () =>
        {
            this.BroadcastMessage("OnClearCanvas");
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
