using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public List<Milestone> milestones { get; set; }
        [DataMember]
        public List<Component> components { get; set; }
        [DataMember]
        public string default_milestone { get; set; }
        [DataMember]
        public List<Version> versions { get; set; }
        [DataMember]
        public bool has_unconfirmed { get; set; }
        [DataMember]
        public string classification { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool is_active { get; set; }
        [DataMember]
        public string description { get; set; }
    }
}
