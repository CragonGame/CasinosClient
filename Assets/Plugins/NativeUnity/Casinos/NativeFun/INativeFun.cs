using UnityEngine;
using System.Collections;

public interface INativeFun
{
    //-------------------------------------------------------------------------    
    string getCountryCode();
    //-------------------------------------------------------------------------
    void installAPK(string file_path);
}
