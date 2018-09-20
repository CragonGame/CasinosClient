using FairyGUI;
using FairyGUI.Utils;
using Casinos;

public class ChatParser : UBBParser
{
    //-------------------------------------------------------------------------
    static ChatParser mChatParser;

    //-------------------------------------------------------------------------
    public new static ChatParser inst
    {
        get
        {
            if (mChatParser == null)
            {
                mChatParser = new ChatParser();
            }
            return mChatParser;
        }
    }

    //-------------------------------------------------------------------------
    private static string[] Tags = new string[]
        { "1","2","3","4","5","6","7","8","9","10",
         "11","12","13","14","15","16","17","18","19","20",
            "pot0" ,"pot1","pot2","pot3"};

    //-------------------------------------------------------------------------
    public ChatParser()
    {
        foreach (string i in Tags)
        {
            this.handlers[i] = OnHandleChat;
        }
    }

    //-------------------------------------------------------------------------
    string OnHandleChat(string tagName, bool end, string attr)
    {
        string resource_name = CasinosContext.Instance.AppendStrWithSB(tagName, "Small");
        var s = "<img src='" + UIPackage.GetItemURL("Common", resource_name) + "'/>";
        return s;
    }
}