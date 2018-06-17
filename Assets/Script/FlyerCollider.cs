using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyerAnimation))]
public class FlyerCollider : MonoBehaviour
{
    // Other Component
    private FlyerAnimation m_comFlyerAnimation;
    private FlyerStateInfo m_comFlyerStateInfo;

    void Awake()
    {
        m_comFlyerAnimation = GetComponent<FlyerAnimation>();
        m_comFlyerStateInfo = GetComponent<FlyerStateInfo>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                if(m_comFlyerStateInfo.GetCurState() == FlyerStateInfo.EState.UNCONTROLLABLE)
                    m_comFlyerAnimation.SetCurAnimState(FlyerAnimation.EAnimState.DEAD);
                break;
        }
    }
}
