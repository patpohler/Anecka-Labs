using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anecka.Labs.Convio
{
    public class ConvioParticipants : ConvioBase 
    {
        public ParticipantProgress GetProgress(long consId, long eventId)
        {
            AddParameter("method", "getParticipantProgress");
            AddParameter("cons_id", consId.ToString());
            AddParameter("fr_id", eventId.ToString());

            ParticipantProgress progress = GetResponse<ParticipantProgress>("CRTeamraiserAPI", "getParticipantProgressResponse");

            return progress;
        }

        public List<Participant> GetParticipantsForEvent(long eventId)
        {
            AddParameter("method", "getParticipants");
            AddParameter("fr_id", eventId.ToString());
            AddParameter("first_name", "%%%");
            AddParameter("list_page_offset", "0");

            int offset = 0;
            List<Participant> participants = new List<Participant>();


            ParticipantList participantList = GetResponse<ParticipantList>(BASE_URL2, "GET", "CRTeamraiserAPI", "getParticipantsResponse");

            while (participantList != null && participantList.Items.Count > 0)
            {
                participantList.Items.ForEach(p => participants.Add(new Participant { ConsId = p.ConsId, Name = p.Name }));
                offset++;
                AddParameter("list_page_offset", offset.ToString());
                participantList = GetResponse<ParticipantList>(BASE_URL2, "GET", "CRTeamraiserAPI", "getParticipantsResponse");
            }

            return participants;
        }

        public Constituent GetConstituentInfo(long consId)
        {
            AddParameter("method", "getUser");
            AddParameter("cons_id", consId.ToString());
            //AddParameter("first_name", "%%%");

            Constituent constituent = GetResponse<Constituent>("POST", "SRConsAPI", "getConsResponse");

            return constituent;
        }
    }
}
