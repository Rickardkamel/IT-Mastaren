using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Configuration;

namespace ITMApp_WebAPI.Ldap
{
    public class ConfigFile
    {

        #region Consts

        private const string keyTempAdmin = "tempAdmin";
        private const string keyTempPass = "tempPass";
        public string[] AdwageCodeArr = { "0", "1" };
        public string appSet;
        public string[] custPrGrArr = { "Price1", "Price2", "Price3", string.Empty };
        public string[] debitCodeArr = { "0", "1" };
        public string dtFormat = "d MMMM, yyyy";
        public string infoTextPrice = "Price Group";
        public string internOrderDeli = "???";
        public string keyAdwageFalse = "adwageFalse";
        public string keyAdwageTrue = "adwageTrue";

        //DB
        public string keyDBDatabaseName = "DBDatabaseName";
        public string keyDBPassword = "DBPassword";
        public string keyDBServer = "DBServer";
        public string keyDBUserId = "DBUserId";
        public string keyDebit = "Debit";
        public string keyDebitEjCode = "debitEjCode";
        public string keyDebiteraCode = "debiteraCode";
        public string keyEjDebit = "EjDebit";
        public string keyMemo = "memoPath";
        public string keyNCacset = "normalCacsetActivity";
        public string keyNInvo = "invoReg";
        public string keyNPri = "normalPriActivity";
        public string keyOCacset = "otherCacsetActivity";
        public string keyOInvo = "invoUtl";
        public string keyOPri = "otherPriActivity";
        public string keyTaxa = "taxaCode";
        public string keyactivityCode = "activityCode";
        public string keycustPrG2 = "custprg2";
        public string keycustPrG3 = "custprg3";
        public string keycustPrGr = "custprgr";

        //Mail

        public string keydomainAD = "domainAD";
        public string keyfin = "Fin";
        public string keyinternOrder = "InternOrder";
        public string keylang = "lang";
        public string keyloginAttempt = "loginAttempt";
        public string keymailFrom = "mailFrom";
        public string keymailText = "mailText";
        public string keymemberAdmin = "memberAdmin";
        public string keymemberDefault = "memberDefault";
        public string keyorderFakCode = "orderFakturaCode";

        public string keyprodPrG2 = "prodprg2";
        public string keyprodPrG3 = "prodprg3";
        public string keyprodPrGr = "prodprgr";

        //If edit this line => change this function db.getRightRInt()
        public string keyr1 = "r1";
        public string keyr10 = "r10";
        public string keyr11 = "r11";
        public string keyr12 = "r12";
        public string keyr2 = "r2";
        public string keyr3 = "r3";
        public string keyr4 = "r4";
        public string keyr5 = "r5";
        public string keyr6 = "r6";

        //If edit this line => change this function db.getRightRString()
        public string keyr7 = "r7";
        public string keyr8 = "r8";
        public string keyr9 = "r9";
        public string keysmtpPassword = "smtpPassword";
        public string keysmtpPort = "smtpPort";
        public string keysmtpSSL = "smtpSSL";
        public string keysmtpServer = "smtpServer";
        public string keysmtpUserName = "smtpUserName";
        public string keyyearLifeOfTheCookie = "yearLifeOfTheCookie";
        public string[] prodPrGrArr = { "Debit", "Utlagg", "Activity", string.Empty };
        public string[] rPrGrIntArr = { "Locale", "Contract", string.Empty };
        public string[] rPrGrStringArr = { "Service", "Project", string.Empty };
        public string varDate = "%date%";
        public string varDelim = "#?#";
        public string varDynamic = "DYNC!";
        public string varEmpty = string.Empty;
        public string varIP = "%IPaddress%";
        public string varOffert = "Offert";
        public string varUtlagg = "Utlägg";
        public string varWebNm = "%webbName%";

        #endregion

        #region Public properties

        /* Domain name */
        public string domainAD { get; set; }

        /* Paths for the regular users */
        public string loginPage { get; private set; }
        public string userPage { get; private set; }
        public string pcUserPage { get; private set; }
        public string timePage { get; private set; }

        /* Paths for the admin */
        public string adminLoginPage { get; private set; }
        public string adminPage { get; private set; }

        /* Path to the config-file */
        public string configPath { get; private set; }

        public int loginAttempt { get; set; }
        public int yearLifeOfTheCookie { get; set; }

        /* DB - Info */
        public string dbServer { get; set; }
        public string dbName { get; set; }
        public string dbUser { get; set; }
        public string dbPass { get; set; }

        /* Mail - Info */
        public string mailServer { get; set; }
        public int mailPort { get; set; }
        public string mailFrom { get; set; }
        public string mailUser { get; set; }
        public string mailPass { get; set; }
        public bool mailSSL { get; set; }

