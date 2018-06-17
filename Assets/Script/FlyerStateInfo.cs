using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyerAnimation))]
public class FlyerStateInfo : MonoBehaviour
{
    public enum EState
    {
        NONE,

        NOMAL,
        UNCONTROLLABLE,
    }

    private EState m_eCurState = EState.NONE;
    public EState GetCurState() { return m_eCurState; }

    // Other Component
    private FlyerAnimation m_comFlyerAnimation;

    [SerializeField] private int m_iHP = VALUE_GAME_PLAY.MAX_HP_BASIC;
    private float m_fUncontrollabletimer  = 0.0f;

    void Awake()
    {
        m_comFlyerAnimation = GetComponent<FlyerAnimation>();
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
                    //Debug.Log(m_fUncontrollabletimer);
                    m_fUncontrollabletimer -= Time.smoothDeltaTime;

                    if (m_fUncontrollabletimer <= 0.0f)
                    {
                        m_eCurState = EState.NOMAL;
                        m_comFlyerAnimation.SetCurAnimState(FlyerAnimation.EAnimState.IDLE);

                        m_iHP = VALUE_GAME_PLAY.MAX_HP_BASIC;
                        m_fUncontrollabletimer = 0.0f;
                    }
                }
                break;
        }
        

        if (m_iHP <= 0 && m_eCurState != EState.UNCONTROLLABLE)
        {
            m_eCurState = EState.UNCONTROLLABLE;
            m_comFlyerAnimation.SetCurAnimState(FlyerAnimation.EAnimState.FALLING);
            m_fUncontrollabletimer = VALUE_GAME_PLAY.MAX_UNCONTROLLABLE_TIME_FALLING;
        }
    }
}
