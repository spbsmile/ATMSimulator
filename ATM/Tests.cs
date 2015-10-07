using System.Collections.Generic;
using NUnit.Framework;

namespace ATM
{
    public class Tests
    {
        [Test]
        public void TestFirst()
        {
            var ai = new AI(new Settings(20, new List<int> {1, 5, 10, 50}));

            Assert.AreEqual(3, ai.DefineMaxCountFaceValues());
            var actual = new List<int>();
            foreach (var faceValue in ai.FindSequenceFaceValues(3))
            {
                for (var i = 0; i < faceValue.Amount; i++)
                {
                    actual.Add(faceValue.Value);
                }
            }
            CollectionAssert.AreEqual(new[] { 1, 1, 1, 1, 1, 5, 10 }, actual);
        }

        [Test]
        public void TestSecond()
        {
            var ai = new AI(new Settings(56, new List<int> { 1, 5, 10, 50 }));

            Assert.AreEqual(3, ai.DefineMaxCountFaceValues());
            var actual = new List<int>();
            foreach (var faceValue in ai.FindSequenceFaceValues(3))
            {
                for (var i = 0; i < faceValue.Amount; i++)
                {
                    actual.Add(faceValue.Value);
                }
            }
            CollectionAssert.AreEqual(new[] {1, 5, 50}, actual);
        }

        [Test]
        public void TestThird()
        {
            var ai = new AI(new Settings(57, new List<int> { 1, 5, 10, 50 }));

            Assert.AreEqual(3, ai.DefineMaxCountFaceValues());
            var actual = new List<int>();
            foreach (var faceValue in ai.FindSequenceFaceValues(3))
            {
                for (var i = 0; i < faceValue.Amount; i++)
                {
                    actual.Add(faceValue.Value);
                }
            }
            CollectionAssert.AreEqual(new[] { 1, 1, 5, 50 }, actual);
        }
    }
}
