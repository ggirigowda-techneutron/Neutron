#region Copyright TechNeutron © 2019

//
// NAME:			ReferenceItemDao.cs
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
    ///     Represents the <see cref="ReferenceItemDao" /> class.
    /// </summary> 
    [Serializable]
    public sealed class ReferenceItemDao
    {

        /// <summary>
        ///     Creates an instance of <see cref="ReferenceItemDao" /> class.
        /// </summary>
        public ReferenceItemDao()
        {
        }


        /// <summary>
        ///     Creates an instance of <see cref="ReferenceItemDao" /> class.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <param name="referenceId">The ReferenceId.</param>
        /// <param name="code">The Code.</param>
        /// <param name="description">The Description.</param>
        /// <param name="createdOn">The CreatedOn.</param>
        /// <param name="changedOn">The ChangedOn.</param>
        public ReferenceItemDao(Guid id, Guid referenceId, string code, string description, DateTime createdOn, DateTime changedOn)
        {
            Id = id;
            ReferenceId = referenceId;
            Code = code;
            Description = description;
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
        ///     The ReferenceId.
        /// </summary>
        public Guid ReferenceId { get; set; }


        /// <summary>
        ///     The Code.
        /// </summary>
        public string Code { get; set; }


        /// <summary>
        ///     The Description.
        /// </summary>
        public string Description { get; set; }


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
    ///     Instance of <see cref="ReferenceItemDaoExtension" />.
    /// </summary> 
    public static class ReferenceItemDaoExtension
    {
        /// <summary>
        ///     Get Async.
        /// </summary>
		/// <param name="key" />
        /// <param name="connectionString" />
        public static async Task<ReferenceItemDao> GetAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"SELECT * FROM [Utility].[ReferenceItem] WHERE Id=@key";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@key", key);
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryFirstAsync<ReferenceItemDao>(sql, para);
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
        public static async Task<IEnumerable<ReferenceItemDao>> AllAsync(string connectionString)
        {
            // Sql
            var sql = "SELECT * FROM [Utility].[ReferenceItem]";

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<ReferenceItemDao>(sql);
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
            string sql = @"DELETE FROM [Utility].[ReferenceItem] WHERE Id=@key";

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
        public static async Task<ReferenceItemDao> InsertAsync(this ReferenceItemDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Utility].[ReferenceItem] 
				 ([Id], [ReferenceId], [Code], [Description], [Archived], [CreatedOn], [ChangedOn], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @ReferenceId, @Code, @Description, @Archived, @CreatedOn, @ChangedOn, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@ReferenceId", entity.ReferenceId);
            para.Add("@Code", entity.Code);
            para.Add("@Description", entity.Description);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);
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
        public static async Task<ReferenceItemDao> InsertTransactionScopeAsync(this ReferenceItemDao entity, string connectionString)
        {
            // Sql
            string sql = @"INSERT INTO [Utility].[ReferenceItem] 
				 ([Id], [ReferenceId], [Code], [Description], [Archived], [CreatedOn], [ChangedOn], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @ReferenceId, @Code, @Description, @Archived, @CreatedOn, @ChangedOn, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@ReferenceId", entity.ReferenceId);
            para.Add("@Code", entity.Code);
            para.Add("@Description", entity.Description);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);
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
        public static async Task<ReferenceItemDao> UpdateAsync(this ReferenceItemDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Utility].[ReferenceItem] 
				 SET ReferenceId=@ReferenceId,Code=@Code,Description=@Description,Archived=@Archived,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@ReferenceId", entity.ReferenceId);
            para.Add("@Code", entity.Code);
            para.Add("@Description", entity.Description);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);
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
        public static async Task<ReferenceItemDao> UpdateTransactionScopeAsync(this ReferenceItemDao entity, string connectionString)
        {
            // Sql
            string sql = @"UPDATE [Utility].[ReferenceItem] 
				 SET ReferenceId=@ReferenceId,Code=@Code,Description=@Description,Archived=@Archived,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para = new DynamicParameters();
            para.Add("@Id", entity.Id);
            para.Add("@ReferenceId", entity.ReferenceId);
            para.Add("@Code", entity.Code);
            para.Add("@Description", entity.Description);
            para.Add("@Archived", entity.Archived);
            para.Add("@CreatedOn", entity.CreatedOn);
            para.Add("@ChangedOn", entity.ChangedOn);
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

