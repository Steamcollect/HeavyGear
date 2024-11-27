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
        "Oc",
        "No",
        "De",
        "Ud",
        "Dd",
        "Tdd",
        "Qdd",
        "Qnd",
        "Sxd",
        "Spd",
        "Ocd",
        "Nvd",
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

    #region Constructors
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
        float value = m;
        if (n % 3 == 0) digit = indexToMagnitude[n / 3];
        else if (n % 3 == 1)
        {
            Debug.Log("dqd");
            value *= 10;
            digit = n / 3 - 1 >= 0 ? indexToMagnitude[n / 3] : indexToMagnitude[0];
        }
        else if (n % 3 == 2)
        {
            value *= 100;
            digit = n / 3 - 1 >= 0 ? indexToMagnitude[n / 3] : indexToMagnitude[0];
        }
        return $"{value:F2}{digit}";
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

    #region Basic Arithmetic
    public BigNumber Multiply(BigNumber other)
    {
        this.m *= other.m;
        this.n += other.n;
        this.Arrange();
        return this;
    }
    #endregion
}