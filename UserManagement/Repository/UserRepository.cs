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

        //IConnectionFactory _connectionFactory;
        //public UserRepository(IConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}

        public List<Users> GetAllUser()
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
                return UD;
            }

            catch (Exception e )
            {
                 throw e;
                
            }
        }
        public List<Users> GetRole()
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

                    if (dr.Read()) return ("Successfully Login");

                }
                return "Invalid UserName or password";
            }
            catch (SqlException e)
            {
                return e.Message;
            }
            catch (Exception e)
            {
                return e.Message;
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
                    SqlCommand cmdForUN = new SqlCommand(QryToInsertUN, con);
                    con.Open();
                    cmdForUN.ExecuteReader();
                    cmdForUN.CommandText = QryToInsertUN;

                    cmdForUN.Dispose();
                    con.Close();

                    SqlCommand cmdForPW = new SqlCommand(QryToInsertPW, con);
                    con.Open();
                    cmdForPW.ExecuteReader();
                    cmdForPW.CommandText = QryToInsertPW;
                    con.Close();

                }

                //using (SqlConnection con = new SqlConnection(CN))
                //{
                //    SqlCommand cmdForPW = new SqlCommand(QryToInsertPW, con);
                //     con.Open();
                //    cmdForUN.ExecuteReader();

                //}
                return "Successfully SignUp";
            }
            catch (Exception e)
            {
                throw e;

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
                    con.Close();

                    if (obj._Password.Equals(OP))
                    {
                            SqlCommand cmdToUpdate = new SqlCommand(QryToUpdatePW, con);
                            con.Open();
                            cmdToUpdate.ExecuteReader();
                    }
                    else
                    {
                        return "UserName and password doesnot match";
                    }
                }
                return "Password reset Sucessfully";
            }
            catch (SqlException e)
            {
                return e.Message;

            }

            catch (Exception e)
            {
                return e.Message;

            }
        }
    }
}