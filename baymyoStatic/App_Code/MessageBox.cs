using System;
using System.Collections.Generic;
using System.Web;

public enum DialogResult
{
    Info,
    Help,
    Stop,
    Succes,
    Warning,
    Error
}

public static class MessageBox
{
    static string m_Info = "<div class=\"info-box\"><img src=\"" + Settings.IconsPath + "32/dialog-info.png\" />{0}</div>";
    static string m_Help = "<div class=\"message-box\"><img src=\"" + Settings.IconsPath + "32/dialog-help.png\" />{0}</div>";
    static string m_Stop = "<div class=\"error-box\"><img src=\"" + Settings.IconsPath + "32/dialog-stop.png\" />{0}</div>";
    static string m_Success = "<div class=\"success-box\"><img src=\"" + Settings.IconsPath + "32/dialog-succes.png\" />{0}</div>";
    static string m_Warning = "<div class=\"message-box\"><img src=\"" + Settings.IconsPath + "32/dialog-warning.png\" />{0}</div>";
    static string m_Error = "<div class=\"error-box\"><img src=\"" + Settings.IconsPath + "32/dialog-error.png\" />{0}</div>";

    public static string Show(DialogResult dialogResult, string message)
    {
        switch (dialogResult)
        {
            case DialogResult.Info:
                return string.Format(m_Info, message);
            case DialogResult.Help:
                return string.Format(m_Help, message);
            case DialogResult.Stop:
                return string.Format(m_Stop, message);
            case DialogResult.Succes:
                return string.Format(m_Success, message);
            case DialogResult.Warning:
                return string.Format(m_Warning, message);
            case DialogResult.Error:
                return string.Format(m_Error, message);
            default:
                return string.Empty;
        }
    }

    public static string IsNotViews()
    {
        return "<img class=\"left\" src=\"" + Settings.IconsPath + "32/dialog-warning.png\" /><span class=\"left\" style=\"margin-left:10px;margin-top:10px;\">Gösterilecek kayıt bulunamadı.</span>";
    }

    public static string AccessDenied()
    {
        return string.Format(m_Warning, "Sisteme giriş yaptığınız için yada gösterim yetkiniz bulunmadığı için bu sayfaya erişim sağlayamıyorsunuz!");
    }

    public static string UnSuccessful()
    {
        return string.Format(m_Stop, "İşleminiz gerçekleştirilemiyor, bunun sebebleri aşağıdakilerden biri olabilir!<br/>- Lütfen sayfaya erişim yolunuzun doğru olduğundan emin olunuz.<br/>- Sunucu yoğunluğundan dolayı bu işlemi gerçekleştiremiyor olabilirsiniz.<br/>- Bilgilerinizin doğru olduğundan emin olunuz.<br/>- Bu işlemi daha önce gerçekleştirmiş olabilirsiniz.<br/>- Bu işlemi gerçekleştirmeye yetkiniz olduğundan emin olunuz.");
    }
}