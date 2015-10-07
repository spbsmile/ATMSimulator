using System.Collections.Generic;

namespace ATM
{
    public class Settings
    {
        public Settings(int cash, List<int> faceValueses)
        {
            Cash = cash;
            FaceValues = new List<FaceValue>();
            foreach (var value in faceValueses)
            {
                FaceValues.Add(new FaceValue(value));
            }
        }

        public List<FaceValue> FaceValues
        {
            get; private set;
        }

        public int Cash
        {
            get; private set;
        }
    }
}
