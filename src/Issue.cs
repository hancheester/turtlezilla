using System;
using System.Linq;
using System.Text;

namespace TurtleZilla
{
    [Serializable]
    internal sealed class Issue
    {
        private string _product;
        private string _component;
        private string _status;
        private string _priority;
        private string _assigned_to;
        private string _summary;

        public int Id { get; set; }
        public string Product { get { return _product ?? string.Empty; } set { _product = value; } }
        public string Component { get { return _component ?? string.Empty; } set { _component = value; } }
        public string Status { get { return _status ?? string.Empty; } set { _status = value; } }
        public string Priority { get { return _priority ?? string.Empty; } set { _priority = value; } }
        public string AssignedTo { get { return _assigned_to ?? string.Empty; } set { _assigned_to = value; } }
        public string Summary { get { return _summary ?? string.Empty; } set { _summary = value; } }

        public bool HasOwner
        {
            get
            {
                var owner = this.AssignedTo;
                return owner.Length > 0 && !owner.All(ch => ch == '-');
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{ Id = ").Append(Id);
            builder.Append(", Product = ").Append(Product);
            builder.Append(", Component = ").Append(Component);
            builder.Append(", Status = ").Append(Status);
            builder.Append(", Priority = ").Append(Priority);
            builder.Append(", AssignedTo = ").Append(AssignedTo);
            builder.Append(", Summary = ").Append(Summary);
            builder.Append(" }");

            return builder.ToString();
        }
    }
}
