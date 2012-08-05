using System;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration.Provider;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using NHibernate;
using Sample.Data;
using System.Linq;
using NHibernate.Linq;

namespace Sample.Components
{
    public class SampleMembershipProvider : MembershipProvider
    {
        Database db;
        ISessionFactory factory;

        private int newPasswordLength = 8;
        private string connectionString;
        private string applicationName;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private int maxInvalidPasswordAttempts;
        private int passwordAttemptWindow;
        private MembershipPasswordFormat passwordFormat;
        private int minRequiredNonAlphanumericCharacters;
        private int minRequiredPasswordLength;
        private string passwordStrengthRegularExpression;

        private MachineKeySection machineKey; //Used when determining encryption key values.

        #region properties

        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return enablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return minRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return passwordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return passwordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return requiresUniqueEmail; }
        }
        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {


            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (name == null || name.Length == 0)
            {
                name = "NHMembershipProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "How Do I: Sample Membership provider");
            }

            //Initialize the abstract base class.
            base.Initialize(name, config);

            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if ((ConnectionStringSettings == null) || (ConnectionStringSettings.ConnectionString.Trim() == String.Empty))
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = ConnectionStringSettings.ConnectionString;

            //Get encryption and decryption key information from the configuration.
            System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            if (machineKey.ValidationKey.Contains("AutoGenerate"))
            {
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                {
                    throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
                }
            }


        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            bool result = false;
            User user = null;
            using (var session = Database.OpenSession())
            {
                var q = from x in session.Query<User>()
                        where x.UserName == username
                        select x;
                if (q.Count() > 0)
                {
                    user = q.First();
                }
                if (user.Password== EncodePassword(oldPassword))
                {
                    user.Password = EncodePassword(newPassword);
                    user.LastPasswordChangedDate = DateTime.Now;

                    session.SaveOrUpdate(user);

                    result = true;
                }
            }

            return result;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string
            newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey,
            out MembershipCreateStatus status)
        {
            try
            {
                User user1 = new User();
                user1.UserName = username;
                user1.Password = EncodePassword(password);
                user1.Email = email;
                user1.PasswordQuestion = passwordQuestion;
                user1.PasswordAnswer = passwordAnswer;
                user1.isApproved = isApproved;
                user1.isOnline = false;
                user1.isLockedOut = false;

                DateTime now = DateTime.Now;

                user1.CreationDate = now;
                user1.LastActivityDate = now;
                user1.LastLockedOutDate = now;
                user1.LastLoginDate = now;
                user1.LastPasswordChangedDate = now;

                using (var session = Database.OpenSession())
                {
                    session.Save(user1);
                }

                status = MembershipCreateStatus.Success;
            }
            catch (Exception)
            {
                status = MembershipCreateStatus.ProviderError;
            }
            return GetUser(username, false);
        }



        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
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

        public override string GetPassword(string username, string answer)
        {
            string result = string.Empty;
            using (var session = Database.OpenSession())
            {
                var q = from x in session.Query<User>()
                        where x.UserName == username
                        select x;
                if (q.Count() != 0)
                {
                    result = q.First().Password;
                }
            }
            return result;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            User user = null;
            using (var session = Database.OpenSession())
            {
                var q = from x in session.Query<User>()
                        where x.UserName == username
                        select x;
                if (q.Count()>0)
                {
                    user = q.First();
                }
            }

            return ToMembershipUser(user);
            //            User user1 = db.Users.Where(x => x.UserName == username).FirstOrDefault();
            //
            //            if (user1 == null)
            //            {
            //                return null;
            //                //throw membership exception                
            //            }
            //            MembershipUser muser = ToMembershipUser(user1);
            //            return muser;
            //throw new NotImplementedException();
        }



        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            int userId = Convert.ToInt32(providerUserKey);
            User user = null;
            using (var session = Database.OpenSession())
            {
                var q = from x in session.Query<User>()
                        where x.UserId == userId
                        select x;
                if (q.Count() > 0)
                {
                    user = q.First();
                }
            }
            //            User user1 = db.Users.Where(x => x.UserId == userId).FirstOrDefault();
            //
            MembershipUser muser = ToMembershipUser(user);
            return muser;

            //throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            string result = string.Empty;
            using (ISession session = Database.OpenSession())
            {
                var q = from user in session.Query<User>()
                        where user.Email == email
                        select user.UserName;
                if (q.Count() > 0)
                {
                    result = q.FirstOrDefault();
                }
            }
            //            return db.Users.Where(x => x.Email == email).Select(x => x.UserName).FirstOrDefault();
            return result;
        }



        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            bool result = false;
            
            using (var session = Database.OpenSession())
            {
                var q = from x in session.Query<User>()
                        where x.UserName == username && x.Password==EncodePassword(password)
                        select x;
                int cnt = q.Count();
                if ( cnt> 0)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Encode password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Encoded password.</returns>
        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                        Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                        Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        #region utilities

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// Convert user class to membership user
        /// </summary>
        /// <param name="user1"></param>
        /// <returns></returns>
        private MembershipUser ToMembershipUser(User user1)
        {
            MembershipUser muser = new MembershipUser(this.Name, user1.UserName, user1.UserId, user1.Email,
                user1.PasswordQuestion, user1.Comment, user1.isApproved.Value, user1.isLockedOut.Value, user1.CreationDate.Value,
                user1.LastLoginDate.Value, user1.LastActivityDate.Value, user1.LastPasswordChangedDate.Value, user1.LastLockedOutDate.Value);
            return muser;
        }
        #endregion
    }

}


