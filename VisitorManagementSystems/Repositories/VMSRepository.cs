using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using VisitorManagementSystems.Interfaces;
using VisitorManagementSystems.Models;

namespace VisitorManagementSystems.Repositories
{
    public class VMSRepository: IVMSRepository
    {
        #region Private Fields
        private string connectionString;

        #endregion Private Fields

        #region Public Constructors

        public VMSRepository(
            IConfiguration configuration,
            ILogger<VMSRepository> logger)
        {
            connectionString = configuration.GetValue<string>("DbConnectionString");
        }

        #endregion Public Constructors
        public IDbConnection GetConnection
        {
            get
            {
                var list = DbProviderFactories.GetProviderInvariantNames();
                var connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
        }
        public async Task<User> CheckUserPassword(string userName,string password)
        {
            var query = "dbo.CheckUserPassword";
            var param = new DynamicParameters();
            param.Add("User_Name", userName, dbType: DbType.String);
            param.Add("Password", password, dbType: DbType.String);
            var user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
        }
        public async Task<User> GetUserRole(int userId)
        {
            var query = "dbo.GetUserRole";
            var param = new DynamicParameters();
            param.Add("UserId", userId, dbType: DbType.Int32);
            var user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
        }
        public async Task<IEnumerable<User>> GetUsersList()
        {
            var query = "dbo.GetUsersList";
            var param = new DynamicParameters();
            var list = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<User> GetUserById(int userId)
        {
            var query = "dbo.GetUserById";
            var param = new DynamicParameters();
            param.Add("Id", userId, dbType: DbType.Int32);
            var user = await SqlMapper.QuerySingleAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user;
        }
        public async Task<Visitor> GetVisitorById(int id)
        {
            var query = "dbo.GetVisitorById";
            var param = new DynamicParameters();
            param.Add("Id", id, dbType: DbType.Int32);
            var visitor = await SqlMapper.QueryAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return visitor.FirstOrDefault();
        }
        public async Task<User> AddEditUser(User user)
        {
            var query = "dbo.AddEditUser";
            var param = new DynamicParameters();
            param.Add("Id", user.Id, dbType: DbType.Int32);
            param.Add("Name", user.Name, dbType: DbType.String);
            param.Add("User_Name", user.USER_NAME, dbType: DbType.String);
            param.Add("Password", user.PASSWORD, dbType: DbType.String);
            var _user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return _user.FirstOrDefault();
        }
        public async Task<User> DeleteUser(int userId) 
        {
            var query = "dbo.DeleteUser";
            var param = new DynamicParameters();
            param.Add("Id", userId, dbType: DbType.Int32);
            var user = await SqlMapper.QuerySingleAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user;
        }
        public async Task<Visitor> DeleteVisitor(int id)
        {
            var query = "dbo.DeleteVisitor";
            var param = new DynamicParameters();
            param.Add("Id", id, dbType: DbType.Int32);
            var visitors = await SqlMapper.QueryAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return visitors.FirstOrDefault();
        }
        public async Task<User> MapeUserGate(int id, int userId, int gateId)
        {
            var query = "dbo.MapUserGate";
            var param = new DynamicParameters();
            param.Add("Id", id, dbType: DbType.Int32);
            param.Add("UserId", userId, dbType: DbType.Int32);
            param.Add("GateId", gateId, dbType: DbType.Int32);
            var user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
        }
        public async Task<Visitor> AddEditVisitor(Visitor visitor)
        {
            var query = "dbo.AddEditVisitor";
            var param = new DynamicParameters();
            param.Add("Id", visitor.Id, dbType: DbType.Int32);
            param.Add("CreatedBy", visitor.CreatedBy, dbType: DbType.Int32);
            param.Add("ModifiedBy", visitor.ModifiedBy, dbType: DbType.Int32);
            param.Add("Name", visitor.Name, dbType: DbType.String);
            param.Add("Address", visitor.Address, dbType: DbType.String);
            param.Add("Phone", visitor.Phone, dbType: DbType.String);
            param.Add("Purpose", visitor.Purpose, dbType: DbType.String);
            param.Add("Photo", visitor.Photo, dbType: DbType.String);
            param.Add("Department", visitor.Department, dbType: DbType.String);
            param.Add("Carried_Assets", visitor.Carried_Assets, dbType: DbType.String);
            param.Add("Person_to_Meet", visitor.Person_to_Meet, dbType: DbType.String);
            param.Add("CreatedOn", visitor.CreatedOn, dbType: DbType.DateTime);
            param.Add("ModifiedOn", visitor.ModifiedOn, dbType: DbType.DateTime);
            param.Add("Entry_Time", visitor.Entry_Time, dbType: DbType.DateTime);
            param.Add("Exit_Time", visitor.Exit_Time, dbType: DbType.DateTime);
            var visitors = await SqlMapper.QuerySingleAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return visitors;
        }
        public async Task<IEnumerable<Visitor>> GetTodaysVisitors()
        {
            var query = "dbo.GetTodaysVisitors";
            var param = new DynamicParameters();
            var list = await SqlMapper.QueryAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            var query = "dbo.GetAllVisitors";
            var param = new DynamicParameters();
            var list = await SqlMapper.QueryAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<IEnumerable<Gate>> GetGates()
        {
            var query = "dbo.GetGates";
            var param = new DynamicParameters();
            var list = await SqlMapper.QueryAsync<Gate>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<User> GetUserGate(int userId)
        {
            var query = "dbo.GetUserGate";
            var param = new DynamicParameters();
            param.Add("UserId", userId, dbType: DbType.Int32);
            var user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
        }
        public async Task<IEnumerable<User>> GetRoles()
        {
            var query = "dbo.GetRoles";
            var param = new DynamicParameters();
            var list = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<User> AddEditUserRole(int userRoleid, int userId, int roleId)
        {
            var query = "dbo.AddEditUserRole";
            var param = new DynamicParameters();
            param.Add("User_Id", userId, dbType: DbType.Int32);
            param.Add("Role_Id", roleId, dbType: DbType.Int32);
            param.Add("UserRole_Id", userRoleid, dbType: DbType.Int32);
            var user = await SqlMapper.QueryAsync<User>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return user.FirstOrDefault();
        }
        public async Task<Visitor> ExitVisitor(int id,DateTime exitTime)
        {
            var query = "dbo.ExitVisitor";
            var param = new DynamicParameters();
            param.Add("Id", id, dbType: DbType.Int32);
            param.Add("Exit_Time", exitTime, dbType: DbType.DateTime);
            var visitors = await SqlMapper.QuerySingleAsync<Visitor>(this.GetConnection, query, param, commandType: CommandType.StoredProcedure);
            return visitors;
        }
    }

}
