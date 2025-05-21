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




    public const int TitlePopupID = 1001; 

    /*#region 짜잘 팝업ID 모음
    *//* StartOrLoadPopup
     * WarningPopup
     *//*
    public const int YesText = 2001; 
    public const int NoText = 2002; 

    public const int WarningTitleText = 2005; 
    public const int WarningButtonText = 2006; 

    public const int StartOrLoadHeaderText = 2008; 
    public const int StartOrLoadBodyText = 2009;

    public const int StatHeaderText = 2010;

    #endregion*/


    public const int MAX_ENDING_COUNT = 10;


}
