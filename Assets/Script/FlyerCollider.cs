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
                ProgressRateMgr.getInstance.UpdateCurLap(other);
                ProgressRateMgr.getInstance.UpdateCurProgressRateZoneNum(other.gameObject);
                break;

            case "ProgressRateZone":
                ProgressRateMgr.getInstance.UpdateCurProgressRateZoneNum(other.gameObject);
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
