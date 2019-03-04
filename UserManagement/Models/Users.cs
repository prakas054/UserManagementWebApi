using System;
using System.Configuration;
using System.Data.SqlClient;

namespace UserManagement.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Users
    {
        public string _ApplicationId { get; set; }
        public string _UserId { get; set; }
        public string _UserName {get; set;}
        public string _LoweredUserName {get; set; }
        public string _LastActivityDate { get; set; }
    }
}