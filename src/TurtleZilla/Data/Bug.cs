using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Bug
    {
        [DataMember]
        public int estimated_time { get; set; }
        [DataMember]
        public string component { get; set; }
        [DataMember]
        public List<object> groups { get; set; }
        [DataMember]
        public string url { get; set; }
        [DataMember]
        public string platform { get; set; }
        [DataMember]
        public string creator { get; set; }
        [DataMember]
        public string assigned_to { get; set; }
        [DataMember]
        public object deadline { get; set; }
        [DataMember]
        public bool is_cc_accessible { get; set; }
        [DataMember]
        public int remaining_time { get; set; }
        [DataMember]
        public string qa_contact { get; set; }
        [DataMember]
        public List<object> cc_detail { get; set; }
        [DataMember]
        public List<object> blocks { get; set; }
        [DataMember]
        public CreatorDetail creator_detail { get; set; }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string last_change_time { get; set; }
        [DataMember]
        public List<object> see_also { get; set; }
        [DataMember]
        public int actual_time { get; set; }
        [DataMember]
        public object dupe_of { get; set; }
        [DataMember]
        public string classification { get; set; }
        [DataMember]
        public string creation_time { get; set; }
        [DataMember]
        public string product { get; set; }
        [DataMember]
        public string priority { get; set; }
        [DataMember]
        public AssignedToDetail assigned_to_detail { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool is_creator_accessible { get; set; }
        [DataMember]
        public string summary { get; set; }
        [DataMember]
        public List<object> keywords { get; set; }
        [DataMember]
        public string whiteboard { get; set; }
        [DataMember]
        public string resolution { get; set; }
        [DataMember]
        public string severity { get; set; }
        [DataMember]
        public string op_sys { get; set; }
        [DataMember]
        public bool is_open { get; set; }
        [DataMember]
        public string target_milestone { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public bool is_confirmed { get; set; }
        [DataMember]
        public List<object> depends_on { get; set; }
        [DataMember]
        public List<object> alias { get; set; }
        [DataMember]
        public List<object> cc { get; set; }
        [DataMember]
        public List<object> flags { get; set; }
    }
}
