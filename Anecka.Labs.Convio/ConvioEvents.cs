using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anecka.Labs.Convio
{
    public class ConvioEvents : ConvioBase
    {
        public List<Event> GetEventsForParticipant(long consId)
        {
            AddParameter("method", "getRegisteredTeamraisers");
            AddParameter("cons_id", consId.ToString());

            EventList eventList = GetResponse<EventList>("CRTeamraiserAPI", "getRegisteredTeamraisersResponse");
            
            List<Event> events = new List<Event>();
            if (eventList != null && eventList.Items.Count > 0)
            {
                //events = eventList.Items.Where(o => o.EventDate >= DateTime.Now.Date).ToList();
                events = eventList.Items.Where(o => o.EventDate.Year >= DateTime.Now.Year).ToList();
            }

            return events;
        }
    }
}
