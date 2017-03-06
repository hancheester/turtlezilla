using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TurtleZilla.Data;
using TurtleZilla.HttpUtility;

namespace TurtleZilla
{
    public partial class IssueBrowserDialog : Form
    {
        private readonly string _titleFormat;
        private readonly string _foundFormat;
        private ReadOnlyCollection<int> _selectedIssues;
        private ReadOnlyCollection<Issue> _readonlySelectedIssues;
        private readonly List<Issue> _selectedIssueObjects;
        private Action _aborter;
        private bool _closed;               
        private readonly ObservableCollection<ListViewItem<Issue>> _issues;

        public string Url { get; set; }
        public string Product { get; set; }
        public string APIKey { get; set; }

        public IList<int> SelectedIssues
        {
            get
            {
                if (_selectedIssues == null)
                    _selectedIssues = new ReadOnlyCollection<int>(SelectedIssueObjects.Select(issue => issue.Id).ToList());

                return _selectedIssues;
            }
        }

        internal IList<Issue> SelectedIssueObjects
        {
            get
            {
                if (_readonlySelectedIssues == null)
                    _readonlySelectedIssues = new ReadOnlyCollection<Issue>(_selectedIssueObjects);

                return _readonlySelectedIssues;
            }
        }

        public IssueBrowserDialog()
        {
            InitializeComponent();

            _titleFormat = Text;
            _foundFormat = labelFound.Text;

            var issues = _issues = new ObservableCollection<ListViewItem<Issue>>();
            issues.ItemAdded += (sender, args) => ListIssues(Enumerable.Repeat(args.Item, 1));
            issues.ItemsAdded += (sender, args) => ListIssues(args.Items);
            issues.ItemRemoved += (sender, args) => listViewIssues.Items.Remove(args.Item);
            issues.Cleared += delegate { listViewIssues.Items.Clear(); };

            _selectedIssueObjects = new List<Issue>();

            var searchSourceItems = comboBoxSearchField.Items;
            searchSourceItems.Add(new MultiFieldIssueSearchSource("All fields", MetaIssue.Properties));

            foreach (IssueField field in Enum.GetValues(typeof(IssueField)))
            {
                searchSourceItems.Add(new SingleFieldIssueSearchSource(field.ToString(), MetaIssue.GetPropertyByField(field),
                    field == IssueField.Summary
                    || field == IssueField.Id                    
                    ? SearchableStringSourceCharacteristics.None
                    : SearchableStringSourceCharacteristics.Predefined));
            }

            comboBoxSearchField.SelectedIndex = 0;

            UpdateControlStates();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Product.Length > 0)
            {
                DownloadIssues();
            }
            
            base.OnLoad(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (DialogResult != DialogResult.OK || e.Cancel) return;

            var selectedIssues = _issues.Where(l => l.Checked)
                                    .Select(l => l.Tag)
                                    .ToArray();

            _selectedIssueObjects.AddRange(selectedIssues);
        }

        protected override void OnClosed(EventArgs e)
        {
            Debug.Assert(!_closed);

            Release(ref _aborter, a => a());

            _closed = true;

            base.OnClosed(e);
        }

        private void DownloadIssues()
        {
            Debug.Assert(_aborter == null);

            statusWork.Visible = true;
            // What is \x2026 ? To find out, please refer http://www.fileformat.info/info/unicode/char/2026/index.htm
            labelStatus.Text = "Downloading\x2026";

            _aborter = DownloadIssues(Product,
                                      0,
                                      checkBoxIncludeClosed.Checked,
                                      OnIssuesDownloaded,
                                      OnUpdateProgress,
                                      OnDownloadComplete);
        }

