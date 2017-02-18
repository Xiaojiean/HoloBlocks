using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Called by GazeGestureController when the user performs a Select gesture
    void OnSelect()
    {
        SceneManager.LoadScene("Canvas");
    }
}
