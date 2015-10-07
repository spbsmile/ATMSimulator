using System;

namespace ATM
{
    public class FaceValue : IComparable<FaceValue>
    {
        /// <summary>
        /// Amount in Sequence
        /// </summary>
        public int Amount;

        public int Value
        {
            get { return value; }
        }

        private readonly int value;

        public FaceValue(int value)
        {
            this.value = value;
        }
       
        public int CompareTo(FaceValue other)
        {
            if (Value == other.Value) return 0;
            if (Value > other.Value) return 1;
            return -1;
        }
    }
}
