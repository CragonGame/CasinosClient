public interface IThirdPartyLoginReceiverListener
{
    //-------------------------------------------------------------------------
    void loginSuccess(string token);
    void loginFail(string fail_type);
}

public enum _eLoginFaildType
{
    ERR_OK = 0,
    ERR_COMM = -1,
    ERR_USER_CANCEL = -2,
    ERR_SENT_FAILED = -3,
    ERR_AUTH_DENIED = -4,
    ERR_UNSUPPORT = -5,
    ERR_NOTINSTALLEDWECHAT = 10000,
}
