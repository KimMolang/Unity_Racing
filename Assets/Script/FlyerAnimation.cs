using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FlyerController))]
public class FlyerAnimation : MonoBehaviour
{
    public enum AnimStateID
    {
        NONE,

        IDLE,
        FALLING,
        DEAD,
    }

    private AnimStateID curAnimState = AnimStateID.NONE;

    // Other Component
    private Rigidbody rigidBody;
    private Animator animaotr;
    private FlyerController flyerController;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animaotr = GetComponent<Animator>();
        flyerController = GetComponent<FlyerController>();

        rigidBody.constraints
            = RigidbodyConstraints.FreezePositionY
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY
            | RigidbodyConstraints.FreezeRotationZ;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurAnimState(AnimStateID _AnimStateID)
    {
        AnimStateID ePreviousAnimState = curAnimState;
        curAnimState = _AnimStateID;

        switch (curAnimState)
        {
            case AnimStateID.NONE:
                break;

            case AnimStateID.IDLE:
                animaotr.CrossFade("FA_IdleFly", 0.2f);

                rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;

                if (ePreviousAnimState == AnimStateID.FALLING
                    || ePreviousAnimState == AnimStateID.DEAD)
                {
                    StartCoroutine(FallingAndDeadThenIdle());
                }
                break;

            case AnimStateID.FALLING:
                //animaotr.Play("FA_Falling");
                animaotr.CrossFade("FA_Falling", 0.2f);
                flyerController.enabled = false;
                rigidBody.useGravity = true;

                rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                break;

            case AnimStateID.DEAD:
                //animaotr.Play("FA_Dead");
                animaotr.CrossFade("FA_Dead", 0.2f);
                rigidBody.useGravity = false;
                rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
    }

    private void EndAnimation()
    {
        switch (curAnimState)
        {
            case AnimStateID.NONE:
                Debug.Log("11");
                break;

            case AnimStateID.FALLING:
                Debug.Log("22");
                break;

            case AnimStateID.DEAD:
                Debug.Log("33");
                break;
        }
    }

    private IEnumerator FallingAndDeadThenIdle()
    {
        // (수정)
        // 바로 0 주지말고 자연스럽게 처리해야함.
        // 가속도 먼저 처리하고
        // 앞으로 가던 속력 보면서 처리 하자

        rigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

        yield return new WaitForSeconds(0.5f);

        flyerController.enabled = true;
        rigidBody.useGravity = false;
    }
}