using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InMapUIMgr : MonoBehaviour
{
    private static InMapUIMgr _instance;
    public static InMapUIMgr getInstance
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

                _instance = inMapMgrs.GetComponent<InMapUIMgr>();

                if (_instance == null)
                    _instance = inMapMgrs.AddComponent<InMapUIMgr>();
            }

            return _instance;
        }
    }

    private enum UIText
    {
        TEXT_CUR_LAP,
        TEXT_TOTAL_LAP,

        TEXT_TIME,

        TEXT_NUM
    }

    //private enum UIImage
    //{
    //    IMAGE_PROGRESSRATE_RATE,

    //    IMAGE_NUM
    //}

    private enum UIRect
    {
        RECT_PROGRESSRATE_RATE,

        RECT_NUM
    }

    private Text[] texts;
    //private Image[] images;
    private RectTransform[] rects;


    public void Init()
    {
        // texts
        texts = new Text[(int)UIText.TEXT_NUM];

        texts[(int)UIText.TEXT_CUR_LAP]
            = GameObject.Find("Text_CurLap").GetComponent<Text>();
        texts[(int)UIText.TEXT_TOTAL_LAP]
            = GameObject.Find("Text_TotalLap").GetComponent<Text>();
        texts[(int)UIText.TEXT_TIME]
            = GameObject.Find("Text_CurTime").GetComponent<Text>();

        //// images
        //images = new Image[(int)UIImage.IMAGE_NUM];

        //images[(int)UIImage.IMAGE_PROGRESSRATE_RATE]
        //    = GameObject.Find("Image_ProgressRate_Rate").GetComponent<Image>();

        // rets
        rects = new RectTransform[(int)UIRect.RECT_NUM];

        rects[(int)UIRect.RECT_PROGRESSRATE_RATE]
            = GameObject.Find("Image_ProgressRate_Rate").GetComponent<RectTransform>();

        ORIGINE_RECT_PROGRESSRATE_RATE_SIZE_Y
            = rects[(int)UIRect.RECT_PROGRESSRATE_RATE].sizeDelta.y;
    }

    public void SetTotalLap(int _iTotalLap)
    {
        texts[(int)UIText.TEXT_TOTAL_LAP].text = "/" + _iTotalLap;
    }

    public void SetCurLap(int _iCurLap)
    {
        texts[(int)UIText.TEXT_CUR_LAP].text = _iCurLap.ToString();
    }

    private float ORIGINE_RECT_PROGRESSRATE_RATE_SIZE_Y;
    public void SetProgressRateBar(float _fRate)
    {
        rects[(int)UIRect.RECT_PROGRESSRATE_RATE].sizeDelta
            = new Vector2(rects[(int)UIRect.RECT_PROGRESSRATE_RATE].sizeDelta.x
            , ORIGINE_RECT_PROGRESSRATE_RATE_SIZE_Y * _fRate);
    }

    public void SetTime(int _iMinute, float _fSecond)
    {
        if(_fSecond > 10.0f)
        {
            texts[(int)UIText.TEXT_TIME].text
                = _iMinute.ToString("D2")
                + " : "
                + _fSecond.ToString("F2");
        }
        else
        {
            texts[(int)UIText.TEXT_TIME].text
                = _iMinute.ToString("D2")
                + " : 0"
                + _fSecond.ToString("F2");
        }
        
    }
}