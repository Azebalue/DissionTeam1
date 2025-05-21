using UnityEngine;

public class Define 
{
    public enum Scene
    {
        Unknown,
        Splah,
        Game,
    }

   public enum UIEvent
    {
        Click,
        Enter,
        Exit,

    }

    public enum stat
    {
        yehak,
        akhak,
        sahak,
        uhak,
        seohak,
        suhak,
        ihak,
        sulhak,
        umyanghak,
        uihak,
        yulhak,
        nongjeong,
        yeoksahak,
    }

    public enum personality
    {
        mercy,
        faith,
        courage,
        openness,
        integrity
    }
    public enum npc
    {

    }

    public enum palaceLocation
    {
        Gangnyeongjeon = 1,   // 강녕전
        Pyeonjeon,    // 편전 (김종서)
        Seungjeongwon, // 승정원 (황보인)
        Jiphyeonjeon, // 집현전 (성삼문)
        Seogo,        // 서고 (신숙주)
        Gyeongyeoncheong, // 경연청 (정인지)
        Naemyeongbu,  // 내명부 (혜빈 양씨)
        Huwon         // 후원 (경혜공주)
    }


    public const int TitlePopupID = 1001; 

    #region 짜잘 팝업ID 모음
    /* StartOrLoadPopup
     * WarningPopup
     */
    public const int YesText = 2001; 
    public const int NoText = 2002; 

    public const int WarningTitleText = 2005; 
    public const int WarningButtonText = 2006; 

    public const int StartOrLoadHeaderText = 2008; 
    public const int StartOrLoadBodyText = 2009;

    public const int StatHeaderText = 2010;

    #endregion

    #region 3.Home
    public const int YearText = 4001;
    public const int MonthText = 4002;
    public const int Stress = 4003;
    public const int Stamina = 4004;
    public const int Loyalty = 4005;
    public const int Majesty = 4006;

    public const int HomeBaseID = 5001;
    public const int SchedulesPopupID = 5101;
    public const int Subject = 5106;


    public const int subjectID= 7001;
    public const int subjectDescriptionID = 7101;



    #endregion
    public const int nameID = 100;
    public const int positionID = 200;

    public const int mapID = 8001;
    public const int InnerTxt = 8101;
    public const int OuterTxt = 8102;

    public const int MAX_ENDING_COUNT = 10;


}
