//using UnityEngine;
//using System.Collections;

//using System;
//using System.IO;
//using System.Linq;
//using System.Collections.Generic;
//using System.Runtime.Serialization.Formatters.Binary;


//static public class Data
//{
//    public const int MAX_FEATHER_NUM = 3;
//    public const int MAX_CHAPTHER_NUM = 3;


//    static public class Chapter
//    {
//        static public String[] ChapterName = { "개시의 폭포", "진리연구회 동굴", "격발 용암지대" };
//        //static public int[] MaxStageNum = { 15, 20, 25 };
//        static public int[] MaxStageNum = { 15, 20, 14 };
//        static public int[] MaxStageFeatherNum = { 45, 60, 75 };
//    }
//    static public class Sound
//    {
//        private const int MAX_BGM_CHAPTER_NUM = 5;

//        static public AudioClip BGM_Title = null;
//        static public AudioClip BGM_Opening = null;
//        static public AudioClip[] BGM_Chapter = null;
//        // (수정) SFX

//        static public void Init()
//        {
//            BGM_Title = (AudioClip)Resources.Load("Sound/BGM/BGM_Title");
//            BGM_Opening = (AudioClip)Resources.Load("Sound/BGM/BGM_Opening");

//            BGM_Chapter = new AudioClip[MAX_BGM_CHAPTER_NUM];
            
//            for( int i = 0; i < MAX_CHAPTHER_NUM; ++i)
//                BGM_Chapter[i]
//                    = (AudioClip)Resources.Load("Sound/BGM/BGM_Chapter_" + (i + 1).ToString());
//        }
//    }
//    static public class User
//    {
//        static public int currentSelectChapterNum = 1;
//        static public int currentSelectStageNum = 1;

//        static public bool bFirstStart = true;
//        static public bool bBGMOn = true;
//        static public bool bSFXOn = true;

//        static public int[] stageClearNum = new int[MAX_CHAPTHER_NUM];
//        static public int[] chapterGetFeatherNum = new int[MAX_CHAPTHER_NUM];
//        static public List<int[]> getFeatherNum = new List<int[]>();
//        static public List<bool[]> bClear = new List<bool[]>();

//        static public void InitUserData()
//        {
//            currentSelectChapterNum = 1;
//            currentSelectStageNum = 1;

//            User.bFirstStart = true;
//            User.bBGMOn = true;
//            User.bSFXOn = true;

//            Array.Clear(stageClearNum, 0, stageClearNum.Length);
//            Array.Clear(chapterGetFeatherNum, 0, chapterGetFeatherNum.Length);


//            int[] getFeatherNumTmp;
//            bool[] bClearTmp;

//            for (int i = 0; i < MAX_CHAPTHER_NUM; ++i)
//            {
//                getFeatherNumTmp = new int[Chapter.MaxStageNum[i]];
//                bClearTmp = new bool[Chapter.MaxStageNum[i]];
//                Array.Clear(getFeatherNumTmp, 0, getFeatherNumTmp.Length);
//                // bClearTmp = 자동으로 false;

//                getFeatherNum.Add(getFeatherNumTmp);
//                bClear.Add(bClearTmp);
//            }
//        }
//        static public void SaveData()
//        {
//            ES2.Save(currentSelectChapterNum, "ISCN");
//            ES2.Save(currentSelectStageNum, "ISSN");

//            ES2.Save(bFirstStart, "BBFS");
//            ES2.Save(bBGMOn, "BBBO");
//            ES2.Save(bSFXOn, "BSBO");

//            ES2.Save(stageClearNum, "Data.txt?tag=IASCN");
//            ES2.Save(chapterGetFeatherNum, "Data.txt?tag=IACGFN");


//            int[] getFeatherNumTmp;
//            bool[] bClearTmp;

//            for (int i = 0; i < MAX_CHAPTHER_NUM; ++i)
//            {
//                getFeatherNumTmp = new int[Chapter.MaxStageNum[i]];
//                bClearTmp = new bool[Chapter.MaxStageNum[i]];

//                for (int j = 0; j < Chapter.MaxStageNum[i]; ++j)
//                {
//                    getFeatherNumTmp[j] = getFeatherNum[i][j];
//                    bClearTmp[j] = bClear[i][j];
//                }

//                ES2.Save(getFeatherNumTmp, "Data.txt?tag=IACGFN_" + (i+1).ToString());
//                ES2.Save(bClearTmp, "Data.txt?tag=BABC_" + (i + 1).ToString());
//            }
//        }
//        static public void LoadData()
//        {
//            Sound.Init();

//            if (ES2.Exists("BSBO"))
//            {
//                currentSelectChapterNum = ES2.Load<int>("ISCN");
//                currentSelectStageNum = ES2.Load<int>("ISSN");

//                bFirstStart = ES2.Load<bool>("BBFS");
//                bBGMOn = ES2.Load<bool>("BBBO");
//                bSFXOn = ES2.Load<bool>("BSBO");

//                stageClearNum = ES2.LoadArray<int>("Data.txt?tag=IASCN");
//                chapterGetFeatherNum = ES2.LoadArray<int>("Data.txt?tag=IACGFN");


//                int[] getFeatherNumTmp;
//                bool[] bClearTmp;
//                getFeatherNum.Clear();
//                bClear.Clear();

//                for (int i = 0; i < MAX_CHAPTHER_NUM; ++i)
//                {
//                    getFeatherNumTmp = new int[Chapter.MaxStageNum[i]];
//                    bClearTmp = new bool[Chapter.MaxStageNum[i]];
//                    getFeatherNumTmp = ES2.LoadArray<int>("Data.txt?tag=IACGFN_" + (i + 1).ToString());
//                    bClearTmp = ES2.LoadArray<bool>("Data.txt?tag=BABC_" + (i + 1).ToString());

//                    getFeatherNum.Add(getFeatherNumTmp);
//                    bClear.Add(bClearTmp);
//                }
//            }
//            else InitUserData();
//        }
//        static public void UpdateDataForClear(int getFeatherNum)
//        {
//            if (Data.User.getFeatherNum[Data.User.currentSelectChapterNum - 1]
//                        [Data.User.currentSelectStageNum - 1] < getFeatherNum)
//            {
//                Data.User.chapterGetFeatherNum[Data.User.currentSelectChapterNum - 1]
//                    += getFeatherNum
//                    - Data.User.getFeatherNum[Data.User.currentSelectChapterNum - 1]
//                    [Data.User.currentSelectStageNum - 1];

//                Data.User.getFeatherNum[Data.User.currentSelectChapterNum - 1]
//                    [Data.User.currentSelectStageNum - 1] = getFeatherNum;
//            }
//            if (!(Data.User.bClear[Data.User.currentSelectChapterNum - 1]
//                    [Data.User.currentSelectStageNum - 1]))
//            {
//                Data.User.bClear[Data.User.currentSelectChapterNum - 1]
//                    [Data.User.currentSelectStageNum - 1] = true;
                
//                if( Data.User.stageClearNum[Data.User.currentSelectChapterNum - 1]
//                    < Data.Chapter.MaxStageNum[Data.User.currentSelectChapterNum - 1] )
//                    ++Data.User.stageClearNum[Data.User.currentSelectChapterNum - 1];
//            }
//        }
//    }
//}