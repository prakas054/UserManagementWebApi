using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace UserManagement.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Users
    {
        public string _UserName {get; set;}
        public string _Password { get; set; }
        public string _ChangePassword { get; set; }

        public IEnumerable<string> Roles;
    }
}