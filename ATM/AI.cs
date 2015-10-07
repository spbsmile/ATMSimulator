using System.Collections.Generic;
using System.Linq;

namespace ATM
{
    public class AI
    {
        private readonly Settings settings;

        public AI(Settings settings)
        {
            this.settings = settings;
        }

        public int DefineMaxCountFaceValues()
        {
            var sum = 0;
            for (var i = 0; i <= settings.FaceValues.Count - 1; i++)
            {
                if (sum + settings.FaceValues[i].Value < settings.Cash)
                {
                    sum += settings.FaceValues[i].Value;
                }
                else
                {
                    return i;
                }
            }
            return settings.FaceValues.Count;
        }

        class Entry
        {
            public int globalIndex;

            public Entry(int globalIndex)
            {
                this.globalIndex = globalIndex;
            }
        }

        /// <summary>
        ///  Algoritm find sequence face value of cashout. Face value - minimum. 
        /// </summary>
        /// <param name="countRequired"></param>
        public IEnumerable<FaceValue> FindSequenceFaceValues(int countRequired)
        {
            //prepare
            var entrys = new Entry[countRequired];
            for (var i = 0; i <= countRequired - 1; i++)
            {
                entrys[i] = new Entry(settings.FaceValues.Count + i - countRequired);
            }

            var simulatorValues = new Entry[settings.FaceValues.Count];
            foreach (var entry in entrys)
            {
                simulatorValues[entry.globalIndex] = entry;
            }
                
            // sorted
            var remainder = -1;
            var thresholdPassed = SumCashOf(entrys) <= settings.Cash;
            if (thresholdPassed) remainder = CreateSequence(entrys);
            while (!thresholdPassed && remainder != 0)
            {
                if (entrys[0].globalIndex != 0)
                {
                    ShiftLeft(entrys[0], simulatorValues);
                }
                else
                {
                    var globalIndex = 1;
                    // find first FaceValue can shift left
                    while (globalIndex <= countRequired - 1 && simulatorValues[globalIndex] != null)
                    {
                        globalIndex++;
                        //if (globalIndex == countRequired) break;
                    }
                    ShiftLeft(entrys[globalIndex], simulatorValues);
                    for (var i = globalIndex - 1; i >= 0; i--)
                    {
                        ShiftRight(entrys[i], simulatorValues, i);
                    }
                }
                thresholdPassed = SumCashOf(entrys) <= settings.Cash;
                if (thresholdPassed) remainder = CreateSequence(entrys);
            }
            return entrys.Select(entry => settings.FaceValues[entry.globalIndex]);
        }

        private  void ShiftLeft(Entry entry, Entry[] simulatorValues)
        {
            simulatorValues[entry.globalIndex] = null;
            simulatorValues[--entry.globalIndex] = entry;
        }

        private  void ShiftRight(Entry entry, Entry[] simulatorValues, int globalIndex)
        {
            simulatorValues[entry.globalIndex] = null;
            entry.globalIndex = globalIndex;
            simulatorValues[globalIndex] = entry;
        }

        private int SumCashOf(IEnumerable<Entry> entrys)
        {
            return entrys.Sum(entry => settings.FaceValues[entry.globalIndex].Value);
        }

        private  int CreateSequence(Entry[] entries)
        {
            var remainder = settings.Cash - SumCashOf(entries);
            foreach (var entry in entries)
            {
                settings.FaceValues[entry.globalIndex].Amount = 1;
            }
            if (remainder == 0) return remainder;
            foreach (var entry in entries)
            {
                var count = remainder / settings.FaceValues[entry.globalIndex].Value;
                settings.FaceValues[entry.globalIndex].Amount += count;
                remainder -= count * settings.FaceValues[entry.globalIndex].Value;
                if (remainder == 0) return remainder;
            }
            return remainder;
        }
    }
}