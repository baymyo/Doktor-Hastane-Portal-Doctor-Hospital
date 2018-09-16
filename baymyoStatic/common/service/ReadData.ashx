<%@ WebHandler Language="C#" Class="ReadData" %>

using System;
using System.Web;

public class ReadData : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;

            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(javaScriptSerializer.Serialize(Lib.SuggestMethods.GetList()));
        }
        catch (Exception)
        {
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}