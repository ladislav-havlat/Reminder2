using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LH.Reminder2.Server.Data;

namespace LH.Reminder2.Server.Auth
{
    public class Rem2MembershipProvider : MembershipProvider
    {
        /// <summary>
        /// Validates an user against the database.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">User password.</param>
        /// <returns>True if the user was succesfully validated, false otherwise.</returns>
        public override bool ValidateUser(string username, string password)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where (u.UserName == username) && 
                              (u.Password == password)
                        select u;
            return query.FirstOrDefault() != null;
        }

        /// <summary>
        /// Gets an MembershipUser from the database, given his username.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="userIsOnline"></param>
        /// <returns>The fetched MemebershipUser.</returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where u.UserName == username
                        select u;

            User found = query.FirstOrDefault();
            if (found != null)
                return new Rem2MembershipUser(this.Name, found);
            else
                return null;
        }

        /// <summary>
        /// Gets a MembershipUser from the database given his database ID.
        /// </summary>
        /// <param name="providerUserKey">ID of the user in the DB.</param>
        /// <param name="userIsOnline"></param>
        /// <returns>The fetched MembershipUser.</returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (!(providerUserKey is int))
                return null;

            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where u.idUser == (int)providerUserKey
                        select u;

            User found = query.FirstOrDefault();
            if (found != null)
                return new Rem2MembershipUser(this.Name, found);
            else
                return null;
        }

        /// <summary>
        /// Gats an user's name from the database given his e-mail address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override string GetUserNameByEmail(string email)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where u.Email == email
                        select u.UserName;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Changes password for an user.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <param name="oldPassword">Old password.</param>
        /// <param name="newPassword">New password.</param>
        /// <returns>True if the operation was successful, false otherwise.</returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where (u.UserName == username) &&
                              (u.Password == oldPassword)
                        select u;

            User found = query.FirstOrDefault();
            if (found == null)
                return false;
            try
            {
                found.Password = newPassword;
                ctx.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an user's password from the database.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <param name="answer"></param>
        /// <returns>User's password.</returns>
        public override string GetPassword(string username, string answer)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where u.UserName == username
                        select u.Password;
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Creates a new user and inserts him into the DB.
        /// </summary>
        /// <param name="username">Name of the user.</param>
        /// <param name="password">User password.</param>
        /// <param name="email">E-mail address.</param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey">User's ID in the DB. Leave null.</param>
        /// <param name="status">User creation status.</param>
        /// <returns>A new Membership user if successful, null otherwise.</returns>
        public override MembershipUser CreateUser(string username, string password, string email, 
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, 
            out MembershipCreateStatus status)
        {
            return CreateUser(username, password, email, string.Empty, string.Empty, out status);
        }

        /// <summary>
        /// Creates a new user and inserts him into the DB.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">User password.</param>
        /// <param name="email">E-mail address.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="status">User creation status.</param>
        /// <returns>A new Membership user if successful, null otherwise.</returns>
        public Rem2MembershipUser CreateUser(string userName, string password, string email,
            string firstName, string lastName, out MembershipCreateStatus status)
        {
            Reminder2DataContext ctx = new Reminder2DataContext();

            //check user name
            if (string.IsNullOrEmpty(userName))
            {
                status = MembershipCreateStatus.InvalidUserName;
                return null;
            }

            //check if the user already exists
            var exists = from User u in ctx.Users
                         where u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
                         select new object();
            if (exists.Count() > 0)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            Rem2MembershipUser memUser = null;
            try
            {
                User dbUser = new User()
                {
                    UserName = userName,
                    Password = password,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };
                ctx.Users.InsertOnSubmit(dbUser);
                ctx.SubmitChanges();
                memUser = new Rem2MembershipUser(this.Name, dbUser);
            }
            catch
            {
                status = MembershipCreateStatus.ProviderError;
                return null;
            }

            status = MembershipCreateStatus.Success;
            return memUser;
        }

        /// <summary>
        /// Updates an user's data in the database.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        public override void UpdateUser(MembershipUser user)
        {
            Rem2MembershipUser memUser = user as Rem2MembershipUser;
            if (memUser == null)
                throw new ArgumentException("The supplied Membership user is not compatible with the DB.");

            Reminder2DataContext ctx = new Reminder2DataContext();
            var query = from User u in ctx.Users
                        where u.UserName == user.UserName
                        select u;
            User found = query.FirstOrDefault();
            if (found == null)
                return;

            found.Email = memUser.Email;
            found.FirstName = memUser.FirstName;
            found.LastName = memUser.LastName;
            ctx.SubmitChanges();
        }

        #region Not implemented
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// Represents an user in Membership system.
    /// </summary>
    public class Rem2MembershipUser : MembershipUser
    {
        private string firstName;
        private string lastName;

        /// <summary>
        /// Initializes a new instance of Rem2MembershipUser.
        /// </summary>
        public Rem2MembershipUser() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of Rem2MembershipUser from a DB user.
        /// </summary>
        /// <param name="providerName">The System.Web.Security.MembershipUser.ProviderName string for the membership user.</param>
        /// <param name="u">The DB user.</param>
        public Rem2MembershipUser(string providerName, User u) : 
            this(providerName, u.UserName, u.idUser, u.Email, null, null, true, false, default(DateTime),
            default(DateTime), default(DateTime), default(DateTime), default(DateTime), u.FirstName, u.LastName)
        {
        }

        /// <summary>
        /// Creates a new membership user object with the specified property values.
        /// </summary>
        /// <param name="providerName">The System.Web.Security.MembershipUser.ProviderName string for the membership user.</param>
        /// <param name="name">The System.Web.Security.MembershipUser.UserName string for the membership user.</param>
        /// <param name="providerUserKey">The System.Web.Security.MembershipUser.ProviderUserKey identifier for the membership user.</param>
        /// <param name="email">The System.Web.Security.MembershipUser.Email string for the membership user.</param>
        /// <param name="passwordQuestion">The System.Web.Security.MembershipUser.PasswordQuestion string for the membership user.</param>
        /// <param name="comment">The System.Web.Security.MembershipUser.Comment string for the membership user.</param>
        /// <param name="isApproved">The System.Web.Security.MembershipUser.IsApproved value for the membership user.</param>
        /// <param name="isLockedOut">true to lock out the membership user; otherwise, false.</param>
        /// <param name="creationDate">The System.Web.Security.MembershipUser.CreationDateSystem.DateTime object for the membership user.</param>
        /// <param name="lastLoginDate">The System.Web.Security.MembershipUser.LastLoginDateSystem.DateTime object for the membership user.</param>
        /// <param name="lastActivityDate">The System.Web.Security.MembershipUser.LastActivityDateSystem.DateTime object for the membership user.</param>
        /// <param name="lastPasswordChangedDate">The System.Web.Security.MembershipUser.LastPasswordChangedDateSystem.DateTime object for the membership user.</param>
        /// <param name="lastLockoutDate">The System.Web.Security.MembershipUser.LastLockoutDateSystem.DateTime object for the membership user.</param>
        /// <param name="firstName">First name of the membership user.</param>
        /// <param name="lastName">Last name of the membership user.</param>
        public Rem2MembershipUser(string providerName, string name, object providerUserKey, string email, 
            string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, 
            DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, 
            DateTime lastLockoutDate, string firstName, string lastName) :

            base(providerName, name, providerUserKey, email, passwordQuestion, comment, isApproved,
            isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate,
            lastLockoutDate)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        #region Public properties
        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        #endregion

        /// <summary>
        /// Converts the Rem2Membership user to DB user.
        /// </summary>
        /// <returns></returns>
        public User ToDBUser()
        {
            User u = new User()
            {
                UserName = this.UserName,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            if (ProviderUserKey is int)
                u.idUser = (int)ProviderUserKey;

            return u;
        }
    }
}
