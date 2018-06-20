using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoMgr : MonoBehaviour {

    private static MapInfoMgr _instance;
    public static MapInfoMgr getInstance
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

                _instance = inMapMgrs.GetComponent<MapInfoMgr>();

                if (_instance == null)
                    _instance = inMapMgrs.AddComponent<MapInfoMgr>();
            }

            return _instance;
        }
    }

    public enum MapType
    {
        MAP_TYPE_NONE,

        MAP_TYPE_CIRCLE,
        MAP_TYPE_LINE
    }

    public MapType mapType { get; private set; }
    public int totalLap { get; private set; }

    void Awake()
    {
        mapType     = MapType.MAP_TYPE_NONE;
        totalLap    = 0;
    }

    public void Init()
    {
        // (test)
        mapType = MapType.MAP_TYPE_CIRCLE;
        totalLap = 1;

        // 플레이어 위치
        // 맵 셋팅

        InMapUIMgr.getInstance.SetCurLap(0);
        InMapUIMgr.getInstance.SetTotalLap(totalLap);
    }
}