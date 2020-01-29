using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ArchitectureOnion.Logic.Model;
using Dapper;

namespace ArchitectureOnion.DataAccess
{
    public class Repository<T> where T : class
    {
        protected readonly string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection Conn => new SqlConnection(_connectionString);

        public async Task<int> Create(T obj)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var sql = $"INSERT INTO {type.Name} ({GetParams(props)}) VALUES ({GetParams(props, true)})";
            return await Conn.ExecuteAsync(sql, obj);
        }

        public async Task<IEnumerable<T>> ReadMany(
            string whereClause = null,
            object parameter = null)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var sql = $"SELECT Id, {GetParams(props)} FROM {type.Name} ";
            if (whereClause == null) return await Conn.QueryAsync<T>(sql);
            sql += " WHERE " + whereClause;
            return await Conn.QueryAsync<T>(sql, parameter);
        }

        public async Task<T> ReadOneById(int id)
        {
            var type = typeof(T);

            var props = type.GetProperties();
            var sql = $"SELECT Id, {GetParams(props)} FROM {type.Name} WHERE Id = @Id";
            return await Conn.QuerySingleAsync<T>(sql, new { Id = id });
        }

        public async Task<int> Update(T obj)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var sql = $"UPDATE {type.Name} SET {GetSetters(props)} WHERE Id = @Id";
            return await Conn.ExecuteAsync(sql, obj);
        }

        public async Task<int> Delete(T obj = null, int? id = null)
        {
            var type = typeof(T);
            var sql = $"DELETE FROM {type.Name} WHERE Id = @Id";
            return await Conn.ExecuteAsync(sql, obj ?? (object)new { Id = id.Value });
        }

        private static string GetParams(PropertyInfo[] props, bool includeAt = false)
        {
            return string.Join(',', props.Where(p => p.Name != "Id").Select(p => (includeAt ? "@" : "") + p.Name));
        }
        private static string GetSetters(PropertyInfo[] props)
        {
            return string.Join(',', props.Where(p => p.Name != "Id").Select(p => p.Name + " = @" + p.Name));
        }
    }

}
