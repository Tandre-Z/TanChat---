using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    bool cursorDisplay = false;
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor ||
                        Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (!cursorDisplay&&Input.GetKeyDown(KeyCode.Escape))
            {
                cursorDisplay = false;
                StartCoroutine(CursorDisplay());
            }
        }

    }

    public void Close()
    {
        Application.Quit();
    }

    IEnumerator CursorDisplay()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(3f);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
