using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anecka.Labs.Convio;
using System.Threading.Tasks;

namespace RelayGear.Reach.Tests.Convio
{
    [TestClass]
    public class ConvioTests
    {
        [TestMethod]
        public void Get_Groups_Test()
        {
            ConvioGroups convioGrps = new ConvioGroups();

            List<GroupInfo> groups = convioGrps.GetGroups();
            Assert.IsTrue(groups.Count > 0);
        }

        [TestMethod]
        public void Get_GroupMembers_Test()
        {
            ConvioGroups convioGrps = new ConvioGroups();
            GroupMembersList convioGrpList = convioGrps.GetGroupMembers(403391);
            GroupMembers grp = convioGrpList.Items.SingleOrDefault(o => o.ConsId == 19845803);
            Assert.IsTrue(grp != null);
            Assert.IsTrue(convioGrpList.Items.Count > 0);
        }

        [TestMethod]
        public void Get_Events_Test()
        {
            ConvioEvents convioEvents = new ConvioEvents();
            List<Event> events = convioEvents.GetEventsForParticipant(5789270);
            Assert.IsTrue(events.Count > 0);
        }

        [TestMethod]
        public void Get_Particpants_For_Event_Test()
        {
            ConvioParticipants convioParticipants = new ConvioParticipants();
            List<Participant> participants = convioParticipants.GetParticipantsForEvent(32848);
            Assert.IsTrue(participants.Count > 0);
        }

        [TestMethod]
        public void Get_Participant_Progress_Test()
        {
            ConvioParticipants convioParticipants = new ConvioParticipants();
            ParticipantProgress participants = convioParticipants.GetProgress(4398520, 32848);

            Assert.IsTrue(participants.Personal != null);
        }

        [TestMethod]
        public void Get_Constituent_Info_Test()
        {
            ConvioParticipants convioParticipants = new ConvioParticipants();
            Constituent participants = convioParticipants.GetConstituentInfo(4398520);

            Assert.IsTrue(participants.Name != null);
        }
    }
}
