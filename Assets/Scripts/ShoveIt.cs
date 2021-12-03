using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveIt : JumpTrick
{
    public ShoveIt(Vector3 originalPos, Transform skateboard, PlayerController player) : base(originalPos, skateboard, player)
    { }

    private static float degreespertick = 13f, framesmax = 180 / degreespertick;

    public override string getDisplayName()
    {
        return "ShoveIt 180!";
    }

    public override float getScore()
    {
        return 0.08f;
    }

    public override void fixedUpdate()
    {
        frames++;

        if (frames == 1)
            incrementScore();

        skateboard.Rotate(new Vector3(0f, degreespertick, 0f), Space.Self);

        if (frames > framesmax)
            killTrick();
    }
}
