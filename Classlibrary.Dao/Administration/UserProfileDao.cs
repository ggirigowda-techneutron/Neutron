#region Copyright TechNeutron © 2019

//
// NAME:			UserProfileDao.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			TechNeutron
// DATE:			2/20/2019
// PURPOSE:			DAO
//

#endregion


#region using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

#endregion

namespace Classlibrary.Dao.Administration
{
    /// <summary>
    ///     Represents the <see cref="UserProfileDao" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserProfileDao
    {

        /// <summary>
        ///     Creates an instance of <see cref="UserProfileDao" /> class.
        /// </summary>
        public UserProfileDao()
        {
        }


        /// <summary>
        ///     Creates an instance of <see cref="UserProfileDao" /> class.
        /// </summary>
        /// <param name="userId">The UserId.</param>
        /// <param name="firstName">The FirstName.</param>
        /// <param name="lastName">The LastName.</param>
        /// <param name="userTypeId">The UserTypeId.</param>
        /// <param name="genderId">The GenderId.</param>
        /// <param name="countryId">The CountryId.</param>
        public UserProfileDao(Guid userId, string firstName, string lastName, Guid userTypeId, Guid genderId, Guid countryId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserTypeId = userTypeId;
            GenderId = genderId;
            CountryId = countryId;
        }



        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }


        /// <summary>
        ///     The UserId.
        /// </summary>
        public Guid UserId { get; set; }


        /// <summary>
        ///     The FirstName.
        /// </summary>
        public string FirstName { get; set; }


        /// <summary>
        ///     The LastName.
        /// </summary>
        public string LastName { get; set; }


        /// <summary>
        ///     The UserTypeId.
        /// </summary>
        public Guid UserTypeId { get; set; }


        /// <summary>
        ///     The Title.
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        ///     The Suffix.
        /// </summary>
        public string Suffix { get; set; }


        /// <summary>
        ///     The Prefix.
        /// </summary>
        public string Prefix { get; set; }


        /// <summary>
        ///     The PrefferedName.
        /// </summary>
        public string PrefferedName { get; set; }


        /// <summary>
        ///     The Dob.
        /// </summary>
        public DateTime? Dob { get; set; }


        /// <summary>
        ///     The GenderId.
        /// </summary>
        public Guid GenderId { get; set; }


        /// <summary>
        ///     The CountryId.
        /// </summary>
        public Guid CountryId { get; set; }


        /// <summary>
        ///     The Organization.
        /// </summary>
        public string Organization { get; set; }


        /// <summary>
        ///     The Department.
        /// </summary>
        public string Department { get; set; }


        /// <summary>
        ///     The PictureUrl.
        /// </summary>
        public string PictureUrl { get; set; }


        /// <summary>
        ///     The Udf1.
        /// </summary>
        public string Udf1 { get; set; }


        /// <summary>
        ///     The Udf2.
        /// </summary>
        public string Udf2 { get; set; }


