
public class UI_Popup : UI_Base
{
    public override bool Init()
    {
        if(!base.Init())
            return false;

        Managers.UI.SetCanvas(gameObject, true);
        return true;
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
