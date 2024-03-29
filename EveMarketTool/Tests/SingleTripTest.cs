using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace EveMarketTool.Tests
{
    [TestFixture]
    public class SingleTripTest
    {
        SingleTrip trip;
        SingleTrip empty;
        Map map;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Parameters param = new Parameters(10.0f, 10.0f, "none", TripType.SingleTrip, 0);
            ItemDatabase database = TestObjectFactory.CreateItemDatabase();
            map = TestObjectFactory.CreateMap();
            trip = new SingleTrip(map, map.GetStation(11), map.GetStation(31));
            trip.AddPurchase(new Transaction(new Trade(database.GetItemType("Navitas"), 1000.0f, 3), new Trade(database.GetItemType("Navitas"), 1100.0f, 3)));

            empty = new SingleTrip(map, map.GetStation(11), map.GetStation(52));
        }

        [SetUp]
        public void TestCaseSetUp()
        {
        }

        [Test]
        public void TestProfit()
        {
            Assert.AreEqual(267.0f, trip.Profit);
            Assert.AreEqual(0.0f, empty.Profit);
        }

        [Test]
        public void TestProfitPerWarp()
        {
            Assert.AreEqual(267.0f / 11.0f, trip.ProfitPerWarp(true));
            Assert.AreEqual(267.0f / 5.0f, trip.ProfitPerWarp(false));
            Assert.AreEqual(0.0f, empty.Profit);
        }

        [Test]
        public void TestProfitPerWarpFrom()
        {
            Assert.AreEqual(267.0f / 16.0f, trip.ProfitPerWarpFrom(map.GetSystem(3), true));
            Assert.AreEqual(267.0f / 10.0f, trip.ProfitPerWarpFrom(map.GetSystem(3), false));
            Assert.AreEqual(0.0f, empty.ProfitPerWarpFrom(map.GetSystem(2), true));
        }

        [Test]
        public void TestJumps()
        {
            Assert.AreEqual(5, trip.Jumps(true));
            Assert.AreEqual(2, trip.Jumps(false));
            Assert.AreEqual(int.MaxValue, empty.Jumps(true));
        }

        [Test]
        public void TestWarps()
        {
            Assert.AreEqual(11, trip.Warps(true));
            Assert.AreEqual(5, trip.Warps(false));
            Assert.AreEqual(int.MaxValue, empty.Warps(true));
        }

        [Test]
        public void TestSecurity()
        {
            Assert.AreEqual(SecurityStatus.Level.LowSecShortcut, trip.Security);
            Assert.AreEqual(SecurityStatus.Level.Isolated, empty.Security);
        }

        [Test]
        public void TestCost()
        {
            Assert.AreEqual(3000.0f, trip.Cost);
            Assert.AreEqual(0.0f, empty.Cost);
        }

    }
}