        /// <summary>
        ///     The Udf3.
        /// </summary>
        public string Udf3 { get; set; }

    }



    /// <summary>
    ///     Instance of <see cref="UserProfileDaoExtension" />.
    /// </summary> 
    public static class UserProfileDaoExtension
    {
        /// <summary>
        ///     Get Async.
        /// </summary>
		/// <param name="key" />
        /// <param name="connectionString" />
        public static async Task<UserProfileDao> GetAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"SELECT * FROM [Administration].[UserProfile] WHERE UserId=@key";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@key", key);
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryFirstAsync<UserProfileDao>(sql, para);
                }
                catch (Exception e)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }

        /// <summary>
        ///     All Async.
        /// </summary>
        /// <param name="connectionString" />
        public static async Task<IEnumerable<UserProfileDao>> AllAsync(string connectionString)
        {
            // Sql
            var sql = "SELECT * FROM [Administration].[UserProfile]";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<UserProfileDao>(sql);
                }
                catch (Exception e)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }

        /// <summary>
        ///     Delete Async.
        /// </summary>
        /// <param name="key" />
        /// <param name="connectionString" />
        public static async Task<bool> DeleteAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"DELETE FROM [Administration].[UserProfile] WHERE UserId=@key";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@key", key);
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    await con.ExecuteAsync(sql, para);
                    return true;
                }
                catch (Exception e)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return false;
                }
            }
        }

        /// <summary>
        ///     Insert Async.
        /// </summary>
        /// <param name="entity" />
        /// <param name="connectionString" />
        public static async Task<UserProfileDao> InsertAsync(this UserProfileDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Administration].[UserProfile] 
				 ([UserId], [FirstName], [LastName], [UserTypeId], [Title], [Suffix], [Prefix], [PrefferedName], [Dob], [GenderId], [CountryId], [Organization], [Department], [PictureUrl], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].UserId 
				 VALUES(@UserId, @FirstName, @LastName, @UserTypeId, @Title, @Suffix, @Prefix, @PrefferedName, @Dob, @GenderId, @CountryId, @Organization, @Department, @PictureUrl, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@FirstName", entity.FirstName);
            para.Add("@LastName", entity.LastName);
            para.Add("@UserTypeId", entity.UserTypeId);
            para.Add("@Title", entity.Title);
            para.Add("@Suffix", entity.Suffix);
            para.Add("@Prefix", entity.Prefix);
            para.Add("@PrefferedName", entity.PrefferedName);
            para.Add("@Dob", entity.Dob);
            para.Add("@GenderId", entity.GenderId);
            para.Add("@CountryId", entity.CountryId);
            para.Add("@Organization", entity.Organization);
            para.Add("@Department", entity.Department);
            para.Add("@PictureUrl", entity.PictureUrl);
            para.Add("@Udf1", entity.Udf1);
            para.Add("@Udf2", entity.Udf2);
            para.Add("@Udf3", entity.Udf3);

            // Db Operation
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                // Transaction
                using (IDbTransaction tn = con.BeginTransaction())
                {
                    try
                    {
                        var item = await con.QuerySingleAsync<Guid>(sql, para, tn);
                        tn.Commit();
                        entity.UserId = item;
                        return entity;
                    }
                    catch (Exception e)
                    {
                        tn.Rollback();
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        ///     Insert TransactionScope Async.
        /// </summary>
        /// <param name="entity" />
        /// <param name="connectionString" />
        public static async Task<UserProfileDao> InsertTransactionScopeAsync(this UserProfileDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Administration].[UserProfile] 
				 ([UserId], [FirstName], [LastName], [UserTypeId], [Title], [Suffix], [Prefix], [PrefferedName], [Dob], [GenderId], [CountryId], [Organization], [Department], [PictureUrl], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].UserId 
				 VALUES(@UserId, @FirstName, @LastName, @UserTypeId, @Title, @Suffix, @Prefix, @PrefferedName, @Dob, @GenderId, @CountryId, @Organization, @Department, @PictureUrl, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@FirstName", entity.FirstName);
            para.Add("@LastName", entity.LastName);
            para.Add("@UserTypeId", entity.UserTypeId);
            para.Add("@Title", entity.Title);
            para.Add("@Suffix", entity.Suffix);
            para.Add("@Prefix", entity.Prefix);
            para.Add("@PrefferedName", entity.PrefferedName);
            para.Add("@Dob", entity.Dob);
            para.Add("@GenderId", entity.GenderId);
            para.Add("@CountryId", entity.CountryId);
            para.Add("@Organization", entity.Organization);
            para.Add("@Department", entity.Department);
            para.Add("@PictureUrl", entity.PictureUrl);
            para.Add("@Udf1", entity.Udf1);
            para.Add("@Udf2", entity.Udf2);
            para.Add("@Udf3", entity.Udf3);

            // Db Operation
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                try
                {
                    var item = await con.QuerySingleAsync<Guid>(sql, para);
                    entity.UserId = item;
                    return entity;
                }
                catch (Exception e)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }

        /// <summary>
        ///     Update Async.
        /// </summary>
		/// <param name="entity" />
        /// <param name="connectionString" />
        public static async Task<UserProfileDao> UpdateAsync(this UserProfileDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Administration].[UserProfile] 
				 SET FirstName=@FirstName,LastName=@LastName,UserTypeId=@UserTypeId,Title=@Title,Suffix=@Suffix,Prefix=@Prefix,PrefferedName=@PrefferedName,Dob=@Dob,GenderId=@GenderId,CountryId=@CountryId,Organization=@Organization,Department=@Department,PictureUrl=@PictureUrl,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE UserId=@UserId;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@FirstName", entity.FirstName);
            para.Add("@LastName", entity.LastName);
            para.Add("@UserTypeId", entity.UserTypeId);
            para.Add("@Title", entity.Title);
            para.Add("@Suffix", entity.Suffix);
            para.Add("@Prefix", entity.Prefix);
            para.Add("@PrefferedName", entity.PrefferedName);
            para.Add("@Dob", entity.Dob);
            para.Add("@GenderId", entity.GenderId);
            para.Add("@CountryId", entity.CountryId);
            para.Add("@Organization", entity.Organization);
            para.Add("@Department", entity.Department);
            para.Add("@PictureUrl", entity.PictureUrl);
            para.Add("@Udf1", entity.Udf1);
            para.Add("@Udf2", entity.Udf2);
            para.Add("@Udf3", entity.Udf3);

            // Db Operation
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                // Transaction
                using (IDbTransaction tn = con.BeginTransaction())
                {
                    try
                    {
                        var item = await con.ExecuteAsync(sql, para, tn);
                        tn.Commit();
                        return entity;
                    }
                    catch (Exception e)
                    {
                        tn.Rollback();
                        if (con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        ///     Update TransactionScope Async.
        /// </summary>
		/// <param name="entity" />
        /// <param name="connectionString" />
        public static async Task<UserProfileDao> UpdateTransactionScopeAsync(this UserProfileDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Administration].[UserProfile] 
				 SET FirstName=@FirstName,LastName=@LastName,UserTypeId=@UserTypeId,Title=@Title,Suffix=@Suffix,Prefix=@Prefix,PrefferedName=@PrefferedName,Dob=@Dob,GenderId=@GenderId,CountryId=@CountryId,Organization=@Organization,Department=@Department,PictureUrl=@PictureUrl,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE UserId=@UserId;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@FirstName", entity.FirstName);
            para.Add("@LastName", entity.LastName);
            para.Add("@UserTypeId", entity.UserTypeId);
            para.Add("@Title", entity.Title);
            para.Add("@Suffix", entity.Suffix);
            para.Add("@Prefix", entity.Prefix);
            para.Add("@PrefferedName", entity.PrefferedName);
            para.Add("@Dob", entity.Dob);
            para.Add("@GenderId", entity.GenderId);
            para.Add("@CountryId", entity.CountryId);
            para.Add("@Organization", entity.Organization);
            para.Add("@Department", entity.Department);
            para.Add("@PictureUrl", entity.PictureUrl);
            para.Add("@Udf1", entity.Udf1);
            para.Add("@Udf2", entity.Udf2);
            para.Add("@Udf3", entity.Udf3);

            // Db Operation
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                try
                {
                    var item = await con.ExecuteAsync(sql, para);
                    return entity;
                }
                catch (Exception e)
                {
                    if (con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }

    }

}

