using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindLine : MonoBehaviour
{
    private Vector3 end1;
    private Vector3 end2;

    private bool active = false;
    private PlayerController controller;
    private Vector3 endgoal;

    private int usecooldown = 0;

    void Start()
    {
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        float radius;
        ToWorldSpaceCapsule(collider, out end1, out end2, out radius);
    }

    void FixedUpdate()
    {
        if (!active)
        {
            usecooldown--;
        }
        else
        {
            if (!(controller.currentTrick is GrindTrick))
            {
                active = false;
            }
            else
            {
                Debug.Log("GRINDING CODE HERE!!!");
                Vector3 playerpos = controller.gameObject.transform.position;
                Vector3 grinddir = (endgoal - playerpos);
                float distleft = grinddir.magnitude;
                grinddir = grinddir.normalized * controller.speed;
                Debug.Log(distleft);

                controller.gameObject.transform.position += grinddir * 0.015f;

                if (distleft < 0.8f) {
                    controller.currentTrick.killTrick();
                    controller.bodymator.SetInteger(Animator.StringToHash("State"), (int)AnimStates.skating);
                }
            }
        }
    }
private void OnTriggerEnter(Collider col)
{
    if (usecooldown <= 0 && col.gameObject.name == "Player")
    {
        usecooldown = 50;
        Debug.Log("Entered Collision");
        active = true;
        controller = col.gameObject.GetComponent<PlayerController>();

        // Grinds to the furthest point on the line
        float dist1 = (col.gameObject.transform.position - end1).magnitude, dist2 = (col.gameObject.transform.position - end2).magnitude;
        endgoal = dist1 > dist2 ? end1 : end2;

        // Sets the trick to grinding
        controller.setGrindingState();
    }
}

// 0 Idea what this does, btw. If it doesn't work, blame and issue https://github.com/justonia/UnityExtensions/blob/master/PhysicsExtensions.cs :)
private static void ToWorldSpaceCapsule(CapsuleCollider capsule, out Vector3 point0, out Vector3 point1, out float radius)
{
    var center = capsule.transform.TransformPoint(capsule.center);
    radius = 0f;
    float height = 0f;
    Vector3 lossyScale = AbsVec3(capsule.transform.lossyScale);
    Vector3 dir = Vector3.zero;

    switch (capsule.direction)
    {
        case 0: // x
            radius = Mathf.Max(lossyScale.y, lossyScale.z) * capsule.radius;
            height = lossyScale.x * capsule.height;
            dir = capsule.transform.TransformDirection(Vector3.right);
            break;
        case 1: // y
            radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
            height = lossyScale.y * capsule.height;
            dir = capsule.transform.TransformDirection(Vector3.up);
            break;
        case 2: // z
            radius = Mathf.Max(lossyScale.x, lossyScale.y) * capsule.radius;
            height = lossyScale.z * capsule.height;
            dir = capsule.transform.TransformDirection(Vector3.forward);
            break;
    }

    if (height < radius * 2f)
    {
        dir = Vector3.zero;
    }

    point0 = center + dir * (height * 0.5f - radius);
    point1 = center - dir * (height * 0.5f - radius);
}
private static Vector3 AbsVec3(Vector3 v)
{
    return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
}

}
