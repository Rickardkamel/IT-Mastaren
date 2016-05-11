using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using ITMApp_WebAPI.Ldap;

namespace asdsad
{
    public class LdapAuthentication : ConfigFile
    {
        private static string _domain;
        private DirectoryEntry conn;
        private UserPrincipal user;

        /// <summary>
        /// Inits some values
        /// </summary>
        public LdapAuthentication()
        {
            _domain = "itm.local";
        }

        /// <summary>
        /// Connect to the AD
        /// </summary>
        /// <param name="user">The username</param>
        /// <param name="pass">The password</param>
        /// <param name="group">Which group/member to search after</param>
        /// <returns>Returns a msg-string - if it contains 'Correct:', everything went well and the AD-path will be send
        /// If not, then a error-msg will be displayed</returns>
        public string[] connectToAD(string username, string password, string group)
        {
             string[] strArr = new string[6];
            strArr[0] = "Error";


            try
            {
                /* Connects to the AD */
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, _domain, username, password))
                {
                    try
                    {
                        /* Check if connection succeeded */
                        if (pc.ConnectedServer == null)
                        {
                            strArr[1] = errorTextNoConToAD;
                            return strArr;
                        }
                    }
                    catch (Exception e)
                    {
                        #region Exception
                        if (e.Message.Contains("Logon failure: unknown user name or bad password."))
                            strArr[1] = errorTextNoConToAD + e.Message;
                        else
                            strArr[1] = errorTextNoConToAD;


                        string email = UserProperties.EMAILADDRESS;

                        try
                        {
                            /* Wants to find the email of the user */
                            conn = new DirectoryEntry(this.getRootLdapPath());
                            //Search in the AD
                            using (DirectorySearcher searcher = new DirectorySearcher())
                            {
                                searcher.Filter = "samAccountName=" + username; //Want this username
                                searcher.PropertiesToLoad.Add(email); //And only the email

                                SearchResult result = searcher.FindOne(); //Find the user, and grab some info

                                if (result != null) //Found the user
                                {
                                    if (result.Properties.Contains(email))    //Check if the user has a email
                                        strArr[2] = Convert.ToString(result.Properties[email][0]);    //Save the email
                                }
                            }
                        }
                        catch { }
                        return strArr;
                        #endregion
                    }

                    //Gets the user-object from the AD
                    user = UserPrincipal.FindByIdentity(pc, username);
                    if (user != null)   //Found the user
                    {
                        if (user.Enabled == true)   //If the user is active
                        {
                            PrincipalSearchResult<Principal> groups = user.GetGroups(pc); //Get all groups he/she is memberOf

                            //Loop over all groups
                            foreach (Principal p in groups)
                            {
                                //Check if the user is member of the specified group
                                if (p is GroupPrincipal && p.ToString().Equals(group, StringComparison.OrdinalIgnoreCase))
                                {
                                    strArr[0] = "Correct";
                                    strArr[1] = user.DistinguishedName;
                                    strArr[3] = user.DisplayName;
                                    strArr[4] = user.EmailAddress;
                                    strArr[5] = user.SamAccountName;

                                    return strArr;
                                }
                            }
                            strArr[1] = username + " " + errorTextUserNotMember;
                        }
                        else
                            strArr[1] = username + errorTextAccountDisabled;
                    }
                    else
                        strArr[1] = errorTextFindNoUser + " " + username;
                    //// validate the credentials(TAKE TO LONG TO VALIDATE)
                    //bool isValid = pc.ValidateCredentials(user, pass);
                }
            }
            catch { strArr[1] = "Something went wrong"; }
            return strArr;
        }

        /// <summary>
        /// Checks if a user exists and are not disable
        /// </summary>
        /// <param name="userLdapPath">Path of the user in the AD</param>
        /// <param name="username">Name of the user to check the status of</param>
        /// <returns>Returns True - if the user is disable/not active | Returns false - if everything is ok</returns>
        public bool IsUserDisable(string userLdapPath, string username)
        {
            try
            {
                conn = new DirectoryEntry("LDAP://" + userLdapPath);

                //Checks if user exists
                if (conn.NativeGuid == null) return true;

                //Search in the AD
                using (DirectorySearcher searcher = new DirectorySearcher(conn))
                {
                    searcher.Filter = "samAccountName=" + username; //Want this username
                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("userAccountControl");

                    SearchResult result = searcher.FindOne(); //Find the user, and grab some info

                    int userAccountControl = Convert.ToInt32(result.Properties["userAccountControl"][0]);
                    string samAccountName = Convert.ToString(result.Properties["samAccountName"][0]);

                    //Checks here if the user is disabled or not
                    bool disabled = ((userAccountControl & 2) > 0);
                    return disabled;
                }
            }
            catch { return true; }
        }

        /// <summary>
        /// Gets the real name of a user
        /// </summary>
        /// <param name="userLdapPath">The path of the user in the AD</param>
        /// <param name="username">Name of the user to get the name of</param>
        /// <returns>Returns a string with th users name</returns>
        /// 
        /// 
        /// TODO Check This, gets an error here. 
        public string getRealName(string userLdapPath, string username)
        {
            string name = string.Empty;
            try
            {
                conn = new DirectoryEntry("LDAP://" + userLdapPath);
                if (conn.Properties.Contains(UserProperties.NAME))  //If the user has a name
                {
                    //Send it back
                    return conn.Properties[UserProperties.NAME][0].ToString();
                }
            }
            catch { }
            return name;
        }

        /// <summary>
        /// Gets the path to the root of the AD
        /// </summary>
        /// <returns>Returns a string with the root of the AD-path</returns>
        private string getRootLdapPath()
        {
            string[] dcArr = _domain.Split('.');    //Splits the domain
            string ldapPath = "LDAP://";

            //Ex. "LDAP://" + "DC=itm,DC=local"
            for (int i = 0; i < dcArr.Length; i++)
            {
                if (dcArr.Length - 1 == i)    //Last word doesn't need a ','
                    ldapPath += "DC=" + dcArr[i];
                else
                    ldapPath += "DC=" + dcArr[i] + ",";
            }
            return ldapPath;
        }
    }
}
