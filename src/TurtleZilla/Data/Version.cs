using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Version
    {
        [DataMember]
        public int sort_key { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public bool is_active { get; set; }
    }
}
