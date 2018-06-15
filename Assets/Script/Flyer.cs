using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    private enum EState
    {
        NONE,

        NOMAL,
        UNCONTROLLABLE,
    }

    private enum EAnimState
    {
        NONE,

        IDLE,
        FALLING,
        DEAD,
    }

    private EState          m_eCurState = EState.NONE;
    private EAnimState      m_eCurAnimState = EAnimState.NONE;

    private void SetCurAnimState(EAnimState _eAnimState)
    { m_eCurAnimState = _eAnimState; UpdateAnimation(); }

    // Other Component
    private Rigidbody   m_comRigidBody;
    private Animator    m_comAnimaotr;
    private Controller  m_comController;

    [SerializeField]
    private int         m_iHP = VALUE_GAME_PLAY.MAX_HP_BASIC;
    private float       m_fUncontrollabletimer  = 0.0f;

    void Awake()
    {
        m_comRigidBody = GetComponent<Rigidbody>();
        m_comAnimaotr = GetComponent<Animator>();
        m_comController = GetComponent<Controller>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdatTotalState();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Feather_Basic":
                break;
            case "Feather_Full":
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "Floor":
                SetCurAnimState(EAnimState.DEAD);
                break;
        }
    }

    // 코드가 정리가 안 되는데..
    // 레이싱 게임 예제를 보자..
    private void UpdatTotalState()
    {
        switch(m_eCurState)
        {
           case EState.NONE:
                break;

           case EState.NOMAL:
                break;

            case EState.UNCONTROLLABLE:
                {
                    Debug.Log(m_fUncontrollabletimer);
                    m_fUncontrollabletimer -= Time.smoothDeltaTime;

                    if (m_fUncontrollabletimer <= 0.0f)
                    {
                        m_eCurState = EState.NOMAL;
                        SetCurAnimState(EAnimState.IDLE);

                        m_comRigidBody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

                        m_iHP = VALUE_GAME_PLAY.MAX_HP_BASIC;
                        m_fUncontrollabletimer = 0.0f;
                    }
                }
                break;
        }
        

        if (m_iHP <= 0 && m_eCurState != EState.UNCONTROLLABLE)
        {
            m_eCurState = EState.UNCONTROLLABLE;
            SetCurAnimState(EAnimState.FALLING);
            m_fUncontrollabletimer = VALUE_GAME_PLAY.MAX_UNCONTROLLABLE_TIME_FALLING;
        }
    }

    private void UpdateAnimation()
    {
        switch (m_eCurAnimState)
        {
            case EAnimState.NONE:
                break;

            case EAnimState.IDLE:
                m_comAnimaotr.Play("FA_IdleFly");
                m_comController.enabled = true;
                m_comRigidBody.useGravity = false;
                break;

            case EAnimState.FALLING:
                m_comAnimaotr.Play("FA_Falling");
                m_comController.enabled = false;
                m_comRigidBody.useGravity = true;
                break;

            case EAnimState.DEAD:
                m_comAnimaotr.Play("FA_Dead");
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
