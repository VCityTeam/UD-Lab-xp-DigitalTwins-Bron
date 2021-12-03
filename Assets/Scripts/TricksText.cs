using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksText : MonoBehaviour
{
    private string buffer = "Starting!";
    private int frames = 0;

    void FixedUpdate()
    {
        frames ++;

        if (frames == 1)
            GetComponent<TMPro.TextMeshProUGUI>().text = buffer;
        else if (frames == 38)
            GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    public void setTrickText(string display) {
        this.buffer = display;
        this.frames = 0;
    }
}
