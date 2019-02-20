#region Copyright TechNeutron © 2019

//
// NAME:			ReferenceDao.cs
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

namespace Classlibrary.Dao.Utility
{
    /// <summary>
    ///     Represents the <see cref="ReferenceDao" /> class.
    /// </summary> 
    [Serializable]
    public sealed class ReferenceDao
    {

        /// <summary>
        ///     Creates an instance of <see cref="ReferenceDao" /> class.
        /// </summary>
        public ReferenceDao()
        {
        }


        /// <summary>
        ///     Creates an instance of <see cref="ReferenceDao" /> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="name">The Name.</param>
        /// <param name="countryCode">The CountryCode.</param>
        /// <param name="createdOn">The CreatedOn.</param>
        /// <param name="changedOn">The ChangedOn.</param>
        public ReferenceDao(Guid id, string name, string countryCode, DateTime createdOn, DateTime changedOn)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
        }



        /// <summary>
        ///     The Ci.
        /// </summary>
        public int Ci { get; set; }


        /// <summary>
        ///     The Id.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        ///     The Name.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     The Description.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        ///     The CountryCode.
        /// </summary>
        public string CountryCode { get; set; }


        /// <summary>
        ///     The Archived.
        /// </summary>
        public DateTime? Archived { get; set; }


        /// <summary>
        ///     The CreatedOn.
        /// </summary>
        public DateTime CreatedOn { get; set; }


        /// <summary>
        ///     The ChangedOn.
        /// </summary>
        public DateTime ChangedOn { get; set; }

    }



    /// <summary>
    ///     Instance of <see cref="ReferenceDaoExtension" />.
    /// </summary> 
    public static class ReferenceDaoExtension
    {
        /// <summary>
        ///     Get Async.
        /// </summary>
		/// <param name="key" />
        /// <param name="connectionString" />
        public static async Task<ReferenceDao> GetAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"SELECT * FROM [Utility].[Reference] WHERE Id=@key";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@key", key);
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryFirstAsync<ReferenceDao>(sql, para);
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
        public static async Task<IEnumerable<ReferenceDao>> AllAsync(string connectionString)
        {
            // Sql
            var sql = "SELECT * FROM [Utility].[Reference]";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<ReferenceDao>(sql);
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
            string sql = @"DELETE FROM [Utility].[Reference] WHERE Id=@key";

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
        public static async Task<ReferenceDao> InsertAsync(this ReferenceDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Utility].[Reference] 
				 ([Id], [Name], [Description], [CountryCode], [Archived], [CreatedOn], [ChangedOn]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @Name, @Description, @CountryCode, @Archived, @CreatedOn, @ChangedOn)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@Name", entity.Name);
            para.Add("@Description", entity.Description);
            para.Add("@CountryCode", entity.CountryCode);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);

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
                        entity.Id = item;
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
        public static async Task<ReferenceDao> InsertTransactionScopeAsync(this ReferenceDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Utility].[Reference] 
				 ([Id], [Name], [Description], [CountryCode], [Archived], [CreatedOn], [ChangedOn]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @Name, @Description, @CountryCode, @Archived, @CreatedOn, @ChangedOn)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@Name", entity.Name);
            para.Add("@Description", entity.Description);
            para.Add("@CountryCode", entity.CountryCode);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);

            // Db Operation
            using (var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                try
                {
                    var item = await con.QuerySingleAsync<Guid>(sql, para);
                    entity.Id = item;
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
        public static async Task<ReferenceDao> UpdateAsync(this ReferenceDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Utility].[Reference] 
				 SET Name=@Name,Description=@Description,CountryCode=@CountryCode,Archived=@Archived,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@Name", entity.Name);
            para.Add("@Description", entity.Description);
            para.Add("@CountryCode", entity.CountryCode);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);

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
        public static async Task<ReferenceDao> UpdateTransactionScopeAsync(this ReferenceDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Utility].[Reference] 
				 SET Name=@Name,Description=@Description,CountryCode=@CountryCode,Archived=@Archived,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@Name", entity.Name);
            para.Add("@Description", entity.Description);
            para.Add("@CountryCode", entity.CountryCode);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);

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

