using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class CreatorDetail
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string real_name { get; set; }
    }
}
