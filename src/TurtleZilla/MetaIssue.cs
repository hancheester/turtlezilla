using System;
using System.Collections.ObjectModel;

namespace TurtleZilla
{
    [Serializable]
    internal static class MetaIssue
    {
        public static readonly IProperty<Issue, int> Id = new Property<Issue, int>(issue => issue.Id, (issue, value) => issue.Id = value);
        public static readonly IProperty<Issue, string> Product = new Property<Issue, string>(issue => issue.Product, (issue, value) => issue.Product = value);
        public static readonly IProperty<Issue, string> Component = new Property<Issue, string>(issue => issue.Component, (issue, value) => issue.Component = value);
        public static readonly IProperty<Issue, string> Status = new Property<Issue, string>(issue => issue.Status, (issue, value) => issue.Status = value);
        public static readonly IProperty<Issue, string> Priority = new Property<Issue, string>(issue => issue.Priority, (issue, value) => issue.Priority = value);
        public static readonly IProperty<Issue, string> AssignedTo = new Property<Issue, string>(issue => issue.AssignedTo, (issue, value) => issue.AssignedTo = value);        
        public static readonly IProperty<Issue, string> Summary = new Property<Issue, string>(issue => issue.Summary, (issue, value) => issue.Summary = value);

        public static ReadOnlyCollection<IProperty<Issue>> Properties { get; private set; }

        static MetaIssue()
        {
            Properties = new ReadOnlyCollection<IProperty<Issue>>(new IProperty<Issue>[]
            {
                Id,
                Product,
                Component,
                Status,
                Priority,
                AssignedTo,
                Summary
            });
        }

        public static IProperty<Issue> GetPropertyByField(IssueField field)
        {
            var index = (int)field;

            if (index < 0 || index >= Properties.Count)
                throw new ArgumentOutOfRangeException("field", field, null);

            return Properties[index];
        }
    }
}
