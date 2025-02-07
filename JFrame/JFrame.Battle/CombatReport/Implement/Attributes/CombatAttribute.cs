
using System;

namespace JFrame
{

    /// <summary>
    /// 屬性抽象類
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CombatAttribute<T> : IUnique where T : struct
    {
        /// <summary>
        /// 
        /// </summary>
        public T OriginValue { get; private set; }

        public T CurValue { get; protected set; }

        public T MaxValue { get; protected set; }

        public string Uid { get; private set; }

        public CombatAttribute(string uid, T value, T maxValue)
        {
            Uid = uid;
            OriginValue = value;
            CurValue = value;
            MaxValue = maxValue;
        }

        public abstract T Plus(T value);
        public abstract T Minus(T value);
        /// <summary>
        /// 乘法，直接乘的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract T Multi(T value);
        public abstract T Div(T value);
        public abstract T PlusMax(T value);
        public abstract T MinusMax(T value);
        public abstract T MultiMax(T value);
        public abstract T DivMax(T value);

        public void Reset() => CurValue = OriginValue;

        public abstract bool IsMax();
    }

    public class CombatAttributeInt : CombatAttribute<int>
    {
        public CombatAttributeInt(string uid, int value, int maxValue) : base(uid, value, maxValue)
        {
        }

        public override int Plus(int value)
        {
            CurValue += value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override int PlusMax(int value)
        {
            MaxValue += value;
            return MaxValue;
        }

        public override int Minus(int value)
        {
            CurValue -= value;
            CurValue = Math.Max(CurValue, 0);
            return CurValue;
        }

        public override int MinusMax(int value)
        {
            MaxValue -= value;
            MaxValue = Math.Max(MaxValue, 0);
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override int Multi(int value)
        {
            CurValue *= value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override int MultiMax(int value)
        {
            MaxValue *= value;
            return MaxValue;
        }

        public override int Div(int value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            CurValue = CurValue / value;
            return CurValue;
        }

        public override int DivMax(int value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            MaxValue = MaxValue / value;
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override bool IsMax()
        {
            return CurValue == MaxValue;
        }
    }


    public class CombatAttributeDouble : CombatAttribute<double>
    {
        public CombatAttributeDouble(string uid, double value, double maxValue) : base(uid, value, maxValue)
        {
        }

        public override double Plus(double value)
        {
            CurValue += value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override double PlusMax(double value)
        {
            MaxValue += value;
            return MaxValue;
        }

        public override double Minus(double value)
        {
            CurValue -= value;
            CurValue = Math.Max(CurValue, 0);
            return CurValue;
        }

        public override double MinusMax(double value)
        {
            MaxValue -= value;
            MaxValue = Math.Max(MaxValue, 0);
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override double Multi(double value)
        {
            CurValue *= value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override double MultiMax(double value)
        {
            MaxValue *= value;
            return MaxValue;
        }

        public override double Div(double value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            CurValue = CurValue / value;
            return CurValue;
        }

        public override double DivMax(double value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            MaxValue = MaxValue / value;
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override bool IsMax()
        {
            return CurValue == MaxValue;
        }
    }

    public class CombatAttributeLong : CombatAttribute<long>
    {
        public CombatAttributeLong(string uid, long value, long maxValue) : base(uid, value, maxValue)
        {
        }

        public override long Plus(long value)
        {
            CurValue += value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override long PlusMax(long value)
        {
            MaxValue += value;
            return MaxValue;
        }

        public override long Minus(long value)
        {
            CurValue -= value;
            CurValue = Math.Max(CurValue, 0);
            return CurValue;
        }

        public override long MinusMax(long value)
        {
            MaxValue -= value;
            MaxValue = Math.Max(MaxValue, 0);
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override long Multi(long value)
        {
            CurValue *= value;
            CurValue = Math.Min(CurValue, MaxValue);
            return CurValue;
        }

        public override long MultiMax(long value)
        {
            MaxValue *= value;
            return MaxValue;
        }

        public override long Div(long value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            CurValue = CurValue / value;
            return CurValue;
        }

        public override long DivMax(long value)
        {
            if (value == 0)
                throw new ArgumentException("除數不能為0");

            MaxValue = MaxValue / value;
            CurValue = Math.Min(CurValue, MaxValue);
            return MaxValue;
        }

        public override bool IsMax()
        {
            return CurValue == MaxValue;
        }
    }

}