using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TripJetLagAPI.Data
{
    public static class DataAccess
    {
        public static DbDataReader QueryStoredProcdure(string spName, int id, MobileDataDBContext context)
        {
            DbCommand cmd;

            cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", id));

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            DbDataReader dr  = cmd.ExecuteReader();

            return dr;
            
        }
    }
}
