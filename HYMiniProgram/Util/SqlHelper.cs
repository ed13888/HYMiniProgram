using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace HYMiniProgram
{
    /// <summary>Dapper帮助类</summary>
    public static class SqlHelper
    {
        public static IDbConnection GetDbConnection(string connctString)
        {
            if (string.IsNullOrEmpty(connctString))
                throw new ArgumentNullException(connctString, "连接字符串不允许为空");
            MySqlConnection sqlConnection = new MySqlConnection(connctString);
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            return sqlConnection;
        }
        public static IDbConnection GetDbConnectionByName(string dbConnectionName, bool encrypted)
        {
            string dbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[dbConnectionName].ConnectionString;
            return GetDbConnection(dbConnectionString);
        }
        public static IDbConnection GetDbConnection()
        {
            return GetDbConnectionByName("ConnectionString", true);
        }

        public static int Insert(
            object data,
            string table,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Insert(data, table, transaction, commandTimeout);
        }


        public static int Update(
            object data,
            object condition,
            string table,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Update(data, condition, table, transaction, commandTimeout);
        }


        public static int Delete(
            object condition,
            string table,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Delete(condition, table, transaction, commandTimeout);
        }

        public static T ExecuteScalar<T>(string sql, object condition = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.ExecuteScalar<T>(sql, condition);
        }

        public static int Execute(string sql, object condition = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Execute(sql, condition);
        }

        public static int GetCount(
            object condition,
            Type type,
            bool isOr = false,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.GetCount(condition, type, isOr, transaction, commandTimeout);
        }

        public static T QuerySingle<T>(
            object condition,
            string sql,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.QuerySingle<T>(sql, condition, transaction, commandTimeout);
        }

        public static IEnumerable<T> QueryList<T>(
            object condition,
            string sql,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Query<T>(sql, condition, transaction, commandTimeout: commandTimeout);
        }

        public static IEnumerable<T> QueryList<T>(
            object condition,
            Type type,
            string columns = "*",
            bool isOr = false,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.QueryList<T>(condition, type, columns, isOr, transaction, commandTimeout);
        }
        public static IEnumerable<T> QueryList<T, T1>(
            string sql,
            object condition,
            Func<T, T1, T> func,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.Query<T, T1, T>("", func, condition);
        }


        public static IEnumerable<dynamic> QueryPaged(
            object condition,
            string table,
            string orderBy,
            int pageIndex,
            int pageSize,
            string columns = "*",
            bool isOr = false,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.QueryPaged<dynamic>(condition, table, orderBy, pageIndex, pageSize, columns, isOr, transaction, commandTimeout);
        }



        public static IEnumerable<T> QueryPaged<T>(
            object condition,
            string table,
            string orderBy,
            int pageIndex,
            int pageSize,
            string columns = "*",
            bool isOr = false,
            IDbTransaction transaction = null,
            int? commandTimeout = null)
        {
            using (IDbConnection connection = GetDbConnection())
                return connection.QueryPaged<T>(condition, table, orderBy, pageIndex, pageSize, columns, isOr, transaction, commandTimeout);
        }




    }
}
