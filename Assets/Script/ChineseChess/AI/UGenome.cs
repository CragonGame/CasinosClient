using UnityEngine;
using System.Collections;

/// <summary>
/// 基因编码
/// </summary>
[System.Serializable]
public class UGenome : System.IComparable
{
    //基因编码
    public double[] Weights;
    //适应性
    public double Fidness;

    public UGenome(int NumWeights)
    {
        Weights = new double[NumWeights];
        //初始化权重值
        for (int i = 0; i < Weights.Length; i++)
        {
            Weights[i] = Random.Range(-1.0f, 1.0f);
        }
    }

    //排序操作（适应性分数高的在前）
    public int CompareTo(object obj)
    {
        UGenome Genome = (UGenome)obj;
        if (this.Fidness < Genome.Fidness)
            return 1;
        else if (this.Fidness == Genome.Fidness)
            return 0;
        else
            return -1;
    }
}
