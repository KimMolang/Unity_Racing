using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressRateZone : MonoBehaviour
{
    //public int index { get; private set; }
    [SerializeField] private int index;
    public int GetProgressRateZoneIndex() { return index; }

    void Awake()
    {
        ProgressRateMgr.getInstance.AddProgressRateZone(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
