using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyerAnimation))]
public class FlyerStateInfo : MonoBehaviour
{
    public enum StateID
    {
        NONE,

        NOMAL,
        UNCONTROLLABLE,
    }

    private StateID curState = StateID.NONE;
    public StateID GetCurState() { return curState; }

    // Other Component
    private FlyerAnimation m_comFlyerAnimation;

    [SerializeField] private int HP = VALUE_GAME_PLAY.MAX_HP_BASIC;
    private float uncontrollableTimer  = 0.0f;

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
        switch(curState)
        {
           case StateID.NONE:
                break;

           case StateID.NOMAL:
                break;

            case StateID.UNCONTROLLABLE:
                {
                    //Debug.Log(uncontrollableTimer);
                    uncontrollableTimer -= Time.smoothDeltaTime;

                    if (uncontrollableTimer <= 0.0f)
                    {
                        curState = StateID.NOMAL;
                        m_comFlyerAnimation.SetCurAnimState(FlyerAnimation.AnimStateID.IDLE);

                        HP = VALUE_GAME_PLAY.MAX_HP_BASIC;
                        uncontrollableTimer = 0.0f;
                    }
                }
                break;
        }
        

        if (HP <= 0 && curState != StateID.UNCONTROLLABLE)
        {
            curState = StateID.UNCONTROLLABLE;
            m_comFlyerAnimation.SetCurAnimState(FlyerAnimation.AnimStateID.FALLING);
            uncontrollableTimer = VALUE_GAME_PLAY.MAX_UNCONTROLLABLE_TIME_FALLING;
        }
    }
}
