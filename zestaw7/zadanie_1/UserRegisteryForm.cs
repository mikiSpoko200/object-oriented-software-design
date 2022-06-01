using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace zadanie_1
{
    public partial class UserRegisteryForm : Form, 
        ISubscriber<UserProfileModifiedNotification>,
        ISubscriber<UserProfileSelectedNotification>,
        ISubscriber<UserProfileAddedNotification>,
        ISubscriber<CategorySelectedNotification>
    {
        private const int LIST_VIEW_TAB_INDEX = 0;
        private const int USER_DETAILS_TAB_INDEX = 1;

        private const int STUDENT_TREE_NODE_INDEX = 0;
        private const int LECTURER_TREE_NODE_INDEX = 1;
        private const string PATH_SEPARATOR = "/";

        
        static public EventAggregator EventAggregator = new EventAggregator();


        private Dictionary<Category, List<Person>> _registery = new Dictionary<Category, List<Person>>()
        {
            { Category.Stuents,   new List<Person>() },
            { Category.Lecturers, new List<Person>() },
        };
        public UserRegisteryForm()
        {
            InitializeComponent();
            InitializeRegisteryForm();
        }

        private void AddUserWithCategory(Person user, Category category)
        {
            this._registery[category].Add(user);
            this.RaiseProfileAddedEvent(user, category);
        }

        private void ModifyUserWithCategory(Person old_profile, Person new_profile, Category category)
        {
            this._registery[category].Remove(old_profile);
            this._registery[category].Add(new_profile);
            this.RaiseProfileModifidiedEvent(old_profile, new_profile, category);
        }

        #region Example data loading methods
        private List<Person> InitialStudentData()
        {
            return new List<Person>() {
                new Person("Jan", "Kochanowski", "ul. Kwiatowa 1/2, Wrocław", new DateTime(1998, 1, 1)),
                new Person("Adam", "Mickiewicz", "ul. Jakaś 2/3, Wrocław", new DateTime(1999, 3, 12))
            };
        }

        private List<Person> InitialLecturerData()
        {
            return new List<Person>()
            {
                new Person("Jerzy", "Tomaszewski", "ul. Ulica 3/4, Wrocław", new DateTime(1983, 2, 10)),
                new Person("Tomasz", "Makowski", "ul. FooBar 4/5, Wrocław", new DateTime(1986, 4, 20))
            };
        }

        private List<Person> LoadUserCategory(Category category)
        {
            return this._registery[category];
        }
        #endregion

        #region Visual component initialization
        private void InitializeRegisteryForm()
        {
            workingRegionTabControl.ItemSize = new System.Drawing.Size() { Width = 0, Height = 1 };

            foreach (var student in this.InitialStudentData())
            {
                this._registery[Category.Stuents].Add(student);
            }
            foreach (var lecturer in this.InitialLecturerData())
            {
                this._registery[Category.Lecturers].Add(lecturer);
            }

            this.InitilizeUserTreeView();
            this.InitilizeUserListView();

            this.ReloadUsersTreeViewData();
            this.ReloadUsersListViewData(Category.Stuents);
        }
        private void InitilizeUserTreeView()
        {
            this.userTreeView.Nodes.Clear();
            this.userTreeView.PathSeparator = PATH_SEPARATOR;
            this.userTreeView.Nodes.Add(Category.Stuents.IntoString());
            this.userTreeView.Nodes.Add(Category.Lecturers.IntoString());
        }
        private void InitilizeUserListView()
        {
            this.userListView.Columns.Clear();
            this.userListView.Columns.Add("Nazwisko", -2, HorizontalAlignment.Left);
            this.userListView.Columns.Add("Imię", -2, HorizontalAlignment.Left);
            this.userListView.Columns.Add("Adres", -2, HorizontalAlignment.Left);
            this.userListView.Columns.Add("Data urodzenia", -2, HorizontalAlignment.Left);
        }

        private void ReloadUsersTreeViewData()
        {
            var students = this.LoadUserCategory(Category.Stuents);
            var lecturers = this.LoadUserCategory(Category.Lecturers);
            foreach (TreeNode node in this.userTreeView.Nodes)
            {
                node.Nodes.Clear();
            }
            foreach (var student in students)
            {
                TreeNode treeNode = new TreeNode(student.FullName());
                treeNode.Tag = student;
                this.userTreeView.Nodes[STUDENT_TREE_NODE_INDEX].Nodes.Add(treeNode);
            }
            foreach (var lecturer in lecturers)
            {
                TreeNode treeNode = new TreeNode(lecturer.FullName());
                treeNode.Tag = lecturer;
                this.userTreeView.Nodes[LECTURER_TREE_NODE_INDEX].Nodes.Add(treeNode);
            }
        }

        private void ReloadUsersListViewData(Category category)
        {
            var users = this.LoadUserCategory(category);

            this.userListView.Items.Clear();
            foreach (Person user in users)
            {
                var entry = this.userListView.Items.Add(user.Surname);
                entry.SubItems.Add(user.Name);
                entry.SubItems.Add(user.Address);
                entry.SubItems.Add(user.DateOfBirth.ToString());
            }
        }
        #endregion

        #region Wrappers for Event Raising
        private void RaiseCategorySelectedEvent(Category category)
        {
            var EventInfo = new CategorySelectedNotification(category);
            UserRegisteryForm.EventAggregator.Publish<CategorySelectedNotification>(EventInfo);
        }

        private void RaiseProfileSelectedEvent(Person profile, Category category)
        {
            var EventInfo = new UserProfileSelectedNotification(profile, category);
            UserRegisteryForm.EventAggregator.Publish<UserProfileSelectedNotification>(EventInfo);
        }

        private void RaiseProfileAddedEvent(Person profile, Category category)
        {
            var EventInfo = new UserProfileAddedNotification(profile, category);
            UserRegisteryForm.EventAggregator.Publish<UserProfileAddedNotification>(EventInfo);
        }

        private void RaiseProfileModifidiedEvent(Person old_profile, Person new_profile, Category category)
        {
            var EventInfo = new UserProfileModifiedNotification(old_profile, new_profile, category);
            UserRegisteryForm.EventAggregator.Publish<UserProfileModifiedNotification>(EventInfo);
        }
        #endregion

        # region Handlers
        public void HandleEvent(CategorySelectedNotification EventInfo)
        {
            this.workingRegionTabControl.SelectedTab = this.workingRegionTabControl.TabPages[LIST_VIEW_TAB_INDEX];
            this.userAddButton.Tag = EventInfo.SelectedCategory;
            this.ReloadUsersListViewData(EventInfo.SelectedCategory);
        }

        public void HandleEvent(UserProfileSelectedNotification EventInfo)
        {
            this.workingRegionTabControl.SelectedTab = this.workingRegionTabControl.TabPages[USER_DETAILS_TAB_INDEX];
            this.userModifyButton.Tag = (EventInfo.SelectedProfile, EventInfo.ProfileCategory);
            this.nameTextBox.Text = EventInfo.SelectedProfile.Name;
            this.surnameTextBox.Text = EventInfo.SelectedProfile.Surname;
            this.adresTextBox.Text = EventInfo.SelectedProfile.Address;
            this.dateOfBirthTextBox.Text = EventInfo.SelectedProfile.DateOfBirth.ToString();
        }

        public void HandleEvent(UserProfileAddedNotification EventInfo)
        {
            this.ReloadUsersTreeViewData();
            this.ReloadUsersListViewData(EventInfo.ProfileCategory);
        }

        public void HandleEvent(UserProfileModifiedNotification EventInfo)
        {
            this.ReloadUsersTreeViewData();
            this.ReloadUsersListViewData(EventInfo.ProfileCategory);
        }
        #endregion

        #region WinForms Event wrappers
        private void userTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            this.RaiseCategorySelectedEvent(e.Node.Text.IntoCategory());
        }

        private void userTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string[] tree_path = e.Node.FullPath.Split(UserRegisteryForm.PATH_SEPARATOR[0]);
            Category category = tree_path[0].IntoCategory();
            if (e.Node.Level == 0)
            {
                this.RaiseCategorySelectedEvent(category);
            } 
            else
            {
                this.RaiseProfileSelectedEvent((Person)e.Node.Tag, category);
            }
        }

        private void userAddButton_Click(object sender, EventArgs e)
        {
            UserRegisteryForm.EventAggregator.AddSubscriber(this as ISubscriber<UserProfileAddedNotification>);

            var button = (Button)sender;
            
            Person newProfile = null;
            Category category = (Category)button.Tag;
            
            using (var dialog = new UserProfileModifiedDialog(null))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    newProfile = dialog.NewProfile;
                }
            }
            if (newProfile != null)
            {
                this.AddUserWithCategory(newProfile, category);
            }
            UserRegisteryForm.EventAggregator.RemoveSubscriber(this as ISubscriber<UserProfileAddedNotification>);
        }

        private void userModifyButton_Click(object sender, EventArgs e)
        {
            UserRegisteryForm.EventAggregator.AddSubscriber(this as ISubscriber<UserProfileModifiedNotification>);

            var button = (Button)sender;
            var profile_data = ((Person, Category))button.Tag;

            Person oldProfle = profile_data.Item1;
            Person newProfile = null;
            Category category = profile_data.Item2;

            using (var dialog = new UserProfileModifiedDialog(oldProfle))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    newProfile = dialog.NewProfile;
                }
            }
            if (newProfile != null) 
            {
                this.ModifyUserWithCategory(oldProfle, newProfile, category);
            }
            UserRegisteryForm.EventAggregator.RemoveSubscriber(this as ISubscriber<UserProfileModifiedNotification>);
        }

        private void UserRegisteryForm_Load(object sender, EventArgs e)
        {
            UserRegisteryForm.EventAggregator.AddSubscriber(this as ISubscriber<UserProfileSelectedNotification>);
            UserRegisteryForm.EventAggregator.AddSubscriber(this as ISubscriber<CategorySelectedNotification>);
        }

        private void UserRegisteryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserRegisteryForm.EventAggregator.RemoveSubscriber(this as ISubscriber<UserProfileSelectedNotification>);
            UserRegisteryForm.EventAggregator.RemoveSubscriber(this as ISubscriber<CategorySelectedNotification>);
        }
        #endregion

    }

    #region Program Logic classes
    public static class CategoryMethods
    {
        private const string StudentsStringRepr = "Studenci";
        private const string LecturersStringRepr = "Wykładowcy";

        public static string IntoString(this Category category)
        {
            switch (category) {
                case Category.Stuents:   return StudentsStringRepr;
                case Category.Lecturers: return LecturersStringRepr;
                default: throw new ArgumentException("Invalid enum vartiant.");
            }
        }
        public static Category IntoCategory(this string category)
        {
            switch (category)
            {
                case StudentsStringRepr: return Category.Stuents;
                case LecturersStringRepr: return Category.Lecturers;
                default: throw new ArgumentException("Invalid Category representation.");
            }
        }
    }

    public enum Category
    {
        Stuents,
        Lecturers,
    }

    public class Person
    {
        public Person(string Name, string Surname, string Address, DateTime DateOfBirth)
        {
            this.Surname = Surname;
            this.Name = Name;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
        }
        public string FullName()
        {
            return $"{this.Name} {this.Surname}";
        }

        public string Surname { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
    #endregion

    #region EventAggregator Implementaion
    public interface ISubscriber<T>
    {
        void HandleEvent(T EventInfo);
    }

    public interface IEventAggregator
    {
        void AddSubscriber<T>(ISubscriber<T> Subscriber);
        void RemoveSubscriber<T>(ISubscriber<T> Subscriber);
        void Publish<T>(T Event);
    }

    public class EventAggregator : IEventAggregator
    {
        private Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();
        public void AddSubscriber<T>(ISubscriber<T> Subscriber)
        {
            if (!_subscribers.ContainsKey(typeof(T)))
                _subscribers.Add(typeof(T), new List<object>());
            _subscribers[typeof(T)].Add(Subscriber);
        }

        public void Publish<T>(T EventInfo)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                foreach (ISubscriber<T> subscriber in this._subscribers[typeof(T)].OfType<ISubscriber<T>>())
                {
                    subscriber.HandleEvent(EventInfo);
                }
            }
        }

        public void RemoveSubscriber<T>(ISubscriber<T> Subscriber)
        {
            if (_subscribers.ContainsKey(typeof(T)))
            {
                this._subscribers[typeof(T)].Remove(Subscriber);
            }
        }
    }

    public class CategorySelectedNotification
    {
        public Category SelectedCategory { get; set; }
        public CategorySelectedNotification(Category category) {
            this.SelectedCategory = category;
        }
    }

    public class UserProfileSelectedNotification
    {
        public Person SelectedProfile { get; set; }
        public Category ProfileCategory { get; set; }
        public UserProfileSelectedNotification(Person profile, Category category)
        {
            this.SelectedProfile = profile;
            this.ProfileCategory = category;
        }
    }

    public class UserProfileAddedNotification
    {
        public Person AddedProfile { get; set; }
        public Category ProfileCategory { get; set; }
        public UserProfileAddedNotification(Person profile, Category category)
        {
            this.AddedProfile = profile;
            this.ProfileCategory = category;
        }

    }

    public class UserProfileModifiedNotification
    {
        public Person OldProfile { get; set; }
        public Person NewProfile { get; set; }
        public Category ProfileCategory { get; set; }
        public UserProfileModifiedNotification(Person old_profile, Person new_profile, Category category)
        {
            this.OldProfile = old_profile;
            this.NewProfile = new_profile;
            this.ProfileCategory = category;
        }
    }
    #endregion
}
