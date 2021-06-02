using TameOfThrones.BL.Interfaces;
using TameOfThrones.BL.Interfaces.Data;

namespace TameOfThrones.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TameOfThronesUnitTest
    {
        [TestMethod]
        public void KingdomInfo_ValidName()
        {
            IKingdom kingdomInfo = BlAccessor.GetKingdom(KingdomData.AirKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.AirKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.AirKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.AirKingdomRuler);

            kingdomInfo = BlAccessor.GetKingdom(KingdomData.FireKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.FireKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.FireKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.FireKingdomRuler);

            kingdomInfo = BlAccessor.GetKingdom(KingdomData.IceKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.IceKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.IceKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.IceKingdomRuler);

            kingdomInfo = BlAccessor.GetKingdom(KingdomData.LandKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.LandKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.LandKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.LandKingdomRuler);

            kingdomInfo = BlAccessor.GetKingdom(KingdomData.SpaceKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.SpaceKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.SpaceKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.SpaceKingdomRuler);

            kingdomInfo = BlAccessor.GetKingdom(KingdomData.WaterKingdomName);
            Assert.IsNotNull(kingdomInfo);
            Assert.IsTrue(kingdomInfo.Emblem == KingdomData.WaterKingdomEmblem);
            Assert.IsTrue(kingdomInfo.KingdomName == KingdomData.WaterKingdomName);
            Assert.IsTrue(kingdomInfo.RulerName == KingdomData.WaterKingdomRuler);

        }

        [TestMethod]
        public void KingdomInfo_InvalidName()
        {
            IKingdom kingdomInfo = BlAccessor.GetKingdom("AnyString");
            Assert.IsNull(kingdomInfo);
        }

        [TestMethod]
        public void Kingdom_And_Output_Test()
        {
            // Init
            IKingdom senderKingdom = BlAccessor.GetKingdom(KingdomData.FireKingdomName);

            // If sender and receiver are same
            IKingdom receiverKingdom = BlAccessor.GetKingdom(KingdomData.FireKingdomName);
            bool result = senderKingdom.IsAllienceProvided(receiverKingdom, string.Empty);
            Assert.IsFalse(result);

            // If sender and receiver are different, with matching message
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.SpaceKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "GORILLA");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 1);
            Assert.IsTrue(receiverKingdom.AllienceKingdom == senderKingdom);

            // Add more allience
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.IceKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "Mammoth");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 2);
            Assert.IsTrue(receiverKingdom.AllienceKingdom == senderKingdom);

            // Add more allience
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.LandKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "panDA");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 3);
            Assert.IsTrue(receiverKingdom.AllienceKingdom == senderKingdom);

            // No allience added if message not match
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.WaterKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "non-match message");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 3);
            Assert.IsTrue(receiverKingdom.AllienceKingdom == null);

            // No allience added if message not match
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.AirKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "non-match message");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 3);
            Assert.IsTrue(receiverKingdom.AllienceKingdom == null);
            
            // No allience added if message is matching but all kingdoms are processed
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.AirKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "owl");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 4);
            Assert.IsTrue(receiverKingdom.AllienceKingdom != null);

            // No allience provided if already in-allience with other
            senderKingdom = BlAccessor.GetKingdom(KingdomData.AirKingdomName);
            receiverKingdom = BlAccessor.GetKingdom(KingdomData.LandKingdomName);
            result = senderKingdom.IsAllienceProvided(receiverKingdom, "Panda");
            Assert.IsTrue(result);
            Assert.IsTrue(senderKingdom.AlliesNameList.Count == 0);
            Assert.IsTrue(receiverKingdom.AllienceKingdom != null);

            // Check the output as per above selections
            IOutputDataProvider output = BlAccessor.GetOutputDataProvider();
            string expectedAlliesList = KingdomData.SpaceKingdomName + ", " + KingdomData.IceKingdomName +
                                        ", " + KingdomData.LandKingdomName + ", " + KingdomData.AirKingdomName;
            Assert.IsTrue(output.RulerName == KingdomData.FireKingdomRuler);
            Assert.IsTrue(output.AlliesList == expectedAlliesList);

            BlAccessor.ResetGlobals();
            Assert.IsTrue(output.RulerName == KingdomData.NoneValue);
            Assert.IsTrue(output.AlliesList == KingdomData.NoneValue);
        }

        [TestMethod]
        public void InputMessageHandlerTest()
        {
            string messageToKingdom = "water, what are you doing";
            bool processResult = BlAccessor.ProcessMessageInput(messageToKingdom, true);
            Assert.IsTrue(processResult);

            // Check with invalid kingdom
            messageToKingdom = "invalid, what are you doing";
            processResult = BlAccessor.ProcessMessageInput(messageToKingdom, true);
            Assert.IsFalse(processResult);
        }

        [TestMethod]
        public void BallotInputHandlerTest()
        {
            // Check with space separator
            string competingKingdoms = "air water";
            BallotResult processResult = BlAccessor.ProcessBallotInput(competingKingdoms);
            Assert.IsTrue(processResult.IsSuccess);

            // Check with comma separator
            competingKingdoms = "air, water";
            processResult = BlAccessor.ProcessBallotInput(competingKingdoms);
            Assert.IsFalse(processResult.IsSuccess);

            // Check with comma separated invalid entry
            competingKingdoms = "air, xyz";
            processResult = BlAccessor.ProcessBallotInput(competingKingdoms);
            Assert.IsFalse(processResult.IsSuccess);

            // Check with space separated invalid entry
            competingKingdoms = "air xyz";
            processResult = BlAccessor.ProcessBallotInput(competingKingdoms);
            Assert.IsFalse(processResult.IsSuccess);
        }
    }
}
