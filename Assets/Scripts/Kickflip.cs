using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickflip : JumpTrick
{
    public Kickflip(Vector3 originalPos, Transform skateboard, PlayerController player) : base(originalPos, skateboard, player)
    { }

    public override void fixedUpdate()
    {
        frames++;
        skateboard.Rotate(new Vector3(0f, 0f, 6f), Space.Self);

        if (frames > 60)
            killTrick();
    }
}
