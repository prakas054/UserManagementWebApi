using System;
using System.Collections.Generic;
using UserManagement.Models;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess.Infrastructure;

namespace UserManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        string CN = ConfigurationManager.ConnectionStrings["SqlServices"].ConnectionString;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger("UserController");

        //IConnectionFactory _connectionFactory;
        //public UserRepository(IConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}

        public IEnumerable<Users> GetAllUser()
        {
            string qry = "select [UserName] from [dbo].[aspnet_Users]";
            try
            {
                List<Users> UD = new List<Users>();
                using (SqlConnection con = new SqlConnection(CN))
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Users UsersObj = new Users();
                        UsersObj._UserName = dr["UserName"].ToString();
                        UD.Add(UsersObj);
                    }
                }
                _log.Info("Received GetAll users request");
                return UD;
            }
            catch (Exception e )
            {
                _log.Error(e.ToString());
                throw;
            }
        }
        public IEnumerable<Users> GetRole()
        {
            throw new NotImplementedException();
        }

        public string Login(string un, string pw)
        {
            string UN = un;
            string PW = pw;
            string qry = "select u.[UserId] from [dbo].[aspnet_Users] u inner join [dbo].[aspnet_Membership] m "+
                          " on u.UserId = m.UserId where u.UserName = '"+UN+"' and m.Password = '"+PW+"' ";

            try
            {
                using (SqlConnection con = new SqlConnection(CN))
                {
                    SqlCommand cmd = new SqlCommand(qry, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        _log.Info("Successfully Login");
                        return ("Successfully Login");                        
                    }
                }
                _log.Info("Invalid Username or Password---------Invalid UserName or password");
                return "Invalid UserName or password";
            }
            catch (SqlException e)
            {
                _log.Error(e.ToString());
                return e.ToString();
            }
        }

        public string Create(string un, string pw)
        {
            string UN = un;
            string PW = pw;

            string QryToInsertUN = "Insert Into aspnet_Users ([ApplicationId], [UserId], [UserName], [LoweredUserName], [LastActivityDate] )" +
                          "values((select[ApplicationId] from aspnet_Applications where ApplicationName = 'MyApplication'),NEWID(), '" + un + "', LOWER('" + un + "'), GETDATE())";

            string QryToInsertPW = "Insert Into aspnet_Membership ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [IsApproved], [IsLockedOut], [CreateDate]," +
                              " [LastLoginDate],[LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart],[FailedPasswordAnswerAttemptCount], " +
                              "[FailedPasswordAnswerAttemptWindowStart]) values ((select[ApplicationId] from aspnet_Users where UserName = '" + un + "'), " +
                              "(select[UserId] from aspnet_Users where UserName = '" + un + "'), '" + pw + "', 0, 'NA', 0, 0, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, GETDATE(), 0, GETDATE()) ";

            try
            {
                using (SqlConnection con = new SqlConnection(CN))
                {
                    SqlCommand cmd = new SqlCommand(QryToInsertUN, con);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = QryToInsertPW;
                    cmd.ExecuteNonQuery();
                }
                _log.Info("Successfully SignUp");
                return "Successfully SignUp";
            }
            catch (SqlException e)
            {
                _log.Error(e.ToString());
                return e.ToString();
            }
        }

        public string ChangePassword(string UserName, string OldPassword, string NewPassword)
        {
            string UN = UserName;
            string OP = OldPassword;
            string NP = NewPassword;

            string QryToGetPW = "select [Password] from [dbo].[aspnet_Membership] where [UserId] = (select [UserId] from [dbo].[aspnet_Users] where [UserName] = '" + UN + "')";

            string QryToUpdatePW = "UPDATE [dbo].[aspnet_Membership] SET[Password] = '"+NP+"' WHERE [UserId] = (select[UserId] from[dbo].[aspnet_Users] where[UserName] = '"+UN+"')";

            try
            {
                using (SqlConnection con = new SqlConnection(CN))
                {
                    SqlCommand cmd = new SqlCommand(QryToGetPW, con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    Membership obj = new Membership();

                    if (dr.Read())
                    {
                        obj._Password = dr["Password"].ToString();
                    }
                    dr.Close();

                    if (obj._Password != null && obj._Password.Equals(OP))
                    {
                            cmd.CommandText = QryToUpdatePW;
                            cmd.ExecuteScalar();
                    }
                    else
                    {
                        _log.Info("Password reset fail");
                        return "UserName and password doesnot match";
                    }
                }
                _log.Info("Password reset Sucessfully");
                return "Password reset Sucessfully";
            }
            catch (SqlException e)
            {
                _log.Error(e.ToString());
                return e.ToString();
            }
        }
    }
}