        /* Error text */
        public string errorTextNoConToAD { get; private set; }
        public string errorTextUserNotMember { get; private set; }
        public string errorTextFindNoUser { get; private set; }
        public string errorTextAccountDisabled { get; private set; }
        public string errorTextDBUserExistsNot { get; private set; }
        public string errorTextWrongUserPass { get; private set; }
        public string errorTextTempCred { get; private set; }
        public string errorTextNoConToDB { get; private set; }

        /* Member groups */
        public string memberDefault { get; set; }
        public string memberAdmin { get; set; }

        public int lang { get; set; }

        public int adWTrue { get; private set; }
        public int adWFalse { get; private set; }

        public int nPriAct { get; set; }
        public int oPriAct { get; set; }

        //Debit or not for activities
        public string debiteraActivity { get; set; }
        public string debitEjActivity { get; set; }

        //The different code if debit or not
        public int debiteraCode { get; private set; }
        public int debitEjCode { get; private set; }

        public int nCacset { get; set; }
        public int oCacset { get; set; }

        public int invoUtl { get; set; }
        public int invoReg { get; set; }

        public int fin { get; private set; }

        public int activityID { get; set; }

        public string tempAdmin { get; private set; }
        public string tempPass { get; private set; }

        public int orderFakturaID { get; set; }

        public string prodPrGr { get; private set; }
        public string prodPrG2 { get; private set; }
        public string prodPrG3 { get; private set; }

        public string r1 { get; private set; }
        public string r2 { get; private set; }
        public string r3 { get; private set; }
        public string r4 { get; private set; }
        public string r5 { get; private set; }
        public string r6 { get; private set; }

        public string r7 { get; private set; }
        public string r8 { get; private set; }
        public string r9 { get; private set; }
        public string r10 { get; private set; }
        public string r11 { get; private set; }
        public string r12 { get; private set; }

        public string custPrGr { get; private set; }
        public string custPrG2 { get; private set; }
        public string custPrG3 { get; private set; }

        public string internOrder { get; private set; }

        public string mailText { get; private set; }

        public int taxaID { get; set; }
        public string memoPath { get; set; }

        #endregion

