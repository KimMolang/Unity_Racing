using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InMapTimer : MonoBehaviour
{
    private static InMapTimer _instance;
    public static InMapTimer getInstance
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

                _instance = inMapMgrs.GetComponent<InMapTimer>();

                if (_instance == null)
                    _instance = inMapMgrs.AddComponent<InMapTimer>();
            }

            return _instance;
        }
    }

    float gameTimer = 0.0f;
    int timeMinit = 0;

    private void FixedUpdate()
    {
        // 게임 진행 상태 일 경우
        UpdateTimer(Time.fixedDeltaTime);
    }

    public void UpdateTimer(float _fTime)
    {
        gameTimer += _fTime;

        if(gameTimer >= 60.0f)
        {
            ++timeMinit;
            gameTimer -= 60.0f;
        }

        float fForSecondPoint = gameTimer;

        InMapUIMgr.getInstance.SetTime(timeMinit, fForSecondPoint);
    }
}