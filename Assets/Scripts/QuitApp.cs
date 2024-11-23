using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApp : MonoBehaviour
{

    void Update()
    {
        ExitGame();
    }

    void ExitGame()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("EXIT GAME");
            Application.Quit();
        }
    }
}
