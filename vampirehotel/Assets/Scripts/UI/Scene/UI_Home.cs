using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Home : UI_Scene
{
    #region 열거자 목록
    enum Buttons
    {
        ScheduleBtn,
        StatsBtn,
        TraitsBtn,
        FactionsBtn,
        LineageBtn,
        HomeBtn,

    }

    enum Texts
    {
        YearText,
        MonthText,

        ScheduleText,
        StatsText,
        TraitsText,
        FactionsText,
        LineageText,
        HomeText,
    }

    public enum Images
    {
        HomeImage,
        PrinceImage,
    }

    public enum Popups
    {
        Prince,
        Schedules,
        Stats,
        Traits,
        Factions,
        Lineage,

    }

    #endregion

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind();
        Gets();

        RefreshUI();

        return true;

    }

    public override void Bind()
    {
        //모든 패널의 요소 바인딩
        Bind<Button>(typeof(Buttons), true);
        Bind<TextMeshProUGUI>(typeof(Texts), true);
        Bind<Image>(typeof(Images), true);
        Bind<GameObject>(typeof(Popups), true);
    }

    public override void Gets()
    {
        GetImage((int)Images.HomeImage).sprite = Managers.Resource.LoadSprite("HomeImage");

        GetImage((int)Images.PrinceImage).sprite = GetCharacter("Prince");

        #region 0.  Base 팝업
        GetButton((int)Buttons.ScheduleBtn).gameObject.AddUIEvent(ScheduleButton);
        GetButton((int)Buttons.StatsBtn).gameObject.AddUIEvent(StatsButton);
        GetButton((int)Buttons.TraitsBtn).gameObject.AddUIEvent(TraitsButton);
        GetButton((int)Buttons.FactionsBtn).gameObject.AddUIEvent(FactionsButton);
        GetButton((int)Buttons.LineageBtn).gameObject.AddUIEvent(LineageButton);
        GetButton((int)Buttons.HomeBtn).gameObject.AddUIEvent(HomeButton);

        GetTexts((int)Texts.ScheduleText, (int)Texts.HomeText, Define.HomeBaseID);
        GetText((int)Texts.YearText).text = Managers.Game.year+Managers.GetText(Define.YearText);
        GetText((int)Texts.MonthText).text = Managers.Game.month+Managers.GetText(Define.MonthText);

        #endregion

    }

    //*****************************************************************************************************

    public void ScheduleButton(PointerEventData data)
    {
        ShowHomePopup(Popups.Schedules);
    }

    #region 0. 베이스 팝업 버튼
    public void StatsButton(PointerEventData data)
    {
        ShowHomePopup(Popups.Stats);
    }

    public void TraitsButton(PointerEventData data)
    {
        ShowHomePopup(Popups.Traits);
    }

    public void FactionsButton(PointerEventData data)
    {
        ShowHomePopup(Popups.Factions);
    }

    public void LineageButton(PointerEventData data)
    {
        Debug.Log("LineageBtn");
        ShowHomePopup(Popups.Lineage);


    }

    public void HomeButton(PointerEventData data)
    {
        ShowHomePopup(Popups.Prince);
    }

    #endregion


    Popups curPanel;
    void ShowHomePopup(Popups popupType)
    {
/*        //기존 패널 닫기
        if (curPanel == Popups.Schedules)
            UI_SchedulesPopup._refresh.Invoke();

        GetObject((int)curPanel).SetActive(false);

        // 새 패널 열기
        curPanel = popupType;
        GetObject((int)curPanel).SetActive(true); */
    }

    void RefreshUI()
    {
        //모든 팝업패널 비활성화
        foreach (Popups popup in System.Enum.GetValues(typeof(Popups)))
            GetObject((int)popup).gameObject.SetActive(false);

        //홈 팝업
        ShowHomePopup(Popups.Prince);
    }

/*    void RefreshSchedulesPopup()
    {
        studyIndex = 0;
        GetTexts((int)Texts.HeaderText, (int)Texts.subjectNameText, Define.SchedulesPopupID);
        GetText((int)Texts.subjectNameText).text = Managers.GetText(Define.Subject);
        GetText((int)Texts.subjectExplainText).text = "";
    }*/

}