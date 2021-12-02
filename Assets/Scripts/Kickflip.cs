using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickflip : JumpTrick
{
    public Kickflip(Vector3 originalPos, Transform skateboard, PlayerController player) : base(originalPos, skateboard, player)
    { }

    private static float degreespertick = 15f, framesmax = 360 / degreespertick;

    public override string getDisplayName()
    {
        return "KickFlip!";
    }

    public override float getScore() {
        return 0.1f;
    }

    public override void fixedUpdate()
    {
        frames++;

        if (frames == 1)
            incrementScore();

        skateboard.Rotate(new Vector3(0f, 0f, degreespertick), Space.Self);

        if (frames > framesmax)
            killTrick();
    }
}
