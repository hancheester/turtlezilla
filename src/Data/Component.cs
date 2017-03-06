using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Component
    {
        [DataMember]
        public string default_assigned_to { get; set; }
        [DataMember]
        public int sort_key { get; set; }
        [DataMember]
        public bool is_active { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string default_qa_contact { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public FlagTypes flag_types { get; set; }
    }
}
