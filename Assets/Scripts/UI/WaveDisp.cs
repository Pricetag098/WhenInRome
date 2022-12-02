using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisp : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateText(int num)
    {
        text.text = "wave <" + num.ToString() +">";
    }

}
