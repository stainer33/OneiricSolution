using Dapper;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Oneiric.DataAccess.Dapper
{
    class DapperDao:IDapperDao
    {
        private readonly string _connectionString;
        private readonly ILogger _log = Log.ForContext<DapperDao>();
        public DapperDao(IConfiguration configuration)
        {
            this._connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<T0> ExecuteQuery<T0>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000,
                        commandType: queryType);
                    var res = result.Read<T0>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        public List<object> ExecuteQuery<T0, T1>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                sqlConnection.Open();
                var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                var res = new List<object>();
                res.Add(result.Read<T0>().ToList());
                res.Add(result.Read<T1>().ToList());
                sqlConnection.Close();
                return res;
            }
        }
        public List<object> ExecuteQuery<T0, T1, T2>(string sqlQuery, object sqlParam, System.Data.CommandType queryType = System.Data.CommandType.StoredProcedure)
        {
            using (var sqlConnection = new SqlConnection(GetConnectionString()))
            {
                try
                {
                    sqlConnection.Open();
                    var result = sqlConnection.QueryMultiple(sqlQuery, sqlParam, commandTimeout: 30000, commandType: queryType);
                    var res = new List<object>();
                    res.Add(result.Read<T0>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T1>().ToList());
                    if (result.IsConsumed) return res;
                    res.Add(result.Read<T2>().ToList());
                    return res;
                }
                catch (Exception ex)
                {
                    _log.Error("Error on Database Operation. Error Details As: {0}", ex.StackTrace);
                    throw;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
         
        }


        private string GetConnectionString()
        {
            return _connectionString ?? "";
        }
    }
}
