using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Anecka.Labs.Convio
{
    #region Groups

    [XmlRoot("listGroupsResponse")]
    public class GroupInfoList
    {
        public GroupInfoList() { Items = new List<GroupInfo>(); }
        [XmlElement("groupInfo")]
        public List<GroupInfo> Items { get; set; }
    }

    public class GroupInfo
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("numMembers")]
        public int NumMembers { get; set; }
    }

    [XmlRoot("getGroupMembersResponse")]
    public class GroupMembersList
    {
        public GroupMembersList() { Items = new List<GroupMembers>(); }
        [XmlElement("member")]
        public List<GroupMembers> Items { get; set; }
    }

    public class GroupMembers
    {
        [XmlElement("cons_id")]
        public long ConsId { get; set; }

        [XmlElement("name")]
        public GroupName Name { get; set; }

        [XmlElement("email")]
        public GroupEmail Email { get; set; }

        [XmlElement("primary_address")]
        public GroupingMailAddress MailingAddress { get; set; }
    }

    public class GroupName
    {
        [XmlElement("first")]
        public string FirstName { get; set; }

        [XmlElement("last")]
        public string LastName { get; set; }
    }

    public class GroupEmail
    {
        [XmlElement("primary_address")]
        public string PrimaryAddress { get; set; }
    }

    public class GroupingMailAddress
    {
        [XmlElement("street1")]
        public string Street { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("zip")]
        public string Zip { get; set; }
    }
    #endregion

    #region Events
    [XmlRoot("getRegisteredTeamraisersResponse")]
    public class EventList
    {
        public EventList() { Items = new List<Event>(); }
        [XmlElement("consId")]
        public long ConsId { get; set; }

        [XmlElement("teamraiser")]
        public List<Event> Items { get; set; }
    }

    public class Event
    {
        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("event_date")]
        public DateTime EventDate { get; set; }

        [XmlElement("lawson_code")]
        public string LawsonCode { get; set; }
    }
    #endregion
    
    #region Participants
    [XmlRoot("getParticipantProgressResponse")]
    public class ParticipantProgress
    {
        [XmlElement("daysLeft")]
        public int DaysLeft { get; set; }
        [XmlElement("personalProgress")]
        public PersonalProgress Personal { get; set; }

    }

    public class PersonalProgress
    {
        [XmlElement("goal")]
        public long Goal { get; set; }

        [XmlElement("percent")]
        public long Percent { get; set; }

        [XmlElement("raised")]
        public long Raised { get; set; }
    }

    [XmlRoot("getParticipantsResponse")]
    public class ParticipantList
    {
        public ParticipantList() { Items = new List<Participant>(); }
        [XmlElement("participant")]
        public List<Participant> Items { get; set; }
    }

    public class Participant
    {
        [XmlElement("name")]
        public GroupName Name { get; set; }

        [XmlElement("consId")]
        public long ConsId { get; set; }
    }
    #endregion  

#region "Constituents"
    [XmlRoot("getConsResponse")]
    public class Constituent
    {
        [XmlElement("cons_id")]
        public long ConsId { get; set; }

        [XmlElement("name")]
        public GroupName Name { get; set; }

        [XmlElement("email")]
        public GroupEmail Email { get; set; }

        [XmlElement("primary_address")]
        public GroupingMailAddress MailingAddress { get; set; }
    }
#endregion
    
}
