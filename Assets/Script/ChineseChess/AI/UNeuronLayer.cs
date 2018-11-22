using UnityEngine;
using System.Collections;

/// <summary>
/// 神经细胞层
/// </summary>
public class UNeuronLayer
{
    public UNeuron[] Neurons;

    public UNeuronLayer(int NumNeurons, int NumInputsPerNeuron)
    {
        Neurons = new UNeuron[NumNeurons];
        for (int i = 0; i < Neurons.Length; i++)
        {
            Neurons[i] = new UNeuron(NumInputsPerNeuron);
        }
    }
}
