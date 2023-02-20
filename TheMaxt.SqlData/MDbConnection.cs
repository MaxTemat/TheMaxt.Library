using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace TheMaxt.SqlData
{
    /// <summary>
    /// Class with redefinable methods to connect and manipulate a database.
    /// </summary>
    public class MDbConnection
	{
		/// <summary>
		/// Database connection property.
		/// </summary>
		protected readonly IDbConnection connection;
		/// <summary>
		/// Initializes a new object <see cref="MDbConnection"/>.
		/// </summary>
		/// <param name="connection">Connection object that implements the <see cref="IDbConnection"/> interface</param>
		public MDbConnection(IDbConnection connection)
		{
			this.connection = connection;
		}
		/// <summary>
		/// Opens a database connection.
		/// </summary>
		public virtual void Open()
		{
			if (connection.State != ConnectionState.Open)
				this.connection.Open();
		}
		/// <summary>
		/// Closes a database connection.
		/// </summary>
		public virtual void Close()
		{
			if (connection.State != ConnectionState.Closed)
				this.connection.Close();
		}
		/// <summary>
		/// Executes a write request on the database.
		/// </summary>
		/// <param name="query">The query to execute.</param>
		/// <param name="parameters">List of parameters contained in the request.</param>
		/// <param name="isStoredProcedure">Indicates if the query is a stored procedure.</param>
		/// <returns>
		/// The number of rows affected by the execution of the query.
		/// </returns>
		public virtual int Execute(string query, IEnumerable<IDataParameter> parameters = null, bool isStoredProcedure = false)
		{
			CommandType type = CommandType.Text;
			if (isStoredProcedure) type = CommandType.StoredProcedure;
			IDbCommand command = this.connection.CreateCommand();
			command.CommandText = query;
			command.CommandType = type;
			if (parameters != null)
				foreach (var p in parameters)
					command.Parameters.Add(p);
			return command.ExecuteNonQuery();
		}
		/// <summary>
		/// Executes a select query on the database.
		/// </summary>
		/// <param name="query">The query to execute.</param>
		/// <param name="parameters">List of parameters contained in the request.</param>
		/// <param name="isStoredProcedure">Indicates if the query is a stored procedure.</param>
		/// <returns>
		/// Returns an <see cref="IDataReader"/> object.
		/// </returns>
		public virtual IDataReader Read(string query, IEnumerable<IDataParameter> parameters = null, bool isStoredProcedure = false)
		{
			CommandType type = CommandType.Text;
			if (isStoredProcedure) type = CommandType.StoredProcedure;
			IDbCommand command = this.connection.CreateCommand();
			command.CommandText = query;
			command.CommandType = type;
			if (parameters != null)
				foreach (var p in parameters)
					command.Parameters.Add(p);
			return command.ExecuteReader();
		}
		/// <summary>
		/// This method allows to convert an <see cref="IDataReader"/> to <see cref="List{T}"/>.
		/// </summary>
		/// <typeparam name="T">Type of objects to return.</typeparam>
		/// <param name="reader"><see cref="IDataReader"/> object. </param>
		/// <returns><see cref="List{T}"/>.</returns>
		public virtual List<T> ToList<T>(IDataReader reader) where T : new()
		{
			List<T> list = new List<T>();
			T obj = default(T);
			while (reader.Read())
			{
				obj = Activator.CreateInstance<T>();
				foreach (PropertyInfo property in obj.GetType().GetProperties())
				{
					if (!object.Equals(reader[property.Name], DBNull.Value))
						property.SetValue(obj, reader[property.Name], default);
				}
				list.Add(obj);
			}
			return list;
		}
		/// <summary>
		/// Executes a select query on the database and returns <see cref="List{T}"/>.
		/// </summary>
		/// <param name="query">The query to execute.</param>
		/// <param name="parameters">List of parameters contained in the request.</param>
		/// <param name="isStoredProcedure">Indicates if the query is a stored procedure.</param>
		/// <typeparam name="T">Type of objects to return.</typeparam>
		/// <returns>
		/// <see cref="List{T}"/>.
		/// </returns>
		public virtual List<T> Read<T>(string query, IEnumerable<IDataParameter> parameters = null, bool isStoredProcedure = false) where T : new()
		{
			IDataReader reader = this.Read(query, parameters, isStoredProcedure);
			return this.ToList<T>(reader);
		}
	}
}
