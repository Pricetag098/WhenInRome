using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificLevel : MonoBehaviour
{
    public int Level;

    [ContextMenu("Load")]
    public void Load()
    {
        SceneManager.LoadScene(Level);
    }
}
