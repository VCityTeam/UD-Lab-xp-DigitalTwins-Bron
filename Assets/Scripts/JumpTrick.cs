using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class JumpTrick
{
    protected Vector3 originalSkatePos;
    protected Transform skateboard;
    protected PlayerController player;
    public bool ended = false;

    public int frames = 0;

    public JumpTrick(Vector3 originalPos, Transform skateboard, PlayerController player) {
        this.originalSkatePos = originalPos;
        this.skateboard = skateboard;
        this.player = player;
    }

    public abstract void fixedUpdate();

    public abstract string getDisplayName();

    public abstract float getScore();

    public void killTrick() {
        this.ended = true;
        this.skateboard.localPosition = originalSkatePos;
        this.skateboard.localRotation = Quaternion.Euler(0f, 0f, 0f);
        this.player.currentTrick = null;
    }

    public void incrementScore()
    {
        Image powerimg = player.ui.transform.Find("Power").GetComponent<Image>();
        float filling = powerimg.fillAmount + getScore();
        powerimg.fillAmount = filling > 1f ? 1f : filling;
    }

}
