using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Milestone
    {
        [DataMember]
        public bool is_active { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int sort_key { get; set; }
    }
}