        /// <summary>
        ///     Constructor, Init some values
        /// </summary>
        public ConfigFile()
        {
            #region Sets variables

            /* Name of the domain */
            domainAD = ConfigurationManager.AppSettings[keydomainAD];

            /* Users */
            loginPage = ConfigurationManager.AppSettings["loginPage"];
            userPage = ConfigurationManager.AppSettings["userPage"];
            pcUserPage = ConfigurationManager.AppSettings["pcUserPage"];
            timePage = ConfigurationManager.AppSettings["timePage"];

            /* Admin */
            adminLoginPage = ConfigurationManager.AppSettings["adminLoginPage"];
            adminPage = ConfigurationManager.AppSettings["adminPage"];

            //Path to the config
            configPath = ConfigurationManager.AppSettings["configPath"];

            //Paths to configFile-Nodes
            appSet = ConfigurationManager.AppSettings["appSet"];

            loginAttempt = Convert.ToInt32(ConfigurationManager.AppSettings[keyloginAttempt]);

            //DB
            dbServer = ConfigurationManager.AppSettings[keyDBServer];
            dbName = ConfigurationManager.AppSettings[keyDBDatabaseName];
            dbUser = ConfigurationManager.AppSettings[keyDBUserId];
            dbPass = ConfigurationManager.AppSettings[keyDBPassword];

            //Mail
            mailServer = ConfigurationManager.AppSettings[keysmtpServer];
            mailPort = Convert.ToInt32(ConfigurationManager.AppSettings[keysmtpPort]);
            mailFrom = ConfigurationManager.AppSettings[keymailFrom];
            mailUser = ConfigurationManager.AppSettings[keysmtpUserName];
            mailPass = ConfigurationManager.AppSettings[keysmtpPassword];
            mailSSL = Convert.ToBoolean(ConfigurationManager.AppSettings[keysmtpSSL]);

            //Error text
            errorTextNoConToAD = ConfigurationManager.AppSettings["errorTextNoConToAD"];
            errorTextUserNotMember = ConfigurationManager.AppSettings["errorTextUserNotMember"];
            errorTextFindNoUser = ConfigurationManager.AppSettings["errorTextFindNoUser"];
            errorTextAccountDisabled = ConfigurationManager.AppSettings["errorTextAccountDisabled"];
            errorTextDBUserExistsNot = ConfigurationManager.AppSettings["errorTextDBUserExistsNot"];
            errorTextWrongUserPass = ConfigurationManager.AppSettings["errorTextWrongUserPass"];
            errorTextTempCred = ConfigurationManager.AppSettings["errorTextTempCred"];
            errorTextNoConToDB = ConfigurationManager.AppSettings["errorTextNoConToDB"];

            yearLifeOfTheCookie = Convert.ToInt32(ConfigurationManager.AppSettings[keyyearLifeOfTheCookie]);

            //Member groups
            memberDefault = ConfigurationManager.AppSettings[keymemberDefault];
            memberAdmin = ConfigurationManager.AppSettings[keymemberAdmin];

            lang = Convert.ToInt32(ConfigurationManager.AppSettings[keylang]);

            activityID = Convert.ToInt32(ConfigurationManager.AppSettings[keyactivityCode]);

            debiteraActivity = ConfigurationManager.AppSettings[keyDebit + activityID];
            debitEjActivity = ConfigurationManager.AppSettings[keyEjDebit + activityID];

            debiteraCode = Convert.ToInt32(ConfigurationManager.AppSettings[keyDebiteraCode]);
            debitEjCode = Convert.ToInt32(ConfigurationManager.AppSettings[keyDebitEjCode]);

            adWTrue = Convert.ToInt32(ConfigurationManager.AppSettings[keyAdwageTrue]);
            adWFalse = Convert.ToInt32(ConfigurationManager.AppSettings[keyAdwageFalse]);

            nPriAct = Convert.ToInt32(ConfigurationManager.AppSettings[keyNPri]);
            oPriAct = Convert.ToInt32(ConfigurationManager.AppSettings[keyOPri]);

            nCacset = Convert.ToInt32(ConfigurationManager.AppSettings[keyNCacset]);
            oCacset = Convert.ToInt32(ConfigurationManager.AppSettings[keyOCacset]);

            invoUtl = Convert.ToInt32(ConfigurationManager.AppSettings[keyOInvo]); //Utlagg
            invoReg = Convert.ToInt32(ConfigurationManager.AppSettings[keyNInvo]); //Regular

            fin = Convert.ToInt32(ConfigurationManager.AppSettings[keyfin]); //Regular

            tempAdmin = ConfigurationManager.AppSettings[keyTempAdmin];
            tempPass = ConfigurationManager.AppSettings[keyTempPass];

            orderFakturaID = Convert.ToInt32(ConfigurationManager.AppSettings[keyorderFakCode]);

            prodPrGr = ConfigurationManager.AppSettings[keyprodPrGr];
            prodPrG2 = ConfigurationManager.AppSettings[keyprodPrG2];
            prodPrG3 = ConfigurationManager.AppSettings[keyprodPrG3];

            r1 = ConfigurationManager.AppSettings[keyr1];
            r2 = ConfigurationManager.AppSettings[keyr2];
            r3 = ConfigurationManager.AppSettings[keyr3];
            r4 = ConfigurationManager.AppSettings[keyr4];
            r5 = ConfigurationManager.AppSettings[keyr5];
            r6 = ConfigurationManager.AppSettings[keyr6];

            r7 = ConfigurationManager.AppSettings[keyr7];
            r8 = ConfigurationManager.AppSettings[keyr8];
            r9 = ConfigurationManager.AppSettings[keyr9];
            r10 = ConfigurationManager.AppSettings[keyr10];
            r11 = ConfigurationManager.AppSettings[keyr11];
            r12 = ConfigurationManager.AppSettings[keyr12];

            custPrGr = ConfigurationManager.AppSettings[keycustPrGr];
            custPrG2 = ConfigurationManager.AppSettings[keycustPrG2];
            custPrG3 = ConfigurationManager.AppSettings[keycustPrG3];

            internOrder = ConfigurationManager.AppSettings[keyinternOrder];

            mailText = ConfigurationManager.AppSettings[keymailText];

            taxaID = Convert.ToInt32(ConfigurationManager.AppSettings[keyTaxa]);

            memoPath = ConfigurationManager.AppSettings[keyMemo];

            #endregion
        }


        public string getDebiteraActivityValue(string actID)
        {
            return ConfigurationManager.AppSettings[keyDebit + actID];
        }

        public string getDebitEjActivityValue(string actID)
        {
            return ConfigurationManager.AppSettings[keyEjDebit + actID];
        }

        /// <summary>
        ///     Write something to the config-File
        /// </summary>
        /// <param name="key">The unique key/id name</param>
        /// <param name="value">Value you want to save</param>
        public void writeToConfig(string key, string value)
        {
            Configuration config = loadConfigFile(); //Load configFile
            KeyValueConfigurationElement setting = config.AppSettings.Settings[key]; //Get key

            if (key.Equals(keyDBPassword) || key.Equals(keysmtpPassword) || key.Equals(keydomainAD) ||
                key.Equals(keyDBServer) || key.Equals(keysmtpServer))
            {
                value = encrypt(value);
            }


            if (setting != null)
            {
                config.AppSettings.Settings[key].Value = value; //Update the value
            }
            else
            {
                config.AppSettings.Settings.Add(key, value); //Insert the new value
            }

            config.Save();
            HttpRuntime.UnloadAppDomain(); //Refresh configFile
        }

