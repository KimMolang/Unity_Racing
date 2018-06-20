using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum ScreenID
    {
        NONE,

        LOBBY,
        IN_MAP,
    }
    public ScreenID currentScreenID { get; private set; }

    public enum ScreenState
    {
        NONE,

        START,
        WORK,
        END
    }
    public ScreenState currentScreenState { get; private set; }

    private void SetCurrentScreenIDAndState(ScreenID _screenID, ScreenState _screenState)
    { currentScreenID = _screenID; currentScreenState = _screenState; }

    AudioSource audioSource = null;

    private static GameManager _instance;
    public static GameManager getInstance
    {
        get
        {
            if (null == _instance)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));

                if (null == _instance)
                {
                    string strObjectName = typeof(GameManager).ToString();
                    GameObject goObject = GameObject.Find(strObjectName);

                    if (null == goObject)
                    {
                        goObject = new GameObject();
                        goObject.name = strObjectName;
                    }

                    _instance = goObject.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }


    /**		
	@fn 	Awake(), Update() (OutStage Scene Frame work)
	@author 김명지(bluevill04@gmail.com)
	@brief	OutStage 씬의 프레임 워크 관리 및 모든 UI를 통제한다.
	@var	currentScreenID     현재 화면 위치
	@var	currentScreenState  현재 화면 상태 (NONE, Start, Work, End)	
     *      Start   해당 화면에서 초기화 해야 하는 함수들을 주로 호출한다.
     *      Work    해당 화면에서 계속 업데이트 해줘야하는 함수들을 주로 호출한다.
     *      End     해당 화면에서 마지막으로 처리해야하는 함수들을 주로 호출한다.
	**/
    void Awake()
    {
        // (test)
        currentScreenID = ScreenID.IN_MAP;
        currentScreenState = ScreenState.START;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // (Modify) 로딩 처리해야함
        InMapUIMgr.getInstance.Init();
        MapInfoMgr.getInstance.Init();
        ProgressRateMgr.getInstance.Init();
    }

    //void Update()
    //{
    //    switch (currentScreenID)
    //    {
    //        #region ** Basic Framework **
    //        case ScreenID.NONE:
    //            switch (currentScreenState)
    //            {
    //                case ScreenState.NONE: break;
    //                case ScreenState.START: currentScreenState = ScreenState.WORK; break;
    //                case ScreenState.WORK: break;
    //                case ScreenState.END:
    //                    SetCurrentScreenIDAndState(
    //                        GameManager.ScreenID.NONE,
    //                        GameManager.ScreenState.NONE);
    //                    break;
    //            }
    //            break;
    //        #endregion
    //        #region ScreenID.IN_MAP
    //        case ScreenID.IN_MAP:
    //            switch (currentScreenState)
    //            {
    //                case ScreenState.NONE: break;

    //                case ScreenState.START:
    //                    //MapInfoMgr.getInstance.Init();
    //                    //ProgressRateMgr.getInstance.Init();
    //                    currentScreenState = ScreenState.WORK;
    //                    break;

    //                case ScreenState.WORK:
    //                    break;

    //                case ScreenState.END:
    //                    break;
    //            }
    //            break;
    //        #endregion
    //    }
    //}

    //public void SetBGM(ScreenID screenId, int chapterNum)
    //{
    //    audioSource.Stop();

    //    switch (screenId)
    //    {
    //        case ScreenID.Title:
    //        case ScreenID.Chapter:
    //        case ScreenID.Stage:
    //        default :
    //            audioSource.loop = true;
    //            audioSource.clip = Data.Sound.BGM_Title; break;

    //        case ScreenID.Story:
    //            audioSource.loop = false;
    //            audioSource.clip = Data.Sound.BGM_Opening; break;

    //        case ScreenID.InStage :
    //            audioSource.loop = true;
    //            audioSource.clip = Data.Sound.BGM_Chapter[chapterNum - 1];break;
    //    }

    //    audioSource.Play();
    //}
    //public void SetBGMOnOff(bool bOn)
    //{
    //    audioSource.volume = (bOn) ? 1f : 0f;
    //}
}
