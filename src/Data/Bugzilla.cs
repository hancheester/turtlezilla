using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class Bugzilla
    {
        [DataMember]
        public List<Bug> bugs { get; set; }

        [DataMember]
        public string version { get; set; }

        [DataMember]
        public List<Product> products { get; set; }
    }
}