        /// <summary>
        ///     Deletes the tempCredentials in the ConfigFile for the admin-section
        /// </summary>
        /// <param name="key">tempAdmin</param>
        /// <param name="key2">tempPass</param>
        public void deleteTempAdminCred()
        {
            try
            {
                Configuration config = loadConfigFile();
                config.AppSettings.Settings.Remove(keyTempAdmin); //Delete the keys
                config.AppSettings.Settings.Remove(keyTempPass);
                config.Save();
                HttpRuntime.UnloadAppDomain(); //Refresh configFile
            }
            catch
            {
            }
        }

        /// <summary>
        ///     Read a key from the configFile
        /// </summary>
        /// <param name="keyName">The key you want to read</param>
        /// <returns>Returns a string with the value</returns>
        public string readValueFromConfig(string keyName)
        {
            string value = string.Empty;
            try
            {
                string str = ConfigurationManager.AppSettings[keyName];
                value = !string.IsNullOrEmpty(str) ? str : string.Empty;
            }
            catch
            {
            }

            return value;
        }

        /// <summary>
        ///     Deletes a key from the configFile
        /// </summary>
        /// <param name="keyName">The key you want to delete</param>
        /// <returns>Returns true if deleted else false</returns>
        public bool deleteKeyFromConfig(string keyName)
        {
            bool succeeded = true;
            try
            {
                Configuration config = loadConfigFile();
                config.AppSettings.Settings.Remove(keyName); //Delete the key
                config.Save();
                HttpRuntime.UnloadAppDomain(); //Refresh configFile
            }
            catch
            {
                succeeded = false;
            }

            return succeeded;
        }

        public List<string> getDynamicKeyForShortcuts()
        {
            string value = string.Empty;
            var lista = new List<string>();

            /* Loop in the configFile */
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key.Length >= varDynamic.Length
                    && key.Substring(0, varDynamic.Length).Equals(varDynamic))
                //Checks if the first letters is the dynamic command-name
                {
                    value = ConfigurationManager.AppSettings[key];
                    if (!string.IsNullOrEmpty(value)) //Found a value
                    {
                        lista.Add(value);
                    }
                }
            }

            return lista;
        }


        /// <summary>
        ///     Loads the configFile, so you can use it
        /// </summary>
        private Configuration loadConfigFile()
        {
            return WebConfigurationManager.OpenWebConfiguration("~");
        }


        /// <summary>
        ///     Gets the R-Type from the configFile
        /// </summary>
        /// <param name="rGroup">Which R-Group took look after</param>
        /// <returns>Returns a string with the value</returns>
        protected string getRType(string rGroup)
        {
            return ConfigurationManager.AppSettings[rGroup];
        }

        /// <summary>
        ///     Gets the Prod-Type forom the configFile
        /// </summary>
        /// <param name="prodGroup">Which prod-Group took look after</param>
        /// <returns>Returns a string with the value</returns>
        protected string getProdPrType(string prodGroup)
        {
            return ConfigurationManager.AppSettings.Get(prodGroup);
        }

        /// <summary>
        ///     Gets the prods for a specific activity
        /// </summary>
        /// <param name="activity">The specific activity, to get the prods for</param>
        /// <returns>Returns a sting array with the prodCodes</returns>
        protected string[] getProdCodeList(string activity)
        {
            string str = ConfigurationManager.AppSettings[activity];
            return !string.IsNullOrEmpty(str) ? str.Split(',') : new string[0];
        }

        /// <summary>
        ///     Gets the customer-PriceType from the configFile
        /// </summary>
        /// <param name="custGroup">Which customer-Group to look after</param>
        /// <returns>Returns a string with the value</returns>
        protected string getCustPrType(string custGroup)
        {
            return ConfigurationManager.AppSettings[custGroup];
        }

        #region Encrypt & Decrypt message

        /// <summary>
        ///     Encrypt a message so a human can't read it
        /// </summary>
        /// <param name="data">The message to encrypt</param>
        /// <returns>Returns the encrypted message</returns>
        public string encrypt(string data)
        {
            string encryptedData;
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, data);
                encryptedData = Convert.ToBase64String(ms.ToArray());

                ms.Close();
            }
            return encryptedData; //Send the decrypted message
        }

        /// <summary>
        ///     Decrypts the message so a human can read it
        /// </summary>
        /// <param name="data">Decrypt the encrypted message</param>
        /// <returns>Returns the decrypted message</returns>
        public string decrypt(string data)
        {
            string decryptedData = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                byte[] convertString = Convert.FromBase64String(data);
                using (var ms = new MemoryStream(convertString))
                {
                    var formatter = new BinaryFormatter();
                    decryptedData = (string)formatter.Deserialize(ms);
                }
            }
            return decryptedData; //Send the result
        }

        #endregion
    }

}