        private bool OnIssuesDownloaded(IEnumerable<Bug> bugs)
        {
            Debug.Assert(bugs != null);

            if (_closed) return false;

            var items = bugs.Select(bug =>
            {
                var issue = new Issue
                {
                    Id = bug.id,
                    Product = bug.product,
                    Component = bug.component,
                    Status = bug.status,
                    Priority = bug.priority,
                    AssignedTo = bug.assigned_to,
                    Summary = bug.summary
                };

                var item = new ListViewItem<Issue>(issue.Id.ToString())
                {
                    Tag = issue,
                    UseItemStyleForSubItems = true
                };

                var subItems = item.SubItems;
                subItems.Add(issue.Component);
                subItems.Add(issue.Status);
                subItems.Add(issue.Priority);
                subItems.Add(issue.AssignedTo);
                subItems.Add(issue.Summary);

                return item;

            }).ToArray();

            _issues.AddRange(items);

            return items.Length > 0;           
        }

        private void OnUpdateProgress(int percentage)
        {
            if (_closed) return;

            labelStatus.Text = string.Format("Downloading\x2026{0}% transferred", percentage);

            UpdateTitle();
        }

        private void OnDownloadComplete(bool cancelled, Exception e)
        {
            if (_closed) return;

            _aborter = null;
            statusWork.Visible = false;

            if (cancelled)
            {
                labelStatus.Text = "Download aborted";
                return;
            }

            if (e != null)
            {
                labelStatus.Text = "Error downloading";
                MessageBox.Show(this, e.Message, "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            labelStatus.Text = string.Format("{0} issue(s) downloaded", _issues.Count.ToString("N0"));

            UpdateTitle();
        }

        private void ListIssues(IEnumerable<ListViewItem<Issue>> items)
        {
            Debug.Assert(items != null);

            var searchWords = comboBoxSearch.Text.Split().Where(s => s.Length > 0);
            if (searchWords.Any())
            {
                var provider = (ISearchSourceStringProvider<Issue>)comboBoxSearchField.SelectedItem;
                items = from item in items
                        let issue = item.Tag
                        where searchWords.All(word => provider.ToSearchableString(issue).IndexOf(word, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        select item;
            }

            //
            // We need to stop listening to the ItemChecked event because it 
            // is raised for each item added and this has visually noticable 
            // performance implications for the user on large lists.
            //

            ItemCheckedEventHandler onItemChecked = listViewIssues_ItemChecked;
            listViewIssues.ItemChecked -= onItemChecked;

            listViewIssues.Items.AddRange(items.ToArray());

            //
            // Update control states once and start listening to the 
            // ItemChecked event once more.
            //

            UpdateControlStates();
            listViewIssues.ItemChecked += onItemChecked;

            labelFound.Text = string.Format(_foundFormat, listViewIssues.Items.Count.ToString("N0"));
            labelFound.Visible = searchWords.Any();
        }

        private void listViewIssues_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateControlStates();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            _issues.Clear();
            OnIssuesDownloaded(Enumerable.Empty<Bug>());
            UpdateTitle();
            DownloadIssues();
        }

        private void comboBoxSearch_TextChanged(object sender, EventArgs e)
        {
            listViewIssues.Items.Clear();
            ListIssues(_issues);
        }

        private void comboBoxSearchField_SelectedIndexChanged(object sender, EventArgs e)
        {
            var provider = (ISearchSourceStringProvider<Issue>)comboBoxSearchField.SelectedItem;
            if (provider == null)
                return;

            var definitions = comboBoxSearch.Items;
            definitions.Clear();

            var isPredefined = SearchableStringSourceCharacteristics.Predefined == (
                provider.SourceCharacteristics & SearchableStringSourceCharacteristics.Predefined);

            if (!isPredefined)
                return;

            // TODO: Update definitions if issues are still being downloaded

            definitions.AddRange(
                _issues.Select(lvi => provider.ToSearchableString(lvi.Tag))
                .Distinct(StringComparer.CurrentCultureIgnoreCase)
                .ToArray());
        }

        private void UpdateControlStates()
        {
            //buttonDetail.Enabled = listViewIssues.SelectedItems.Count == 1;
            buttonOK.Enabled = listViewIssues.CheckedItems.Count > 0;
        }

        private void UpdateTitle()
        {
            Text = string.Format(_titleFormat, Product, _issues.Count.ToString("N0"));
        }

        private Action DownloadIssues(
            string project,
            int start,
            bool includeClosedIssues,
            Func<IEnumerable<Bug>, bool> onDownloaded,
            Action<int> onProgress,
            Action<bool, Exception> onCompleted)
        {
            Debug.Assert(project != null);
            Debug.Assert(onDownloaded != null);
            
            var client = new RestJsonClient(Url + BugzillaAPI.BUG, HttpVerb.GET);
            var resolution = includeClosedIssues ? "" : "---";

            Action<int> pager = next => client.SendRequestAsync(string.Format("api_key={0}&product={1}&offset={2}&limit=20&resolution={3}", APIKey, Product, next, resolution));
            
            client.DownloadCompleted += (sender, args) =>
            {
                var data = args.Serialize<Bugzilla>();
                var more = onDownloaded(data.bugs);

                if (more)
                {
                    start += data.bugs.Count;
                    pager(start);
                }
                else
                {
                    onCompleted?.Invoke(false, null);
                }
            };

            if (onProgress != null)
                client.ProgressChanged += (sender, args) => onProgress(args);

            pager(start);

            return client.Abort;
        }

        private static void Release<T>(ref T member, Action<T> free) where T : class
        {
            Debug.Assert(free != null);

            var local = member;
            if (local == null) return;

            member = null;
            free(local);
        }

        [Serializable, Flags]
        private enum SearchableStringSourceCharacteristics
        {
            None,
            Predefined
        }

        /// <summary>
        /// Represents a provider that yields the string for an object that 
        /// can be used in text-based searches and indexing.
        /// </summary>
        private interface ISearchSourceStringProvider<T>
        {
            SearchableStringSourceCharacteristics SourceCharacteristics { get; }
            string ToSearchableString(T item);
        }

        /// <summary>
        /// Base class for transforming an <see cref="Issue"/> into a 
        /// searchable string.
        /// </summary>
        private abstract class IssueSearchSource : ISearchSourceStringProvider<Issue>
        {
            private readonly string _label;

            public SearchableStringSourceCharacteristics SourceCharacteristics { get; private set; }

            public abstract string ToSearchableString(Issue issue);

            protected IssueSearchSource(string label, SearchableStringSourceCharacteristics sourceCharacteristics)
            {
                _label = label;
                SourceCharacteristics = sourceCharacteristics;
            }

            public override string ToString()
            {
                return _label;
            }
        }

        /// <summary>
        /// An <see cref="IssueSearchSource"/> implementation that uses a 
        /// property of an <see cref="Issue"/> as the searchable string.
        /// </summary>
        private sealed class SingleFieldIssueSearchSource : IssueSearchSource
        {
            private readonly IProperty<Issue> _property;

            public SingleFieldIssueSearchSource(string label, IProperty<Issue> property) 
                : this(label, property, SearchableStringSourceCharacteristics.None)
            { }

            public SingleFieldIssueSearchSource(string label, IProperty<Issue> property, SearchableStringSourceCharacteristics sourceCharacteristics) 
                : base(label, sourceCharacteristics)
            {
                _property = property;
            }

            public override string ToSearchableString(Issue issue)
            {             
                return _property.GetValue(issue).ToString();
            }
        }

        /// <summary>
        /// An <see cref="IssueSearchSource"/> implementation that uses 
        /// concatenates multiple properties of an <see cref="Issue"/> as 
        /// the searchable string.
        /// </summary>
        private sealed class MultiFieldIssueSearchSource : IssueSearchSource
        {
            private readonly IProperty<Issue>[] _properties;

            public MultiFieldIssueSearchSource(string label, IEnumerable<IProperty<Issue>> properties) :
                base(label, SearchableStringSourceCharacteristics.None)
            {
                _properties = properties.Where(p => p != null).ToArray();
            }

            public override string ToSearchableString(Issue issue)
            {
                return _properties.Aggregate(new StringBuilder(), (sb, p) => sb.Append(p.GetValue(issue)).Append(' ')).ToString();
            }
        }
    }
}
