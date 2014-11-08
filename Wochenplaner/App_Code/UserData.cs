using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wochenplaner.App_Code {
    public class UserData {
        #region Variables
        /// <summary>
        /// Declaration of variables
        /// </summary>
        private string id = "randomUser";
        public string Id { get { return this.id; } set { this.id = value; } }
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        private string password;
        public string Password { get { return this.password; } set { this.password = value; } }
        #endregion

        /// <summary>
        /// Empty constructor creates random user id (empty user)
        /// </summary>
        /// <param name="_id">The id of the user</param>
        public UserData() {
            this.id = createUserId();
        }

        /// <summary>
        /// Constructor for variable name
        /// </summary>
        /// <param name="_id">The id of the user</param>
        /// <param name="_name">The name of the user</param>
        public UserData(string _name) {
            this.id = createUserId();
            this.name = _name;
        }

        /// <summary>
        /// Constructor for 3 variables
        /// </summary>
        /// <param name="_id">The id of the user</param>
        /// <param name="_name">The name of the user</param>
        /// <param name="_pass">The password from the user</param>
        [Obsolete("Full login is not intended at this moment")]
        public UserData(string _id, string _name, string _pass) {
            this.id = _id;
            this.name = _name;
            this.password = _pass;
        }

        /// <summary>
        /// Creates a random string to be used as the user id
        /// </summary>
        /// <returns>user id</returns>
        private string createUserId() {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 10)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}