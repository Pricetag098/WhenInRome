using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    [ContextMenu("Exit")]
    public void Exit()
    {
        Application.Quit();
    }
}
