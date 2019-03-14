using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILTypeKeeper
{
    //-------------------------------------------------------------------------
    public Dictionary<short, ILRuntime.Runtime.Intepreter.ILTypeInstance> M1 { get; set; }
    public HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance> H1 { get; set; }
    public List<ILRuntime.Runtime.Intepreter.ILTypeInstance> L1 { get; set; }
}
