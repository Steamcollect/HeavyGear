using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class BigNumber
{
    public float m { get; private set; }
    public int n { get; private set; }

    public List<string> indexToMagnitude = new()
    {
        "",
        "K",
        "M",
        "B",
        "T",
        "Qd",
        "Qn",
        "Sx",
        "Sp",
        "O",
        "N",
    };

    public BigNumber Arrange()
    {
        float absm = Mathf.Abs(m);

        if(absm < float.Epsilon)
        {
            n = 0;
            return this;
        }

        int log = (int)Mathf.Floor(Mathf.Log10(absm));

        n += log;
        m /= Mathf.Pow(10, log);

        return this;
    }

    #region constructors
    public BigNumber(string value)
    {
        BigNumber bf = Parse(value);
        this.m = bf.m;
        this.n = bf.n;
    }

    public BigNumber(float m, int n)
    {
        this.m = m;
        this.n = n;
        Arrange();
    }

    public BigNumber(double value) : this(value.ToString("e9"))
    {
    }
    #endregion

    #region ToString and Parse
    public override string ToString()
    {
        string digit = "";
        if (n % 3 == 0) digit = indexToMagnitude[n / 3];
        else if (n % 3 == 1)
        {
            m *= 10;
            digit = indexToMagnitude[n / 3 - 1];
        }
        else if (n % 3 == 2)
        {
            m *= 100;
            digit = indexToMagnitude[n / 3 - 1];
        }
        return $"{m:F2}{digit}";
    }

    public static BigNumber Parse(string value)
    {
        if(value == null)
            throw new ArgumentNullException("value");

        value.Trim();
        value = value.Replace(",", "");
        int pos = value.IndexOf('e');
        value = value.Replace("e", "");

        float Signifacand = float.Parse(value.Substring(0, pos));

        int denominator = int.Parse(value.Substring(pos));

        return new BigNumber(Signifacand, denominator).Arrange();
    }
    #endregion

    #region basic arithmetic
    public BigNumber Multiply(BigNumber other)
    {
        this.m *= other.m;
        this.n += other.n;
        this.Arrange();
        return this;
    }
    #endregion
}