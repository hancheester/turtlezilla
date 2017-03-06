using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TurtleZilla.Data
{
    [DataContract]
    public class FlagTypes
    {
        [DataMember]
        public List<object> attachment { get; set; }
        [DataMember]
        public List<object> bug { get; set; }
    }
}
