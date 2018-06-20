using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlyerAnimation))]
public class FlyerCollider : MonoBehaviour
{
    // Other Component
    private FlyerAnimation flyerAnimation;
    private FlyerStateInfo flyerStateInfo;

    void Awake()
    {
        flyerAnimation = GetComponent<FlyerAnimation>();
        flyerStateInfo = GetComponent<FlyerStateInfo>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool isInProgressRateZone = false;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Feather_Basic":
                Destroy(other.gameObject);
                break;

            case "Feather_Full":
                Destroy(other.gameObject);
                break;

            case "ProgressRateZone_StartOrEnd":
                if( isInProgressRateZone == false )
                {
                    ProgressRateMgr.getInstance.UpdateCurLap(other);
                    ProgressRateMgr.getInstance.UpdateCurProgressRateZoneNum(other.gameObject);
                }
                isInProgressRateZone = true;
                break;

            case "ProgressRateZone":
                if (isInProgressRateZone == false)
                {
                    ProgressRateMgr.getInstance.UpdateCurProgressRateZoneNum(other.gameObject);
                }
                isInProgressRateZone = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "ProgressRateZone_StartOrEnd":
            case "ProgressRateZone":
                isInProgressRateZone = false;
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.collider.tag)
        {
            case "Floor":
                if(flyerStateInfo.GetCurState() == FlyerStateInfo.StateID.UNCONTROLLABLE)
                    flyerAnimation.SetCurAnimState(FlyerAnimation.AnimStateID.DEAD);
                break;
        }
    }
}
