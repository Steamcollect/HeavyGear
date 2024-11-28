using BigFloatNumerics;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class BigNumber
{
    public float m { get; private set; }
    public int n { get; private set; }

    const float CompTolerance = 1e-1f;
    const int CompTolerancei = 1;

    private List<string> indexToMagnitude = new()
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

    public BigNumber(BigNumber value)
    {
        this.m = value.m;
        this.n = value.n;
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
    public BigNumber Add(BigNumber other)
    {
        if(n > other.n)
        {
            if(this.n - other.n > 20) return this;
            m += other.m / Mathf.Pow(10, (float)(this.n - other.n));
            this.Arrange();
        }
        else
        {
            if(other.n - this.n > 20) return other;
            m *= Mathf.Pow(10, (float)(this.n - other.n));
            m += other.n;
            this.n = other.n;
            this.Arrange();
        }

        return this;
    }

    public BigNumber Subtract(BigNumber other)
    {
        return Add(other.Negate());
    }

    public BigNumber Multiply(BigNumber other)
    {
        this.m *= other.m;
        this.n += other.n;
        this.Arrange();
        return this;
    }

    public BigNumber Negate()
    {
        m = -m;
        return this;
    }

    public int CompareTo(BigNumber other)
    {
        var diff = this - other;
        if (diff.n == 0 && diff.m == 0)
            return 0;
        
        else return diff.m.CompareTo(0);
    }

    public static int Compare(BigNumber left, BigNumber right)
    {
        return (new BigNumber(left)).CompareTo(right);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion

    #region Operators
    public static BigNumber operator -(BigNumber value)
    {
        return (new BigNumber(value)).Negate();
    }
    public static BigNumber operator -(BigNumber left, BigNumber right)
    {
        return (new BigNumber(left)).Subtract(right);
    }
    public static bool operator !=(BigNumber left, BigNumber right)
    {
        return Compare(left, right) != 0;
    }
    public static bool operator ==(BigNumber left, BigNumber right)
    {
        return Compare(left, right) == 0;
    }
    public static bool operator <(BigNumber left, BigNumber right)
    {
        return Compare(left, right) < 0;
    }
    public static bool operator <=(BigNumber left, BigNumber right)
    {
        return Compare(left, right) <= 0;
    }
    public static bool operator >(BigNumber left, BigNumber right)
    {
        return Compare(left, right) > 0;
    }
    public static bool operator >=(BigNumber left, BigNumber right)
    {
        return Compare(left, right) >= 0;
    }
    #endregion
}