using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Anecka.Labs.Convio
{
    public class ConvioGroups : ConvioBase
    {
        public List<GroupInfo> GetGroups()
        {
            return GetGroups("RFL_FY11_SA");
        }

        public List<GroupInfo> GetGroups(string searchTerm)
        {
            AddParameter("method", "listGroups");
            AddParameter("list_page_size", "500");
            AddParameter("selection_mode", "HEIRARCHY");
            //AddParameter("list_filter_text", "RFL_FY11_SA");
            AddParameter("list_filter_text", searchTerm);
            AddParameter("list_record_offset", "1");

            List<GroupInfo> groupList = new List<GroupInfo>();

            GroupInfoList tmpGroups = GetResponse<GroupInfoList>("SRGroupAPI", "listGroupsResponse"); //GetMultiple<GroupInfo>(url);
            int offsetCount = 1;

            while (tmpGroups != null && tmpGroups.Items.Count > 0)
            {
                foreach (GroupInfo grp in tmpGroups.Items)
                {
                    //if (grp.NumMembers > 0)
                    groupList.Add(grp);
                }
                offsetCount += 500;
                AddParameter("list_record_offset", (offsetCount).ToString());
                tmpGroups = GetResponse<GroupInfoList>("SRGroupAPI", "listGroupsResponse");
            }

            return groupList;
        }

        public List<GroupMembers> GetAllGroupMembers(List<GroupInfo> groups)
        {
            List<GroupMembers> groupMembers = new List<GroupMembers>();
            foreach (GroupInfo group in groups)
            {

                GroupMembersList tempMemberList = GetGroupMembers(group.Id);
                if (tempMemberList != null && tempMemberList.Items.Count > 0)
                {
                    foreach (GroupMembers member in tempMemberList.Items)
                    {
                        GroupMembers tempMember = groupMembers.SingleOrDefault(o => o.ConsId == member.ConsId);
                        if (tempMemberList == null)
                            groupMembers.Add(member);
                    }
                }
            }

            return groupMembers;
        }

        public GroupMembersList GetGroupMembers(long groupId)
        {
            AddParameter("method", "getGroupMembers");
            AddParameter("group_id", groupId.ToString());

            List<GroupMembers> memberList = new List<GroupMembers>();

            GroupMembersList tmpGroups = GetResponse<GroupMembersList>("POST", "SRConsAPI", "getGroupMembersResponse"); //GetMultiple<GroupInfo>(url);

            return tmpGroups;
        }
    }
}
