using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;

namespace UnityTestGameTest.TestComponents
{
    public class MockCardWithData : GameCard
    {
        public int data;
        public MockCardWithData(int data)
        {
            this.data = data;
        }
    }
}
