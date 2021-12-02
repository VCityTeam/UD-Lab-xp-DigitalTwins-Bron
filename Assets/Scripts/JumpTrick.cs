using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void killTrick() {
        this.ended = true;
        this.skateboard.localPosition = originalSkatePos;
        this.skateboard.localRotation = Quaternion.Euler(0f, 0f, 0f);
        this.player.currentTrick = null;
    }

}
