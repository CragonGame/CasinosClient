using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILTypeKeeper
{
    //-------------------------------------------------------------------------
    public Dictionary<short, ILRuntime.Runtime.Intepreter.ILTypeInstance> M1 = new Dictionary<short, ILRuntime.Runtime.Intepreter.ILTypeInstance>();
    public Dictionary<int, ILRuntime.Runtime.Intepreter.ILTypeInstance> M2 = new Dictionary<int, ILRuntime.Runtime.Intepreter.ILTypeInstance>();
    public Dictionary<string, ILRuntime.Runtime.Intepreter.ILTypeInstance> M3 = new Dictionary<string, ILRuntime.Runtime.Intepreter.ILTypeInstance>();
    public HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance> H1 = new HashSet<ILRuntime.Runtime.Intepreter.ILTypeInstance>();
    public List<ILRuntime.Runtime.Intepreter.ILTypeInstance> L1 = new List<ILRuntime.Runtime.Intepreter.ILTypeInstance>();
}
