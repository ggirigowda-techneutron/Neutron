#region Copyright TechNeutron © 2019

//
// NAME:			UserClaimDao.cs
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
    ///     Represents the <see cref="UserClaimDao" /> class.
    /// </summary> 
    [Serializable]
    public sealed class UserClaimDao
    {

        /// <summary>
        ///     Creates an instance of <see cref="UserClaimDao" /> class.
        /// </summary>
        public UserClaimDao()
        {
        }


        /// <summary>
        ///     Creates an instance of <see cref="UserClaimDao" /> class.
        /// </summary>
        /// <param name="userId">The UserId.</param>
        /// <param name="claimType">The ClaimType.</param>
        /// <param name="claimValue">The ClaimValue.</param>
        public UserClaimDao(Guid userId, string claimType, string claimValue)
        {
            UserId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
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
        ///     The ClaimType.
        /// </summary>
        public string ClaimType { get; set; }


        /// <summary>
        ///     The ClaimValue.
        /// </summary>
        public string ClaimValue { get; set; }

    }



    /// <summary>
    ///     Instance of <see cref="UserClaimDaoExtension" />.
    /// </summary> 
    public static class UserClaimDaoExtension
    {
        /// <summary>
        ///     Get Async.
        /// </summary>
		/// <param name="key" />
        /// <param name="connectionString" />
        public static async Task<UserClaimDao> GetAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"SELECT * FROM [Administration].[UserClaim] WHERE UserId=@key";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@key", key);
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryFirstAsync<UserClaimDao>(sql, para);
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
        public static async Task<IEnumerable<UserClaimDao>> AllAsync(string connectionString)
        {
            // Sql
            var sql = "SELECT * FROM [Administration].[UserClaim]";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<UserClaimDao>(sql);
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
            string sql = @"DELETE FROM [Administration].[UserClaim] WHERE UserId=@key";

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
        public static async Task<UserClaimDao> InsertAsync(this UserClaimDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Administration].[UserClaim] 
				 ([UserId], [ClaimType], [ClaimValue]) 
				 OUTPUT [INSERTED].UserId 
				 VALUES(@UserId, @ClaimType, @ClaimValue)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@ClaimType", entity.ClaimType);
            para.Add("@ClaimValue", entity.ClaimValue);

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
        public static async Task<UserClaimDao> InsertTransactionScopeAsync(this UserClaimDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Administration].[UserClaim] 
				 ([UserId], [ClaimType], [ClaimValue]) 
				 OUTPUT [INSERTED].UserId 
				 VALUES(@UserId, @ClaimType, @ClaimValue)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@ClaimType", entity.ClaimType);
            para.Add("@ClaimValue", entity.ClaimValue);

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
        public static async Task<UserClaimDao> UpdateAsync(this UserClaimDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Administration].[UserClaim] 
				 SET  
				 WHERE UserId=@UserId;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@ClaimType", entity.ClaimType);
            para.Add("@ClaimValue", entity.ClaimValue);

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
        public static async Task<UserClaimDao> UpdateTransactionScopeAsync(this UserClaimDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Administration].[UserClaim] 
				 SET  
				 WHERE UserId=@UserId;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserId", entity.UserId);
            para.Add("@ClaimType", entity.ClaimType);
            para.Add("@ClaimValue", entity.ClaimValue);

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

