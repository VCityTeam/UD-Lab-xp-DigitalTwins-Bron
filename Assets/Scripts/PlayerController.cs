using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimStates
{
    skating = 1, paddling = 2
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody skaterbody;
    [SerializeField]
    private Animator bodymator;

    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float turncurrent;

    public float jumpspeed = 2.8f;
    private bool bufferedJump = false;
    private float disttoground;
    private bool grounded = false;

    private Vector3 originalSkatePos;
    private Transform skateTransform;

    public JumpTrick currentTrick;

    private void Start()
    {
        this.skaterbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        disttoground = GetComponent<CapsuleCollider>().bounds.extents.y;
        foreach (Transform child in transform)
            if (child.CompareTag("skateboard")) {
                originalSkatePos = child.localPosition;
                skateTransform = child;
            }
        bodymator.SetInteger(animatorstateid, (int)AnimStates.skating);
    }

    private int animatorstateid = Animator.StringToHash("State");

    void FixedUpdate()
    {
        float hori = Input.GetAxis("Horizontal"), verti = Input.GetAxis("Vertical");
        bool near0 = Mathf.Approximately(hori, 0f) && Mathf.Approximately(verti, 0f);

        // Compute mouse and keys movement
        if (!near0)
        {
            // Computes the wanted input direction with Y velocity untouched
            float vertmomentum = skaterbody.velocity.y;
            Vector3 direction = new Vector3(hori, 0, verti).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed;
            direction = new Vector3(direction.x, vertmomentum, direction.z);

            // Updates the player to appear with smooth facing
            float visualangle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turncurrent, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, visualangle, 0f);

            // Apply the moving velocity
            skaterbody.velocity = direction;
        }

        if (bufferedJump)
        {
            // Jump physics
            bufferedJump = false;
            skaterbody.velocity = new Vector3(skaterbody.velocity.x, skaterbody.velocity.y + jumpspeed, skaterbody.velocity.z);
            grounded = false;
            // Jump tricks
            currentTrick = new Kickflip(originalSkatePos,skateTransform, this);
        }

        if (currentTrick != null)
            currentTrick.fixedUpdate();

        if (grounded && currentTrick != null) {
            currentTrick.killTrick();
        }

    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, -Vector3.up, disttoground + 0.1f);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            bufferedJump = true;
    }
}
