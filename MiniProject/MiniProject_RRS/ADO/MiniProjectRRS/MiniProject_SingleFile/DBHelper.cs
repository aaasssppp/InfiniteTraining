using System;
using System.Data;
using System.Data.SqlClient;

namespace MiniProjectRRS
{
    public static class DBHelper
    {
        // Update your server name here
        private static string connectionString =
            @"Data Source=ICS-LT-4FK96V3\SQLEXPRESS; Initial Catalog=MiniProjectRRS; User ID=sa; Password=Developer@123";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Execute SELECT and return DataTable. Returns null on error.
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"[DB ERROR] Operation: ExecuteQuery -> {ex.Message}");
                // Return empty table instead of null
            }
            return dt;
        }

        // Execute INSERT/UPDATE/DELETE. Returns rows affected or -1 on error.
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteNonQuery", ex);
                return -1;
            }
        }

        // Execute scalar, returns null on error or no value.
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteScalar", ex);
                return null;
            }
        }

        // Execute multiple DB operations inside a transaction. Returns true on commit, false on error.
        public static bool ExecuteTransaction(Action<SqlConnection, SqlTransaction> transactionalWork)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        transactionalWork(conn, tran);
                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        try { tran.Rollback(); } catch { /* ignore rollback errors */ }
                        LogError("ExecuteTransaction", ex);
                        return false;
                    }
                }
            }
        }

        // Helper to create parameter easily
        public static SqlParameter Param(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        // Basic console logger — you can change to file logger later
        private static void LogError(string operation, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[DB ERROR] Operation: {operation} -> {ex.GetType().Name}: {ex.Message}");
            Console.ResetColor();
        }
    }
}

