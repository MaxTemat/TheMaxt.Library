using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace TheMaxt.SqlData
{
    /// <summary>
    /// This class allows basic manipulations on a database.
    /// </summary>
    /// <typeparam name="T">Class corresponding to a database table.</typeparam>
    public class SqlEntity<T> where T : new()
	{
		/// <summary>
		/// Array of <see cref="PropertyInfo"/> containing all properties of the class T.
		/// </summary>
		protected readonly PropertyInfo[] properties;
		/// <summary>
		/// T object.
		/// </summary>
		protected T theObject = Activator.CreateInstance<T>();
		/// <summary>
		/// <see cref="MDbConnection"/> object.
		/// </summary>
		protected readonly MDbConnection dbConnection;
		/// <summary>
		/// Name of the table.
		/// </summary>
		protected readonly string tableName;
		/// <summary>
		/// <see cref="DbProviderFactory"/> object.
		/// </summary>
		private readonly DbProviderFactory factory;
		/// <summary>
		/// Initializes a new object <see cref="SqlEntity{T}"/>.
		/// </summary>
		/// <param name="connection">Connection object that implements the <see cref="IDbConnection"/> interface</param>
		public SqlEntity(IDbConnection connection)
		{
			this.properties = theObject.GetType().GetProperties();
			this.dbConnection = new MDbConnection(connection);
			this.tableName = this.NameOf();
			factory = DbProviderFactories.GetFactory((DbConnection)connection);
		}
		/// <summary>
		/// Gets all the items from a table T.
		/// </summary>
		/// <returns><see cref="IEnumerable{T}"></see>.</returns>
		public virtual IEnumerable<T> GetAll()
		{
			try
			{
				List<T> list;
				string query = "SELECT * FROM " + tableName + ";";
				this.dbConnection.Open();
				list = this.dbConnection.Read<T>(query);
				this.dbConnection.Close();
				return list;
			}
			finally
			{
				this.dbConnection.Close();
			}
		}
		/// <summary>
		/// Gets sql data type of a property.
		/// </summary>
		/// <param name="property"></param>
		/// <returns><see cref="DbType"/>.</returns>
		protected virtual DbType GetDbType(PropertyInfo property)
		{
			if (property.PropertyType.ToString().Contains("Int32")) return DbType.Int32;
			if (property.PropertyType.ToString().Contains("Int64")) return DbType.Int64;
			if (property.PropertyType.ToString().Contains("UInt32")) return DbType.UInt32;
			if (property.PropertyType.ToString().Contains("UInt64")) return DbType.UInt64;
			if (property.PropertyType.Name == "String") return DbType.String;
			if (property.PropertyType.ToString().Contains("Double")) return DbType.Double;
			if (property.PropertyType.Name == "Byte []") return DbType.Object;
			if (property.PropertyType.ToString().Contains("Byte")) return DbType.Binary;
			if (property.PropertyType.ToString().Contains("DateTime")) return DbType.DateTime;
			if (property.PropertyType.ToString().Contains("Boolean")) return DbType.Boolean;
			return DbType.Object;
		}
        /// <summary>
        /// This method allows to insert the data of a T object into the T table.
        /// </summary>
        /// <param name="object">Object to insert.</param>
        /// <param name="idIsAutoIncrement">Specifies if id is auto increment</param>
        public virtual void Add(T @object, bool idIsAutoIncrement = true)
		{
			try
			{
				this.dbConnection.Open();
				string columnIds = "";
				string p_value = "";
				List<string> p_values = new List<string>();
				List<object> values = new List<object>();
				List<IDataParameter> parameters = new List<IDataParameter>();
				List<int> columnsToRemove = new List<int>();
				for (int i = 0; i < properties.Length; i++)
					if (properties[i].GetValue(@object) == null)
						columnsToRemove.Add(i);
				if (idIsAutoIncrement)
				{
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (i != 0)
						{
							if (!columnsToRemove.Contains(i))
								columnIds += this.properties[i].Name + ", ";
						}
					}
					columnIds = columnIds.Remove(columnIds.Length - 2);
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (i != 0)
						{
							if (!columnsToRemove.Contains(i))
							{
								p_values.Add("@p" + i);
								p_value += "@p" + i + ", ";
							}
						}
					}
					p_value = p_value.Remove(p_value.Length - 2);
					for (int i = 0; i < properties.Length; i++)
						if (i != 0 && !columnsToRemove.Contains(i))
							values.Add(properties[i].GetValue(@object));
					for (int i = 0; i < p_values.Count; i++)
					{
						var p = factory.CreateParameter();
						p.ParameterName = p_values[i];
						p.DbType = this.GetDbType(this.properties[i + 1]);
						p.Value = values[i];
						parameters.Add(p);
					}
				}
				else
				{
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (!columnsToRemove.Contains(i))
							columnIds += this.properties[i].Name + ", ";
					}
					columnIds = columnIds.Remove(columnIds.Length - 2);
					for (int i = 0; i < this.properties.Length; i++)
					{
						if (!columnsToRemove.Contains(i))
						{
							p_values.Add("@p" + i);
							p_value += "@p" + i + ", ";
						}
					}
					p_value = p_value.Remove(p_value.Length - 2);
					for (int i = 0; i < properties.Length; i++)
						if (i != 0 && !columnsToRemove.Contains(i))
							values.Add(properties[i].GetValue(@object));
					for (int i = 0; i < p_values.Count; i++)
					{
						var p = factory.CreateParameter();
						p.ParameterName = p_values[i];
						p.DbType = this.GetDbType(this.properties[i + 1]);
						p.Value = values[i];
						parameters.Add(p);
					}
				}
				string query = "INSERT INTO " + tableName + " (" + columnIds + ") VALUES (" + p_value + ");";
				this.dbConnection.Execute(query, parameters);
			}
			finally
			{
				this.dbConnection.Close();
			}
		}

		/// <summary>
		/// Removes all data from T table.
		/// </summary>
		public virtual void DeleteAll()
		{
			try
			{
				string query = "DELETE FROM " + this.tableName + ";";
				this.dbConnection.Open();
				this.dbConnection.Execute(query);
				this.dbConnection.Close();
			}
			finally
			{
				this.dbConnection.Close();
			}
		}

		/// <summary>
		/// Deletes a T object by id.
		/// </summary>
		/// <param name="object">Object to delete.</param>
		public virtual void Delete(T @object)
		{
			try
			{
				this.dbConnection.Open();
				string columnId = this.properties[0].Name;
				List<IDataParameter> parameters = new List<IDataParameter>();
				var p = factory.CreateParameter();
				p.ParameterName = "@p";
				p.DbType = this.GetDbType(this.properties[0]);
				p.Value = this.properties[0].GetValue(@object);
				parameters.Add(p);
				string query = "DELETE FROM " + tableName + " WHERE " + columnId + " = @p ;";
				this.dbConnection.Execute(query, parameters);
			}
			finally
			{
				this.dbConnection.Close();
			}
		}
		/// <summary>
		/// This method allows to modify the data of an existing object in the T table.
		/// </summary>
		/// <param name="oldObject">Object not modified</param>
		/// <param name="newObject">Object modified</param>
		public virtual void Set(T oldObject, T newObject)
		{
			try
			{
				this.dbConnection.Open();
				var columns = "";
				var columnToUpdate = new List<int>();
				var parameters = new List<IDataParameter>();
				for (int i = 0; i < properties.Length; i++)
				{
					dynamic oldValue = (properties[i].GetValue(oldObject)) != null ? properties[i].GetValue(oldObject) : null;
					dynamic newValue = (properties[i].GetValue(newObject)) != null ? properties[i].GetValue(newObject) : null;
					if (oldValue != newValue)
					{
						columns += $"{properties[i].Name} = @p{i}, ";
						columnToUpdate.Add(i);
					}
				}
				if (columnToUpdate.Count < 1)
					throw new DuplicateWaitObjectException("", "There is no difference between the old and the new object.");
				if (columns.EndsWith(", "))
					columns = columns.Remove(columns.Length - 2);
				var par = factory.CreateParameter();
				par.ParameterName = "@p_id";
				par.DbType = GetDbType(properties[0]);
				par.Value = properties[0].GetValue(newObject);
				parameters.Add(par);
				for (int i = 0; i < columnToUpdate.Count; i++)
				{
					var p = factory.CreateParameter();
					p.ParameterName = $"@p{columnToUpdate[i]}";
					p.DbType = GetDbType(properties[columnToUpdate[i]]);
					p.Value = properties[columnToUpdate[i]].GetValue(newObject) ?? DBNull.Value;
					parameters.Add(p);
				}
				string query = "UPDATE " + tableName + " SET " + columns + " WHERE " + properties[0].Name + " = @p_id;";
				this.dbConnection.Execute(query, parameters);
				this.dbConnection.Close();
			}
			finally
			{
				this.dbConnection.Close();
			}
		}
		/// <summary>
		/// Gets an object by id.
		/// </summary>
		/// <typeparam name="I">Type of id.</typeparam>
		/// <param name="id">Id of object.</param>
		/// <returns>A T object.</returns>
		public virtual T Get<I>(I id)
		{
			try
			{
				this.dbConnection.Open();
				string columnId = this.properties[0].Name;
				string query = "select * from " + this.tableName + " where " + columnId + $" = '{id}';";
				var values = this.dbConnection.Read<T>(query);
				if (values.Count < 1)
					throw new KeyNotFoundException("No data found !");
				return values[0];
			}
			finally
			{
				this.dbConnection.Close();
			}
		}
		/// <summary>
		/// Gets objects from a property.
		/// </summary>
		/// <typeparam name="V">Type of property.</typeparam>
		/// <param name="position">Position of property in the class or table. Index starts at zero</param>
		/// <param name="value">value of property</param>
		/// <returns><see cref="IEnumerable{T}"/></returns>
		public virtual IEnumerable<T> GetBy<V>(V value, int position)
		{
			try
			{
				this.dbConnection.Open();
				string column = this.properties[position].Name;
				string query = "select * from " + this.tableName + " where " + column + $" like '%{value}%';";
				return this.dbConnection.Read<T>(query);
			}
			finally
			{
				this.dbConnection.Close();
			}
		}
		/// <summary>
		/// Gets name of table or class
		/// </summary>
		/// <returns><see cref="string"/> that represents the name of table</returns>
		protected virtual string NameOf()
		{
			T obj = default(T);
			obj = Activator.CreateInstance<T>();
			return obj.GetType().Name;
		}
	}
}
