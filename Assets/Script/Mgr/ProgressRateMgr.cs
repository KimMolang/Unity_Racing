using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressRateMgr : MonoBehaviour {

    private static ProgressRateMgr _instance;
    public static ProgressRateMgr getInstance
    {
        get
        {
            if (_instance == null)
            {
                GameObject inMapMgrs = GameObject.Find("InMapMgrs");

                if (inMapMgrs == null)
                {
                    inMapMgrs = new GameObject();
                    inMapMgrs.name = "InMapMgrs";
                }

                _instance = inMapMgrs.GetComponent<ProgressRateMgr>();

                if (_instance == null)
                    _instance = inMapMgrs.AddComponent<ProgressRateMgr>();
            }

            return _instance;
        }
    }

    // Bellow ArrayLists are about Only one lap
    private ArrayList progressRateZoneList = new ArrayList();
    private ArrayList lineBtweenProgreesRateZoneList = new ArrayList();
    private ArrayList cumulativeLengthList = new ArrayList();

    private GameObject player;
    private int curProgressRateZoneNum; // If player had passed the ProgressRateZone had index 5, it is 5.
    private int curLapNum;
    private float curProgressRate;

    void Awake()
    {
        //FlyerController controller
        //    = (FlyerController)FindObjectOfType(typeof(FlyerController)); // too hard..

        //if(controller)
        //{
        //    player = controller.gameObject;
        //}
        player = GameObject.Find("Player");
    }

    // Use this for initialization
    void Start ()
    {
        
    }

    public void Init()
    {
        InitProgressRateZoneInfo();
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        UpdateProgressRate();
    }

    public void AddProgressRateZone(ProgressRateZone _obProgressRateZone)
    {
        progressRateZoneList.Add(_obProgressRateZone);
    }

    private void InitProgressRateZoneInfo()
    {
        // When curent map's type is MapInfoMgr.MapType.MAP_TYPE_LINE,
        // lineBtweenProgreesRateZoneList.Count and cumulativeLengthList.Count
        // are one less then progressRateZoneList.Count


        if (progressRateZoneList.Count == 0)
            return;

        // Sort
        IComparer compare = new SortOfProgressIndex();
        progressRateZoneList.Sort(compare);

        // lineBtweenProgreesRateZoneList & cumulativeLengthList
        ProgressRateZone previousZone;
        ProgressRateZone NextZone;

        Vector3 vLine;
        float fCumulativeLength = 0.0f;

        for (int i = 0; i < progressRateZoneList.Count - 1; ++i)
        {
            previousZone = (ProgressRateZone)progressRateZoneList[i];
            NextZone = (ProgressRateZone)progressRateZoneList[i + 1];

            vLine = NextZone.transform.position - previousZone.transform.position;
            fCumulativeLength += vLine.sqrMagnitude;

            lineBtweenProgreesRateZoneList.Add(vLine);
            cumulativeLengthList.Add(fCumulativeLength);
        }

        switch (MapInfoMgr.getInstance.mapType)
        {
            case MapInfoMgr.MapType.MAP_TYPE_CIRCLE:
                {
                    previousZone = (ProgressRateZone)progressRateZoneList[progressRateZoneList.Count - 1];
                    NextZone = (ProgressRateZone)progressRateZoneList[0];

                    vLine = NextZone.transform.position - previousZone.transform.position;
                    fCumulativeLength += vLine.sqrMagnitude;

                    lineBtweenProgreesRateZoneList.Add(vLine);
                    cumulativeLengthList.Add(fCumulativeLength);
                }
                break;

            case MapInfoMgr.MapType.MAP_TYPE_LINE:
                break;
        }
    }

    public void UpdateCurLap(Collider _Collider)
    {
        if (_Collider.tag != "ProgressRateZone_StartOrEnd")
            return;


        int iTotalProgressZoneNum = progressRateZoneList.Count
            - ((MapInfoMgr.getInstance.mapType == MapInfoMgr.MapType.MAP_TYPE_LINE) ? 1 : 0);

        ProgressRateZone zoneCollided
            = _Collider.gameObject.GetComponent<ProgressRateZone>();

        // curLapNum
        if (( curProgressRateZoneNum
            == (iTotalProgressZoneNum * (curLapNum + 1)) - 1 ) )
            //&& ( zoneCollided.GetProgressRateZoneIndex() + (curLapNum * iTotalProgressZoneNum)
            //== curProgressRateZoneNum + 1 )) // Last Area In only one track
        {
            ++curLapNum;
        }
        else
            --curLapNum;

        if (curLapNum < 0)
            curLapNum = 0;


        InMapUIMgr.getInstance.SetCurLap(curLapNum);
        //Debug.Log("curLapNum : " + curLapNum + "/" + MapInfoMgr.getInstance.totalLap);
    }

    public void UpdateCurProgressRateZoneNum(GameObject _objCollided)
    {
        if (curLapNum == MapInfoMgr.getInstance.totalLap)
            return;


        int iTotalProgressZoneNum = progressRateZoneList.Count;

        ProgressRateZone zoneCollided
            = _objCollided.GetComponent<ProgressRateZone>();

        if (zoneCollided == null)
            return;

        if (zoneCollided.GetProgressRateZoneIndex() + (curLapNum * iTotalProgressZoneNum)
            == curProgressRateZoneNum + 1)
            ++curProgressRateZoneNum;
        else
            --curProgressRateZoneNum;

        Debug.Log("curProgressRateZoneNum : " + curProgressRateZoneNum);
    }

    private void UpdateProgressRate()
    {
        if (player == null)
            return;

        if (curProgressRateZoneNum < 0)
            return;

        if (progressRateZoneList.Count == 0
            || lineBtweenProgreesRateZoneList.Count == 0
            || cumulativeLengthList.Count == 0)
            return;

        if (curLapNum == MapInfoMgr.getInstance.totalLap)
            return;


        int iIndexOfList
            = curProgressRateZoneNum % progressRateZoneList.Count;

        ProgressRateZone previousZone
            = (ProgressRateZone)progressRateZoneList[iIndexOfList];

        Vector3 vPlayerPositionCurZone
            = player.transform.position- previousZone.transform.position;


        float fLengthInCurZone
            = Vector3.Dot(vPlayerPositionCurZone, (Vector3)lineBtweenProgreesRateZoneList[iIndexOfList]);

        float fTotalCumulativeLengthInOnlyOneTrack
            = (float)cumulativeLengthList[cumulativeLengthList.Count - 1];

        curProgressRate = ((float)cumulativeLengthList[iIndexOfList] + fLengthInCurZone
            + (fTotalCumulativeLengthInOnlyOneTrack * curLapNum))
            / fTotalCumulativeLengthInOnlyOneTrack * MapInfoMgr.getInstance.totalLap;

        //Debug.Log("curProgressRate : " + curProgressRate);
        InMapUIMgr.getInstance.SetProgressRateBar(curProgressRate);
    }
}

public class SortOfProgressIndex : IComparer
{
    int IComparer.Compare(object x, object y)
    {
        ProgressRateZone obX = (ProgressRateZone)x;
        ProgressRateZone obY = (ProgressRateZone)y;

        return obX.GetProgressRateZoneIndex().CompareTo(obY.GetProgressRateZoneIndex());
    }
}