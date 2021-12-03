using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindTrick : JumpTrick
{
    public GrindTrick(Vector3 originalPos, Transform skateboard, PlayerController player) : base(originalPos, skateboard, player)
    { }

    private static float degreespertick = 25f;

    public override string getDisplayName()
    {
        return "Grind " + frames + "!";
    }

    public override float getScore()
    {
        return 0.002f;
    }

    public override void fixedUpdate()
    {
        frames++;
        incrementScore();

        skateboard.Rotate(new Vector3(0f, degreespertick, 0f), Space.Self);

        this.player.skaterbody.velocity = Vector3.zero;
    }
}
