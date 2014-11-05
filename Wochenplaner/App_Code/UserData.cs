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
        private readonly string id;
        public string Id { get { return this.id; } set; }
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        private string password;
        public string Password { get { return this.password; } set { this.password = value; } }
        #endregion

        /// <summary>
        /// Constructor for variable id (empty user)
        /// </summary>
        /// <param name="_id">The id of the user</param>
        public UserData(string _id) {
            this.id = _id;
        }

        /// <summary>
        /// Constructor for 2 variables
        /// </summary>
        /// <param name="_id">The id of the user</param>
        /// <param name="_name">The name of the user</param>
        public UserData(string _id, string _name) {
            this.id = _id;
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
    }
}