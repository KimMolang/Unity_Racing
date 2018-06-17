using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(FlyerController))]
public class FlyerAnimation : MonoBehaviour
{
    public enum EAnimState
    {
        NONE,

        IDLE,
        FALLING,
        DEAD,
    }

    private EAnimState      m_eCurAnimState = EAnimState.NONE;

    // Other Component
    private Rigidbody   m_comRigidBody;
    private Animator    m_comAnimaotr;
    private FlyerController m_comFlyerController;

    void Awake()
    {
        m_comRigidBody = GetComponent<Rigidbody>();
        m_comAnimaotr = GetComponent<Animator>();
        m_comFlyerController = GetComponent<FlyerController>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurAnimState(EAnimState _eAnimState)
    {
        EAnimState ePreviousAnimState = m_eCurAnimState;
        m_eCurAnimState = _eAnimState;

        switch (m_eCurAnimState)
        {
            case EAnimState.NONE:
                break;

            case EAnimState.IDLE:
                m_comAnimaotr.CrossFade("FA_IdleFly", 0.2f);
                
                // (수정)
                // 애니메이션 보간이 끝난 후 enabled 처리를 해야한다
                m_comFlyerController.enabled = true;
                m_comRigidBody.useGravity = false;

                // 계속 추락 중이었다면
                if(ePreviousAnimState == EAnimState.FALLING)
                {
                    // (수정)
                    // 바로 0 주지말고 자연스럽게 처리해야함.
                    // 가속도 먼저 처리하고
                    // 앞으로 가던 속력 보면서 처리 하자
                    m_comRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
                break;

            case EAnimState.FALLING:
                //m_comAnimaotr.Play("FA_Falling");
                m_comAnimaotr.CrossFade("FA_Falling", 0.2f);
                m_comFlyerController.enabled = false;
                m_comRigidBody.useGravity = true;
                break;

            case EAnimState.DEAD:
                //m_comAnimaotr.Play("FA_Dead");
                m_comAnimaotr.CrossFade("FA_Dead", 0.2f);
                m_comRigidBody.useGravity = false;
                m_comRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }
    }

    private void EndAnimation()
    {
        switch (m_eCurAnimState)
        {
            case EAnimState.NONE:
                Debug.Log("11");
                break;

            case EAnimState.FALLING:
                Debug.Log("22");
                break;

            case EAnimState.DEAD:
                Debug.Log("33");
                break;
        }
    }
}
