using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 神经细胞
/// </summary>
public class UNeuron
{
    public double[] InputWeights;

    public UNeuron(int NumInputs)
    {
        //多一个权重值
        InputWeights = new double[NumInputs + 1];
    }
}
