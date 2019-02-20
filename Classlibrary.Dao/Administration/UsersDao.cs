#region Copyright Neutron © 2019

//
// NAME:			UserDao.cs
// AUTHOR:			Girish Girigowda
// COMPANY:			Neutron
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
    ///     <see cref="UserDao" />
    /// </summary> 
    [Serializable]
    public sealed class UserDao
    {
   
        /// <summary>
        ///     Constructor
        /// </summary>
        public UserDao()
        {
        }

        
        /// <summary>
        ///     Constructor
        /// </summary>
        public UserDao(Guid id, string userName, string email, bool emailConfirmed, string passwordHash, string securityStamp, bool phoneNumberConfirmed, bool twoFactorEnabled, bool lockoutEnabled, int accessFailedCount, DateTime createdOn, DateTime changedOn)
        {
            Id = id;
            UserName = userName;
            Email = email;
            EmailConfirmed = emailConfirmed;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            TwoFactorEnabled = twoFactorEnabled;
            LockoutEnabled = lockoutEnabled;
            AccessFailedCount = accessFailedCount;
            CreatedOn = createdOn;
            ChangedOn = changedOn;
        }
        
    
        
        /// <summary>
        ///     Ci
        /// </summary>
        public int Ci { get; set; }
        
        
        /// <summary>
        ///     Id
        /// </summary>
        public Guid Id { get; set; }
        
        
        /// <summary>
        ///     UserName
        /// </summary>
        public string UserName { get; set; }
        
        
        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }
        
        
        /// <summary>
        ///     EmailConfirmed
        /// </summary>
        public bool EmailConfirmed { get; set; }
        
        
        /// <summary>
        ///     PasswordHash
        /// </summary>
        public string PasswordHash { get; set; }
        
        
        /// <summary>
        ///     SecurityStamp
        /// </summary>
        public string SecurityStamp { get; set; }
        
        
        /// <summary>
        ///     PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }
        
        
        /// <summary>
        ///     PhoneNumberConfirmed
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        
        
        /// <summary>
        ///     MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        
        
        /// <summary>
        ///     TwoFactorEnabled
        /// </summary>
        public bool TwoFactorEnabled { get; set; }
        
        
        /// <summary>
        ///     LockoutEndDateUtc
        /// </summary>
        public DateTime? LockoutEndDateUtc { get; set; }
        
        
        /// <summary>
        ///     LockoutEnabled
        /// </summary>
        public bool LockoutEnabled { get; set; }
        
        
        /// <summary>
        ///     AccessFailedCount
        /// </summary>
        public int AccessFailedCount { get; set; }
        
        
        /// <summary>
        ///     CreatedOn
        /// </summary>
        public DateTime CreatedOn { get; set; }
        
        
        /// <summary>
        ///     ChangedOn
        /// </summary>
        public DateTime ChangedOn { get; set; }
        
        
        /// <summary>
        ///     DeletedOn
        /// </summary>
        public DateTime? DeletedOn { get; set; }
        
        
        /// <summary>
        ///     DeactivatedDate
        /// </summary>
        public DateTime? DeactivatedDate { get; set; }
        
        
        /// <summary>
        ///     Udf1
        /// </summary>
        public string Udf1 { get; set; }
        
        
        /// <summary>
        ///     Udf2
        /// </summary>
        public string Udf2 { get; set; }
        
        
        /// <summary>
        ///     Udf3
        /// </summary>
        public string Udf3 { get; set; }
        
    }
   
                
      
    /// <summary>
    ///     <see cref="UserDaoExtension" />
    /// </summary> 
    public static class UserDaoExtension
    {
        /// <summary>
        ///     Get Async
        /// </summary>
        public static async Task<UserDao> GetAsync(Guid key, string connectionString)
        {
            // Sql
            string sql = @"SELECT * FROM [Administration].[Users] WHERE Id=@key";

            // Parameters
            DynamicParameters para =new DynamicParameters();
            para.Add("@key", key);
            using(var con = new SqlConnection(connectionString))
            {
                try 
                {
                    await con.OpenAsync();
                    return await con.QueryFirstAsync<UserDao>(sql, para);
                }
                catch(Exception e)
                {
                    if(con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }               
        }
        
        /// <summary>
        ///     All Async
        /// </summary>
        public static async Task<IEnumerable<UserDao>> AllAsync(string connectionString)
        {
            // Sql
            var sql = "SELECT * FROM [Administration].[User]";

            using(var con = new SqlConnection(connectionString))
            {
                try 
                {
                    await con.OpenAsync();
                    return await con.QueryAsync<UserDao>(sql);
                }
                catch(Exception e)
                {
                    if(con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }               
        }
       
        /// <summary>
        ///     Delete Async
        /// </summary>
        public static async Task<bool> DeleteAsync(Guid key, string connectionString)
        {
            // Sql
            string sql=@"DELETE FROM [Administration].[Users] WHERE Id=@key";

            // Parameters
            DynamicParameters para =new DynamicParameters();
            para.Add("@key", key);
            using(var con = new SqlConnection(connectionString))
            {
                try 
                {
                    await con.OpenAsync();
                    await con.ExecuteAsync(sql, para);
                    return true;
                }
                catch(Exception e)
                {
                    if(con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return false;
                }
            }               
        }    

        /// <summary>
        ///     Insert Async
        /// </summary>
        public static async Task<UserDao> InsertAsync(this UserDao entity, string connectionString)
        {   
            // Sql
            string sql = @"INSERT INTO [Administration].[Users] 
				 ([Id], [UserName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [MobileNumber], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreatedOn], [ChangedOn], [DeletedOn], [DeactivatedDate], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @UserName, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @MobileNumber, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @CreatedOn, @ChangedOn, @DeletedOn, @DeactivatedDate, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para =new DynamicParameters();
			para.Add("@Id", entity.Id);
			para.Add("@UserName", entity.UserName);
			para.Add("@Email", entity.Email);
			para.Add("@EmailConfirmed", entity.EmailConfirmed);
			para.Add("@PasswordHash", entity.PasswordHash);
			para.Add("@SecurityStamp", entity.SecurityStamp);
			para.Add("@PhoneNumber", entity.PhoneNumber);
			para.Add("@PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
			para.Add("@MobileNumber", entity.MobileNumber);
			para.Add("@TwoFactorEnabled", entity.TwoFactorEnabled);
			para.Add("@LockoutEndDateUtc", entity.LockoutEndDateUtc);
			para.Add("@LockoutEnabled", entity.LockoutEnabled);
			para.Add("@AccessFailedCount", entity.AccessFailedCount);
			para.Add("@CreatedOn", entity.CreatedOn);
			para.Add("@ChangedOn", entity.ChangedOn);
			para.Add("@DeletedOn", entity.DeletedOn);
			para.Add("@DeactivatedDate", entity.DeactivatedDate);
			para.Add("@Udf1", entity.Udf1);
			para.Add("@Udf2", entity.Udf2);
			para.Add("@Udf3", entity.Udf3);
        
            // Db Operation
            using(var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                // Transaction
                using(IDbTransaction tn = con.BeginTransaction())
                {
                    try 
                    {
                        var item = await con.QuerySingleAsync<Guid>(sql, para, tn);
                        tn.Commit();
                        entity.Id = item;
                        return entity;
                    }
                    catch(Exception e)
                    {
                        tn.Rollback();
                        if(con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                        return null;
                    }
                }
            }
        }
     
        /// <summary>
        ///     Insert TransactionScope Async
        /// </summary>
        public static async Task<UserDao> InsertTransactionScopeAsync(this UserDao entity, string connectionString)
        {   
            // Sql
            string sql = @"INSERT INTO [Administration].[Users] 
				 ([Id], [UserName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [MobileNumber], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreatedOn], [ChangedOn], [DeletedOn], [DeactivatedDate], [Udf1], [Udf2], [Udf3]) 
				 OUTPUT [INSERTED].Id 
				 VALUES(@Id, @UserName, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @MobileNumber, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @CreatedOn, @ChangedOn, @DeletedOn, @DeactivatedDate, @Udf1, @Udf2, @Udf3)";

            // Parameters
            DynamicParameters para =new DynamicParameters();
			para.Add("@Id", entity.Id);
			para.Add("@UserName", entity.UserName);
			para.Add("@Email", entity.Email);
			para.Add("@EmailConfirmed", entity.EmailConfirmed);
			para.Add("@PasswordHash", entity.PasswordHash);
			para.Add("@SecurityStamp", entity.SecurityStamp);
			para.Add("@PhoneNumber", entity.PhoneNumber);
			para.Add("@PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
			para.Add("@MobileNumber", entity.MobileNumber);
			para.Add("@TwoFactorEnabled", entity.TwoFactorEnabled);
			para.Add("@LockoutEndDateUtc", entity.LockoutEndDateUtc);
			para.Add("@LockoutEnabled", entity.LockoutEnabled);
			para.Add("@AccessFailedCount", entity.AccessFailedCount);
			para.Add("@CreatedOn", entity.CreatedOn);
			para.Add("@ChangedOn", entity.ChangedOn);
			para.Add("@DeletedOn", entity.DeletedOn);
			para.Add("@DeactivatedDate", entity.DeactivatedDate);
			para.Add("@Udf1", entity.Udf1);
			para.Add("@Udf2", entity.Udf2);
			para.Add("@Udf3", entity.Udf3);
        
            // Db Operation
            using(var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                try 
                {
                    var item = await con.QuerySingleAsync<Guid>(sql, para);
                    entity.Id = item;
                    return entity;
                }
                catch(Exception e)
                {
                    if(con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }

        /// <summary>
        ///     Update Async
        /// </summary>
        public static async Task<UserDao> UpdateAsync(this UserDao entity, string connectionString)
        {   
            // Sql
            string sql=@"UPDATE [Administration].[Users] 
				 SET UserName=@UserName,Email=@Email,EmailConfirmed=@EmailConfirmed,PasswordHash=@PasswordHash,SecurityStamp=@SecurityStamp,PhoneNumber=@PhoneNumber,PhoneNumberConfirmed=@PhoneNumberConfirmed,MobileNumber=@MobileNumber,TwoFactorEnabled=@TwoFactorEnabled,LockoutEndDateUtc=@LockoutEndDateUtc,LockoutEnabled=@LockoutEnabled,AccessFailedCount=@AccessFailedCount,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn,DeletedOn=@DeletedOn,DeactivatedDate=@DeactivatedDate,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para =new DynamicParameters();
			para.Add("@Id", entity.Id);
			para.Add("@UserName", entity.UserName);
			para.Add("@Email", entity.Email);
			para.Add("@EmailConfirmed", entity.EmailConfirmed);
			para.Add("@PasswordHash", entity.PasswordHash);
			para.Add("@SecurityStamp", entity.SecurityStamp);
			para.Add("@PhoneNumber", entity.PhoneNumber);
			para.Add("@PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
			para.Add("@MobileNumber", entity.MobileNumber);
			para.Add("@TwoFactorEnabled", entity.TwoFactorEnabled);
			para.Add("@LockoutEndDateUtc", entity.LockoutEndDateUtc);
			para.Add("@LockoutEnabled", entity.LockoutEnabled);
			para.Add("@AccessFailedCount", entity.AccessFailedCount);
			para.Add("@CreatedOn", entity.CreatedOn);
			para.Add("@ChangedOn", entity.ChangedOn);
			para.Add("@DeletedOn", entity.DeletedOn);
			para.Add("@DeactivatedDate", entity.DeactivatedDate);
			para.Add("@Udf1", entity.Udf1);
			para.Add("@Udf2", entity.Udf2);
			para.Add("@Udf3", entity.Udf3);
            
            // Db Operation
            using(var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                // Transaction
                using(IDbTransaction tn = con.BeginTransaction())
                {
                    try 
                    {
                        var item = await con.ExecuteAsync(sql, para, tn);
                        tn.Commit();
                        return entity;
                    }
                    catch(Exception e)
                    {
                        tn.Rollback();
                        if(con.State != ConnectionState.Closed)
                        {
                            con.Close();
                        }
                        return null;
                    }
                }
            }
        }

        /// <summary>
        ///     Update TransactionScope Async
        /// </summary>
        public static async Task<UserDao> UpdateTransactionScopeAsync(this UserDao entity, string connectionString)
        {   
            // Sql
            string sql=@"UPDATE [Administration].[Users] 
				 SET UserName=@UserName,Email=@Email,EmailConfirmed=@EmailConfirmed,PasswordHash=@PasswordHash,SecurityStamp=@SecurityStamp,PhoneNumber=@PhoneNumber,PhoneNumberConfirmed=@PhoneNumberConfirmed,MobileNumber=@MobileNumber,TwoFactorEnabled=@TwoFactorEnabled,LockoutEndDateUtc=@LockoutEndDateUtc,LockoutEnabled=@LockoutEnabled,AccessFailedCount=@AccessFailedCount,CreatedOn=@CreatedOn,ChangedOn=@ChangedOn,DeletedOn=@DeletedOn,DeactivatedDate=@DeactivatedDate,Udf1=@Udf1,Udf2=@Udf2,Udf3=@Udf3 
				 WHERE Id=@Id;";

            // Parameters
            DynamicParameters para =new DynamicParameters();
			para.Add("@Id", entity.Id);
			para.Add("@UserName", entity.UserName);
			para.Add("@Email", entity.Email);
			para.Add("@EmailConfirmed", entity.EmailConfirmed);
			para.Add("@PasswordHash", entity.PasswordHash);
			para.Add("@SecurityStamp", entity.SecurityStamp);
			para.Add("@PhoneNumber", entity.PhoneNumber);
			para.Add("@PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
			para.Add("@MobileNumber", entity.MobileNumber);
			para.Add("@TwoFactorEnabled", entity.TwoFactorEnabled);
			para.Add("@LockoutEndDateUtc", entity.LockoutEndDateUtc);
			para.Add("@LockoutEnabled", entity.LockoutEnabled);
			para.Add("@AccessFailedCount", entity.AccessFailedCount);
			para.Add("@CreatedOn", entity.CreatedOn);
			para.Add("@ChangedOn", entity.ChangedOn);
			para.Add("@DeletedOn", entity.DeletedOn);
			para.Add("@DeactivatedDate", entity.DeactivatedDate);
			para.Add("@Udf1", entity.Udf1);
			para.Add("@Udf2", entity.Udf2);
			para.Add("@Udf3", entity.Udf3);
                
            // Db Operation
            using(var con = new SqlConnection(connectionString))
            {
                await con.OpenAsync();
                try 
                {
                    var item = await con.ExecuteAsync(sql, para);
                    return entity;
                }
                catch(Exception e)
                {
                    if(con.State != ConnectionState.Closed)
                    {
                        con.Close();
                    }
                    return null;
                }
            }
        }    
    
     }

}

