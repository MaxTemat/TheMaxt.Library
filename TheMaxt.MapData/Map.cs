using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using TheMaxt.Extensions;

namespace TheMaxt.MapData
{
    /// <summary>
    /// This class is used to map data from a database management system.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Dictionary whose key represents an database type and value represents its type c#.
        /// </summary>
        public Dictionary<string, string> keys { get; }

        /// <summary>
        /// The name of connection.
        /// </summary>
        public string ConnectionStringName { get; }
        /// <summary>
        /// 
        /// </summary>
        public delegate void MapAction();
        /// <summary>
        /// 
        /// </summary>
        public event MapAction OnMapping;
        /// <summary>
        /// 
        /// </summary>
        public event MapAction OnMapFinished;

        /// <summary>
        /// Returns the number of tables in the database.
        /// </summary>
        public int TableLength { get; set; }

        private readonly Database database;

        /// <summary>
        /// <see cref="IDbConnection"/> property.
        /// </summary>
        public IDbConnection Connection { get; set; }

        /// <summary>
        /// Initializes new instance of <see cref="Map"/>. from a <see cref="IDbConnection"/> object.
        /// </summary>
        /// <param name="connection">objet <see cref="IDbConnection"/></param>
        /// <param name="database">Database management system</param>
        public Map(IDbConnection connection, Database database)
        {
            Connection = connection;
            ConnectionStringName = "SqlEntities";
            this.database = database;
            OnMapping += new MapAction(action);
            OnMapFinished += new MapAction(action);
            switch (database)
            {
                case Database.Sqlserver:
                    {
                        #region Keys
                        keys = new Dictionary<string, string>();
                        keys.Add("34", "byte[]");
                        keys.Add("341", "byte[]");

                        keys.Add("165", "byte[]");
                        keys.Add("1651", "byte[]");

                        keys.Add("173", "byte[]");
                        keys.Add("1731", "byte[]");

                        keys.Add("35", "string");
                        keys.Add("351", "string");

                        keys.Add("99", "string");
                        keys.Add("991", "string");

                        keys.Add("167", "string");
                        keys.Add("1671", "string");

                        keys.Add("231", "string");
                        keys.Add("2311", "string");

                        keys.Add("175", "char");
                        keys.Add("1751", "char?");

                        keys.Add("239", "char");
                        keys.Add("2391", "char?");

                        keys.Add("40", "DateTime");
                        keys.Add("401", "DateTime?");

                        keys.Add("41", "DateTime");
                        keys.Add("411", "DateTime?");

                        keys.Add("42", "DateTime");
                        keys.Add("421", "DateTime?");

                        keys.Add("43", "DateTime");
                        keys.Add("431", "DateTime?");

                        keys.Add("58", "DateTime");
                        keys.Add("581", "DateTime?");

                        keys.Add("61", "DateTime");
                        keys.Add("611", "DateTime?");

                        keys.Add("189", "DateTime");
                        keys.Add("1891", "DateTime?");

                        keys.Add("48", "short");
                        keys.Add("481", "short?");

                        keys.Add("52", "short");
                        keys.Add("521", "short?");

                        keys.Add("56", "int");
                        keys.Add("561", "int?");

                        keys.Add("127", "long");
                        keys.Add("1271", "long?");

                        keys.Add("59", "decimal");
                        keys.Add("591", "decimal?");

                        keys.Add("106", "decimal");
                        keys.Add("1061", "decimal?");

                        keys.Add("108", "decimal");
                        keys.Add("1081", "decimal?");

                        keys.Add("62", "double");
                        keys.Add("621", "double?");

                        keys.Add("104", "bool");
                        keys.Add("1041", "bool?");
                        #endregion
                        var tableName = "select t.TABLE_NAME " +
                        "from information_schema.tables t where t.TABLE_TYPE = 'BASE TABLE'";
                        var tableNames = new List<string>();
                        IDbCommand command = Connection.CreateCommand();
                        command.CommandText = tableName;
                        command.CommandType = CommandType.Text;
                        if (Connection.State != ConnectionState.Open)
                            Connection.Open();
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                            tableNames.Add(reader.GetString(0));
                        reader.Close();
                        if (Connection.State != ConnectionState.Closed)
                            Connection.Close();
                        TableLength = tableNames.Count;
                    }
                    break;
                case Database.Mysql:
                    {
                        #region Keys
                        keys = new Dictionary<string, string>();
                        keys.Add("char", "char");
                        keys.Add("char?", "char?");

                        keys.Add("varchar", "string");
                        keys.Add("varchar?", "string");

                        keys.Add("binary", "byte");
                        keys.Add("binary?", "byte?");

                        keys.Add("varbinary", "byte[]");
                        keys.Add("varbinary?", "byte[]");

                        keys.Add("tinyblob", "byte[]");
                        keys.Add("tinyblob?", "byte[]");

                        keys.Add("tinytext", "string");
                        keys.Add("tinytext?", "string");

                        keys.Add("text", "string");
                        keys.Add("text?", "string");

                        keys.Add("blob", "byte[]");
                        keys.Add("blob?", "byte[]");

                        keys.Add("mediumtext", "string");
                        keys.Add("mediumtext?", "string");

                        keys.Add("mediumblob", "byte[]");
                        keys.Add("mediumblob?", "byte[]");

                        keys.Add("longtext", "string");
                        keys.Add("longtext?", "string");

                        keys.Add("longblob", "byte[]");
                        keys.Add("longblob?", "byte[]");

                        keys.Add("tinyint", "int");
                        keys.Add("tinyint?", "int?");

                        keys.Add("bool", "bool");
                        keys.Add("bool?", "bool?");

                        keys.Add("boolean", "bool");
                        keys.Add("boolean?", "bool?");

                        keys.Add("smallint", "short");
                        keys.Add("smallint?", "short?");

                        keys.Add("usmallint", "ushort");
                        keys.Add("usmallint?", "ushort?");

                        keys.Add("mediumint", "int");
                        keys.Add("mediumint?", "int?");

                        keys.Add("umediumint", "uint");
                        keys.Add("umediumint?", "uint?");

                        keys.Add("int", "int");
                        keys.Add("int?", "int?");

                        keys.Add("uint", "uint");
                        keys.Add("uint?", "uint?");

                        keys.Add("integer", "int");
                        keys.Add("integer?", "int?");

                        keys.Add("uinteger", "uint");
                        keys.Add("uinteger?", "uint?");

                        keys.Add("bigint", "long");
                        keys.Add("bigint?", "long?");

                        keys.Add("ubigint", "ulong");
                        keys.Add("ubigint?", "ulong?");

                        keys.Add("decimal", "decimal");
                        keys.Add("decimal?", "decimal?");

                        keys.Add("dec", "decimal");
                        keys.Add("dec?", "decimal?");

                        keys.Add("float", "double");
                        keys.Add("float?", "double?");

                        keys.Add("double", "double");
                        keys.Add("double?", "double?");

                        keys.Add("date", "DateTime");
                        keys.Add("date?", "DateTime?");

                        keys.Add("datetime", "DateTime");
                        keys.Add("datetime?", "DateTime?");

                        keys.Add("timestamp", "DateTime");
                        keys.Add("timestamp?", "DateTime?");

                        keys.Add("time", "DateTime");
                        keys.Add("time?", "DateTime?");

                        keys.Add("year", "DateTime");
                        keys.Add("year?", "DateTime?");
                        #endregion
                        var tableName = $"SHOW FULL TABLES IN {Connection.Database} WHERE TABLE_TYPE LIKE 'BASE TABLE';";
                        var tableNames = new List<string>();
                        IDbCommand command = Connection.CreateCommand();
                        command.CommandText = tableName;
                        command.CommandType = CommandType.Text;
                        if (Connection.State != ConnectionState.Open)
                            Connection.Open();
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                            tableNames.Add(reader.GetString(0));
                        reader.Close();
                        if (Connection.State != ConnectionState.Closed)
                            Connection.Close();
                        TableLength = tableNames.Count;
                    }
                    break;
                case Database.Oracle:
                    {
                        #region Keys
                        keys = new Dictionary<string, string>();
                        keys.Add("char", "char");
                        keys.Add("char?", "char?");

                        keys.Add("nchar", "char");
                        keys.Add("nchar?", "char?");

                        keys.Add("varchar", "string");
                        keys.Add("varchar?", "string");

                        keys.Add("varchar2", "string");
                        keys.Add("varchar2?", "string");

                        keys.Add("nvarchar", "string");
                        keys.Add("nvarchar?", "string");

                        keys.Add("nvarchar2", "string");
                        keys.Add("nvarchar2?", "string");

                        keys.Add("blob", "byte[]");
                        keys.Add("blob?", "byte[]");

                        keys.Add("long", "decimal");
                        keys.Add("long?", "decimal?");

                        keys.Add("float", "float");
                        keys.Add("float?", "float?");

                        keys.Add("number", "double");
                        keys.Add("number?", "double?");

                        keys.Add("date", "DateTime");
                        keys.Add("date?", "DateTime?");

                        keys.Add("timestamp", "DateTime");
                        keys.Add("timestamp?", "DateTime?");

                        #endregion
                        var tableName = "SELECT table_name FROM user_tables";
                        var tableNames = new List<string>();
                        IDbCommand command = Connection.CreateCommand();
                        command.CommandText = tableName;
                        command.CommandType = CommandType.Text;
                        if (Connection.State != ConnectionState.Open)
                            Connection.Open();
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                            tableNames.Add(reader.GetString(0));
                        reader.Close();
                        if (Connection.State != ConnectionState.Closed)
                            Connection.Close();
                        TableLength = tableNames.Count;
                    }
                    break;
                case Database.Postgresql:
                    {
                        #region Keys

                        keys = new Dictionary<string, string>();

                        keys.Add("bigint", "long");
                        keys.Add("bigint?", "long?");

                        keys.Add("int8", "long");
                        keys.Add("int8?", "long?");

                        keys.Add("bigserial", "long");
                        keys.Add("bigserial?", "long?");

                        keys.Add("serial8", "long");
                        keys.Add("serial8?", "long?");

                        keys.Add("bit", "byte");
                        keys.Add("bit?", "byte?");

                        keys.Add("bit varying", "byte");
                        keys.Add("bit varying?", "byte?");

                        keys.Add("boolean", "bool");
                        keys.Add("boolean?", "bool?");

                        keys.Add("bool", "bool");
                        keys.Add("bool?", "bool?");

                        keys.Add("bytea", "byte[]");
                        keys.Add("bytea?", "byte[]");

                        keys.Add("character", "char");
                        keys.Add("character?", "char?");

                        keys.Add("text", "string");
                        keys.Add("text?", "string");

                        keys.Add("varchar", "string");
                        keys.Add("varchar?", "string");

                        keys.Add("character varying", "string");
                        keys.Add("character varying?", "string");

                        keys.Add("date", "DateTime");
                        keys.Add("date?", "DateTime?");

                        keys.Add("timestamp without time zone", "DateTime");
                        keys.Add("timestamp without time zone?", "DateTime?");

                        keys.Add("timestamp", "DateTime");
                        keys.Add("timestamp?", "DateTime?");

                        keys.Add("double precision", "double");
                        keys.Add("double precision?", "double?");

                        keys.Add("float8", "double");
                        keys.Add("float8?", "double?");

                        keys.Add("int", "int");
                        keys.Add("int?", "int?");

                        keys.Add("integer", "int");
                        keys.Add("integer?", "int?");

                        keys.Add("int4", "int");
                        keys.Add("int4?", "int?");

                        keys.Add("serial", "int");
                        keys.Add("serial?", "int?");

                        keys.Add("numeric", "decimal");
                        keys.Add("numeric?", "decimal?");

                        keys.Add("decimal", "decimal");
                        keys.Add("decimal?", "decimal?");

                        keys.Add("real", "float");
                        keys.Add("real?", "float?");

                        keys.Add("float4", "float");
                        keys.Add("float4?", "float?");

                        keys.Add("smallserial", "short");
                        keys.Add("smallserial?", "short?");

                        keys.Add("smallint", "short");
                        keys.Add("smallint?", "short?");

                        #endregion

                        var tableName = "SELECT table_name FROM information_schema.tables " +
                            "WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";
                        var tableNames = new List<string>();
                        IDbCommand command = Connection.CreateCommand();
                        command.CommandText = tableName;
                        command.CommandType = CommandType.Text;
                        if (Connection.State != ConnectionState.Open)
                            Connection.Open();
                        IDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                            tableNames.Add(reader.GetString(0));
                        reader.Close();
                        if (Connection.State != ConnectionState.Closed)
                            Connection.Close();
                        TableLength = tableNames.Count;
                    }
                    break;
                default:
                    break;
            }
        }

        private void action()
        {}

        /// <summary>
        /// This method allows to execute the data mapping
        /// </summary>
        /// <param name="directoryPath">The path of the folder containing the project file(.csproj)</param>
        public void Execute(string directoryPath)
        {
            var directory = new DirectoryInfo(directoryPath);

            if (database == Database.Sqlserver)
                SqlBindClasses(directory);
            if (database == Database.Mysql)
                MysqlBindClasses(directory);
            if (database == Database.Oracle)
                OracleBindClasses(directory);
            if (database == Database.Postgresql)
                PostgresBindClasses(directory);
            AddConnection(directory.FullName, ConnectionStringName);
            OnMapFinished();
        }

        /// <summary>
        /// Converts sql server tables to c# classes.
        /// </summary>
        /// <param name="directory"></param>
        private void SqlBindClasses(DirectoryInfo directory)
        {
            var tableName = "select t.TABLE_NAME " +
                        "from information_schema.tables t where t.TABLE_TYPE = 'BASE TABLE'";
            var propertyName = "select c.name, c.xusertype, c.isnullable " +
            "from sysobjects o inner join syscolumns c on o.id = c.id " +
            $"where o.name = ";

            var path = directory.FullName;
            var @namespace = directory.Name;
            var tableNames = new List<string>();
            var paramNames = new Dictionary<int, List<Column>>();
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = tableName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
                tableNames.Add(reader.GetString(0));
            reader.Close();
            
            for (int i = 0; i < tableNames.Count; i++)
            {
                command.CommandText = propertyName + $"'{tableNames[i]}'";

                var @params = new List<Column>();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt32(2) == 1)
                    {
                        var n = int.Parse(reader.GetInt16(1) + "1");
                        @params.Add(new Column(n.ToString(), reader.GetString(0)));
                    }
                    else
                        @params.Add(new Column(reader.GetInt16(1).ToString(), reader.GetString(0)));
                }
                reader.Close();
                paramNames.Add(i, @params);
            }
            for (int i = 0; i < tableNames.Count; i++)
            {
                var text = "//------------------------------------------------------------------------------" +
                    "\n// <auto-generated>" +
                    "\n//     This code was generated from a template." +
                    "\n//     Changes to this file may cause your application to behave unexpectedly." +
                    "\n//     Changes to this file are overwritten if the code is regenerated." +
                    "\n//     By TheMaxt." +
                    "\n// </auto-generated>" +
                    "\n//------------------------------------------------------------------------------\n\n" +
                    $"namespace {@namespace}.MapData\n" +
                    "{\n" +
                    "\tusing System;\n" +
                    "\tusing System.Collections.Generic;\n\n" +
                    "\t/// <summary>\n" +
                    $"\t/// This class represents the {tableNames[i]} table of the {Connection.Database} database.\n" +
                    "\t/// </summary>\n" +
                    $"\tpublic partial class {(tableNames[i]).ToPascalCase()}\n" +
                    "\t{\n";
                foreach (var paramName in paramNames)
                {
                    if (paramName.Key == i)
                    {
                        foreach (var param in paramName.Value)
                        {
                            text += $"\t\tpublic {GetType(param.Ctype.ToString())} {(param.Name).ToPascalCase()} {{ get; set; }}\n";
                        }
                        break;
                    }
                }

                text += "\t}\n}";
                var dir = Directory.CreateDirectory(path + "\\MapData");
                var p = $"{dir.FullName}\\{tableNames[i].ToPascalCase()}.cs";

                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\{tableNames[i].ToPascalCase()}.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
                OnMapping();
            }
            WriteSqlClass(directory, ConnectionStringName);
            Connection.Close();
        }

        /// <summary>
        /// Converts mysql tables to c# classes.
        /// </summary>
        /// <param name="directory"></param>
        private void MysqlBindClasses(DirectoryInfo directory)
        {
            var tableName = $"SHOW FULL TABLES IN {Connection.Database} WHERE TABLE_TYPE LIKE 'BASE TABLE';";
            var propertyName = "desc ";
            var path = directory.FullName;
            var @namespace = directory.Name;
            var tableNames = new List<string>();
            var paramNames = new Dictionary<int, List<Column>>();
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = tableName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
                tableNames.Add(reader.GetString(0));
            reader.Close();
            
            for (int i = 0; i < tableNames.Count; i++)
            {
                command.CommandText = propertyName + $"{tableNames[i]}";

                var @params = new List<Column>();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var u = "";
                    if (reader.GetString(1).ToLower().Contains("unsigned"))
                        u = "u";
                    if (reader.GetString(2).ToLower() == "yes")
                    {

                        var n = u + RemoveParenthesis(reader.GetString(1).ToLower()) + "?";
                        @params.Add(new Column(n, reader.GetString(0)));
                    }
                    else
                        @params.Add(new Column(u + (RemoveParenthesis(reader.GetString(1).ToLower())), reader.GetString(0)));
                }
                reader.Close();
                paramNames.Add(i, @params);
            }
            for (int i = 0; i < tableNames.Count; i++)
            {
                var text = "//------------------------------------------------------------------------------" +
                    "\n// <auto-generated>" +
                    "\n//     This code was generated from a template." +
                    "\n//     Changes to this file may cause your application to behave unexpectedly." +
                    "\n//     Changes to this file are overwritten if the code is regenerated." +
                    "\n//     By TheMaxt." +
                    "\n// </auto-generated>" +
                    "\n//------------------------------------------------------------------------------\n\n" +
                    $"namespace {@namespace}.MapData\n" +
                    "{\n" +
                    "\tusing System;\n" +
                    "\tusing System.Collections.Generic;\n\n" +
                    "\t/// <summary>\n" +
                    $"\t/// This class represents the {tableNames[i]} table of the {Connection.Database} database.\n" +
                    "\t/// </summary>\n" +
                    $"\tpublic partial class {(tableNames[i]).ToPascalCase()}\n" +
                    "\t{\n";
                foreach (var paramName in paramNames)
                {
                    if (paramName.Key == i)
                    {
                        foreach (var param in paramName.Value)
                        {
                            text += $"\t\tpublic {GetType(param.Ctype.ToString())} {(param.Name).ToPascalCase()} {{ get; set; }}\n";
                        }
                        break;
                    }
                }

                text += "\t}\n}";
                var dir = Directory.CreateDirectory(path + "\\MapData");
                var p = $"{dir.FullName}\\{tableNames[i].ToPascalCase()}.cs";

                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\{tableNames[i].ToPascalCase()}.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
                OnMapping();
            }
            WriteMysqlClass(directory, ConnectionStringName);
            Connection.Close();
        }

        /// <summary>
        /// Converts oracle tables to c# classes.
        /// </summary>
        /// <param name="directory"></param>
        private void OracleBindClasses(DirectoryInfo directory)
        {
            var tableName = "SELECT table_name FROM user_tables";
            var propertyName = "select column_name, data_type, nullable " +
                "from USER_TAB_COLUMNS " +
                "where table_name = '";
            var path = directory.FullName;
            var @namespace = directory.Name;
            var tableNames = new List<string>();
            var paramNames = new Dictionary<int, List<Column>>();
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = tableName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
                tableNames.Add(reader.GetString(0));
            reader.Close();

            for (int i = 0; i < tableNames.Count; i++)
            {
                command.CommandText = propertyName + $"{tableNames[i]}'";

                var @params = new List<Column>();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(2).ToLower() == "yes")
                    {

                        var n = reader.GetString(1).ToLower() + "?";
                        @params.Add(new Column(n, reader.GetString(0)));
                    }
                    else
                        @params.Add(new Column(reader.GetString(1).ToLower(), reader.GetString(0)));
                }
                reader.Close();
                paramNames.Add(i, @params);
            }
            for (int i = 0; i < tableNames.Count; i++)
            {
                var text = "//------------------------------------------------------------------------------" +
                    "\n// <auto-generated>" +
                    "\n//     This code was generated from a template." +
                    "\n//     Changes to this file may cause your application to behave unexpectedly." +
                    "\n//     Changes to this file are overwritten if the code is regenerated." +
                    "\n//     By TheMaxt." +
                    "\n// </auto-generated>" +
                    "\n//------------------------------------------------------------------------------\n\n" +
                    $"namespace {@namespace}.MapData\n" +
                    "{\n" +
                    "\tusing System;\n" +
                    "\tusing System.Collections.Generic;\n\n" +
                    "\t/// <summary>\n" +
                    $"\t/// This class represents the {tableNames[i]} table of the {Connection.Database} database.\n" +
                    "\t/// </summary>\n" +
                    $"\tpublic partial class {(tableNames[i]).ToPascalCase()}\n" +
                    "\t{\n";
                foreach (var paramName in paramNames)
                {
                    if (paramName.Key == i)
                    {
                        foreach (var param in paramName.Value)
                        {
                            text += $"\t\tpublic {GetType(param.Ctype.ToString())} {(param.Name).ToPascalCase()} {{ get; set; }}\n";
                        }
                        break;
                    }
                }

                text += "\t}\n}";
                var dir = Directory.CreateDirectory(path + "\\MapData");
                var p = $"{dir.FullName}\\{tableNames[i].ToPascalCase()}.cs";

                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\{tableNames[i].ToPascalCase()}.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
                OnMapping();
            }
            //WriteOracleClass(directory, ConnectionStringName);
            Connection.Close();
        }

        /// <summary>
        /// Converts Postgresql tables to c# classes.
        /// </summary>
        /// <param name="directory"></param>
        private void PostgresBindClasses(DirectoryInfo directory)
        {
            var tableName = "SELECT table_name FROM information_schema.tables " +
                "WHERE table_schema = 'public' AND table_type = 'BASE TABLE';";
            var propertyName = "SELECT column_name, udt_name, is_nullable " +
                "FROM information_schema.columns WHERE table_name = ";

            var path = directory.FullName;
            var @namespace = directory.Name;
            var tableNames = new List<string>();
            var paramNames = new Dictionary<int, List<Column>>();
            IDbCommand command = Connection.CreateCommand();
            command.CommandText = tableName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
                tableNames.Add(reader.GetString(0));
            reader.Close();

            for (int i = 0; i < tableNames.Count; i++)
            {
                command.CommandText = propertyName + $"'{tableNames[i]}' order by ordinal_position;";

                var @params = new List<Column>();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(2).ToLower() == "yes")
                    {
                        var n = reader.GetString(1).ToLower() + "?";
                        @params.Add(new Column(n, reader.GetString(0)));
                    }
                    else
                        @params.Add(new Column((reader.GetString(1).ToLower()), reader.GetString(0)));
                }
                reader.Close();
                paramNames.Add(i, @params);
            }
            for (int i = 0; i < tableNames.Count; i++)
            {
                var text = "//------------------------------------------------------------------------------" +
                    "\n// <auto-generated>" +
                    "\n//     This code was generated from a template." +
                    "\n//     Changes to this file may cause your application to behave unexpectedly." +
                    "\n//     Changes to this file are overwritten if the code is regenerated." +
                    "\n//     By TheMaxt." +
                    "\n// </auto-generated>" +
                    "\n//------------------------------------------------------------------------------\n\n" +
                    $"namespace {@namespace}.MapData\n" +
                    "{\n" +
                    "\tusing System;\n" +
                    "\tusing System.Collections.Generic;\n\n" +
                    "\t/// <summary>\n" +
                    $"\t/// This class represents the {tableNames[i]} table of the {Connection.Database} database.\n" +
                    "\t/// </summary>\n" +
                    $"\tpublic partial class {(tableNames[i]).ToPascalCase()}\n" +
                    "\t{\n";
                foreach (var paramName in paramNames)
                {
                    if (paramName.Key == i)
                    {
                        foreach (var param in paramName.Value)
                        {
                            text += $"\t\tpublic {GetType(param.Ctype.ToString())} {(param.Name).ToPascalCase()} {{ get; set; }}\n";
                        }
                        break;
                    }
                }

                text += "\t}\n}";
                var dir = Directory.CreateDirectory(path + "\\MapData");
                var p = $"{dir.FullName}\\{tableNames[i].ToPascalCase()}.cs";

                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\{tableNames[i].ToPascalCase()}.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
                OnMapping();
            }
            WritePostgresClass(directory, ConnectionStringName);
            Connection.Close();
        }

        private void WriteSqlClass(DirectoryInfo directory, string ConnectionStringName)
        {
            var procedureName = "select distinct so.name from sys.objects so" +
                " inner join sys.parameters p on so.object_id = p.object_id" +
                " inner join syscolumns c on c.id = p.object_id";
            var procedureArg = "select distinct SCHEMA_NAME(schema_id), so.name, so.type, p.name, p.is_output, c.xusertype" +
                " from sys.objects so inner join sys.parameters p on so.object_id = p.object_id" +
                " inner join syscolumns c on c.id = p.object_id where so.name =";
            var viewName = "select SCHEMA_NAME(o.schema_id), o.name, o.type " +
                "from sys.sql_modules m inner join sys.objects o on m.object_id = o.object_id where o.type = 'V'";

            var path = directory.FullName;
            var @namespace = directory.Name;
            string theType(string type) =>
                !type.Contains("int") ? (!type.Contains("string") ? (!type.Contains("char") ? (!type.Contains("double") ?
                (!type.Contains("DateTime") ? (!type.Contains("bool") ? (!type.Contains("byte[]") ?
                "reader[0]" : "reader[0] as byte[]") : "Convert.ToBoolean(reader[0])") : "Convert.ToDateTime(reader[0])") :
                "Convert.ToDouble(reader[0])") : "Convert.ToChar(reader[0])") : "Convert.ToString(reader[0])") :
                "Convert.ToInt32(reader[0])";
            string text = "";
            text = "//------------------------------------------------------------------------------\n" +
                "// <auto-generated>\n" +
                "//     This code was generated from a template.\n" +
                "//     Changes to this file may cause your application to behave unexpectedly.\n" +
                "//     Changes to this file are overwritten if the code is regenerated.\n" +
                "//     By TheMaxt.\n" +
                "// </auto-generated>\n" +
                "//------------------------------------------------------------------------------\n\n" +
                "namespace " + @namespace + ".MapData\n" +
                "{\n" +
                "\tusing System;\n" +
                "\tusing System.Data;\n" +
                "\tusing System.Data.Common;\n" +
                "\t/// <summary>\n" +
                "\t/// This class contains the functions mapped from the database.\n" +
                "\t/// </summary>\n" +
                "\tpublic class SqlData\n" +
                "\t{\n" +
                "\t\tprivate readonly IDbConnection connection;\n" +
                "\t\tprivate readonly DbProviderFactory factory;\n" +
                "\t\tpublic SqlData(IDbConnection connection)\n" +
                "\t\t{\n" +
                $"\t\t\tthis.connection = connection;\n" +
                "\t\t\tfactory = DbProviderFactories.GetFactory((DbConnection)connection);\n" +
                "\t\t}\n";
            var exists = false;
            var command = Connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            var reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader.GetString(0));
            reader.Close();

            if (names.Count > 0)
            {
                exists = true;
                foreach (var name in names)
                {
                    bool isFunction = false, firstPassage = true;
                    string fname = "", schema = "", returnType = "";
                    List<(string, string)> args = new List<(string, string)>();
                    var cmd = Connection.CreateCommand();
                    cmd.CommandText = $"{procedureArg} '{name}'";
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if (firstPassage)
                        {
                            if (rd.GetBoolean(4))
                            {
                                returnType = GetType(Convert.ToInt32(rd[5]).ToString());
                                isFunction = true;
                            }
                            else
                            {
                                returnType = "void";
                                isFunction = false;
                                args.Add((GetType(Convert.ToInt32(rd[5]).ToString()), CheckArgs(rd.GetString(3))));
                            }
                            fname = rd.GetString(1);
                            schema = rd.GetString(0);
                            firstPassage = false;
                        }
                        else args.Add((GetType(Convert.ToInt32(rd[5]).ToString()), CheckArgs(rd.GetString(3))));
                    }
                    rd.Close();
                    if (isFunction)
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents function {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        var param = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                            param += $"{arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        if (!param.EndsWith("(")) param = param.Remove(param.Length - 2);
                        txt += ")";
                        param += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"select {schema}.{fname}{param}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.Text;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tvar reader = command.ExecuteReader();\n" +
                            $"\t\t\t{returnType} result = default;\n" +
                            "\t\t\twhile (reader.Read())\n" +
                            "\t\t\t{\n" +
                            $"\t\t\t\tresult = {theType(returnType)};\n" +
                            "\t\t\t\tbreak;\n" +
                            "\t\t\t}\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t\treturn result;\n" +
                            "\t\t}\n";
                    }
                    else
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents procedure {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        txt += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"{fname}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.StoredProcedure;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tcommand.ExecuteNonQuery();\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t}\n";

                    }
                }
            }
            command.CommandText = viewName;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                exists = true;
                var vname = reader.GetString(1);
                text += "\t\t/// <summary>\n" +
                    $"\t\t/// This method represents view {vname}.\n" +
                    "\t\t/// </summary>\n" +
                    $"\t\tpublic IDataReader {vname.ToPascalCase()}()\n" +
                    "\t\t{\n" +
                    "\t\t\tvar command = connection.CreateCommand();\n" +
                    $"\t\t\tcommand.CommandText = \"select * from {vname}\";\n" +
                    "\t\t\tcommand.CommandType = CommandType.Text;\n" +
                    "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                    "\t\t\t\tconnection.Open();\n" +
                    "\t\t\treturn command.ExecuteReader();\n" +
                    "\t\t}\n";
            }
            reader.Close();
            text += "\t}\n" +
                "}";
            if (exists)
            {
                var dir = new DirectoryInfo(path + "\\MapData");
                var p = $"{dir.FullName}\\SqlData.cs";
                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\SqlData.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
            }
        }

        private void WriteMysqlClass(DirectoryInfo directory, string ConnectionStringName)
        {
            var procedureName = "select name from mysql.proc where db = database();";
            var procedureArg = "select name, type, param_list, returns from mysql.proc where db = database() and name =";
            var viewName = $"SHOW FULL TABLES IN {Connection.Database} WHERE TABLE_TYPE LIKE 'VIEW';";

            var path = directory.FullName;
            var @namespace = directory.Name;
            string theType(string type) =>
                !type.Contains("int") ? (!type.Contains("string") ? (!type.Contains("char") ? (!type.Contains("double") ?
                (!type.Contains("DateTime") ? (!type.Contains("bool") ? (!type.Contains("byte[]") ?
                "reader[0]" : "reader[0] as byte[]") : "Convert.ToBoolean(reader[0])") : "Convert.ToDateTime(reader[0])") :
                "Convert.ToDouble(reader[0])") : "Convert.ToChar(reader[0])") : "Convert.ToString(reader[0])") :
                "Convert.ToInt32(reader[0])";
            string text = "";
            text = "//------------------------------------------------------------------------------\n" +
                "// <auto-generated>\n" +
                "//     This code was generated from a template.\n" +
                "//     Changes to this file may cause your application to behave unexpectedly.\n" +
                "//     Changes to this file are overwritten if the code is regenerated.\n" +
                "//     By TheMaxt.\n" +
                "// </auto-generated>\n" +
                "//------------------------------------------------------------------------------\n\n" +
                "namespace " + @namespace + ".MapData\n" +
                "{\n" +
                "\tusing System;\n" +
                "\tusing System.Data;\n" +
                "\tusing System.Data.Common;\n" +
                "\t/// <summary>\n" +
                "\t/// This class contains the functions mapped from the database.\n" +
                "\t/// </summary>\n" +
                "\tpublic class SqlData\n" +
                "\t{\n" +
                "\t\tprivate readonly IDbConnection connection;\n" +
                "\t\tprivate readonly DbProviderFactory factory;\n" +
                "\t\tpublic SqlData(IDbConnection connection)\n" +
                "\t\t{\n" +
                $"\t\t\tthis.connection = connection;\n" +
                "\t\t\tfactory = DbProviderFactories.GetFactory((DbConnection)connection);\n" +
                "\t\t}\n";
            var command = Connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            var reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader.GetString(0));
            reader.Close();

            var exists = false;
            if (names.Count > 0)
            {
                exists = true;
                foreach (var name in names)
                {
                    bool isFunction = false;
                    string fname = "", returnType = "";
                    List<(string, string)> args = new List<(string, string)>();
                    var cmd = Connection.CreateCommand();
                    cmd.CommandText = $"{procedureArg} '{name}'";
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd.GetString(3)))
                        {
                            returnType = GetType(RemoveParenthesis(rd.GetString(3)));
                            isFunction = true;
                        }
                        else
                        {
                            returnType = "void";
                            isFunction = false;
                        }
                        fname = rd.GetString(0);
                        var arg = rd.GetString(2).Split(' ', ',').ToList();
                        for (int i = 0; i < arg.Count; i++)
                            if (arg[i].Length < 1)
                                arg.RemoveAt(i);
                        for (int i = 0; i < arg.Count; i += 2)
                            args.Add((GetType(RemoveParenthesis(arg[i + 1])), CheckArgs(arg[i])));
                    }
                    rd.Close();
                    if (isFunction)
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents function {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        var param = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                            param += $"{arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        if (!param.EndsWith("(")) param = param.Remove(param.Length - 2);
                        txt += ")";
                        param += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"select {fname}{param}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.Text;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tvar reader = command.ExecuteReader();\n" +
                            $"\t\t\t{returnType} result = default;\n" +
                            "\t\t\twhile (reader.Read())\n" +
                            "\t\t\t{\n" +
                            $"\t\t\t\tresult = {theType(returnType)};\n" +
                            "\t\t\t\tbreak;\n" +
                            "\t\t\t}\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t\treturn result;\n" +
                            "\t\t}\n";
                    }
                    else
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents procedure {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        txt += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"{fname}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.StoredProcedure;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tcommand.ExecuteNonQuery();\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t}\n";

                    }
                }
            }
            command.CommandText = viewName;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                exists = true;
                var vname = reader.GetString(0);
                text += "\t\t/// <summary>\n" +
                    $"\t\t/// This method represents view {vname}.\n" +
                    "\t\t/// </summary>\n" +
                    $"\t\tpublic IDataReader {vname.ToPascalCase()}()\n" +
                    "\t\t{\n" +
                    "\t\t\tvar command = connection.CreateCommand();\n" +
                    $"\t\t\tcommand.CommandText = \"select * from {vname}\";\n" +
                    "\t\t\tcommand.CommandType = CommandType.Text;\n" +
                    "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                    "\t\t\t\tconnection.Open();\n" +
                    "\t\t\treturn command.ExecuteReader();\n" +
                    "\t\t}\n";
            }
            reader.Close();
            text += "\t}\n" +
                "}";
            if (exists)
            {
                var dir = new DirectoryInfo(path + "\\MapData");
                var p = $"{dir.FullName}\\SqlData.cs";
                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\SqlData.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
            }
        }

        //private void WriteOracleClass(DirectoryInfo directory, string ConnectionStringName)
        //{
        //    var procedureName = "SELECT object_name FROM user_procedures " +
        //        "WHERE object_type = 'PROCEDURE' OR object_type = 'FUNCTION'";
        //    var procedureArg = "SELECT text FROM all_source WHERE name =";
        //    var viewName = $"select view_name from user_views";

        //    var path = directory.FullName;
        //    var @namespace = directory.Name;
        //    string theType(string type) =>
        //        !type.Contains("int") ? (!type.Contains("string") ? (!type.Contains("char") ? (!type.Contains("double") ?
        //        (!type.Contains("DateTime") ? (!type.Contains("bool") ? (!type.Contains("byte[]") ?
        //        "reader[0]" : "reader[0] as byte[]") : "Convert.ToBoolean(reader[0])") : "Convert.ToDateTime(reader[0])") :
        //        "Convert.ToDouble(reader[0])") : "Convert.ToChar(reader[0])") : "Convert.ToString(reader[0])") :
        //        "Convert.ToInt32(reader[0])";
        //    string text = "";
        //    text = "//------------------------------------------------------------------------------\n" +
        //        "// <auto-generated>\n" +
        //        "//     This code was generated from a template.\n" +
        //        "//     Changes to this file may cause your application to behave unexpectedly.\n" +
        //        "//     Changes to this file are overwritten if the code is regenerated.\n" +
        //        "//     By TheMaxt.\n" +
        //        "// </auto-generated>\n" +
        //        "//------------------------------------------------------------------------------\n\n" +
        //        "namespace " + @namespace + ".MapData\n" +
        //        "{\n" +
        //        "\tusing System;\n" +
        //        "\tusing System.Data;\n" +
        //        "\tusing System.Data.Common;\n" +
        //        "\t/// <summary>\n" +
        //        "\t/// This class contains the functions mapped from the database.\n" +
        //        "\t/// </summary>\n" +
        //        "\tpublic class SqlData\n" +
        //        "\t{\n" +
        //        "\t\tprivate readonly IDbConnection connection;\n" +
        //        "\t\tprivate readonly DbProviderFactory factory;\n" +
        //        "\t\tpublic SqlData(IDbConnection connection)\n" +
        //        "\t\t{\n" +
        //        $"\t\t\tthis.connection = connection;\n" +
        //        "\t\t\tfactory = DbProviderFactories.GetFactory((DbConnection)connection);\n" +
        //        "\t\t}\n";
        //    var command = Connection.CreateCommand();
        //    command.CommandText = procedureName;
        //    command.CommandType = CommandType.Text;
        //    if (Connection.State != ConnectionState.Open)
        //        Connection.Open();
        //    var reader = command.ExecuteReader();
        //    List<string> names = new List<string>();
        //    while (reader.Read())
        //        names.Add(reader.GetString(0));
        //    reader.Close();

        //    var exists = false;
        //    if (names.Count > 0)
        //    {
        //        exists = true;
        //        foreach (var name in names)
        //        {
        //            bool isFunction = false;
        //            string fname = "", returnType = "";
        //            List<(string, string)> args = new List<(string, string)>();
        //            var cmd = Connection.CreateCommand();
        //            cmd.CommandText = $"{procedureArg} '{name}'";
        //            cmd.CommandType = CommandType.Text;
        //            var rd = cmd.ExecuteReader();
        //            var str = "";
        //            while (rd.Read())
        //            {
        //                str += rd.GetString(0).ToLower();
        //            }
        //            rd.Close();
        //            var sep = new string[] { " ", "\n", "\t", "(", ")", "in ", "out " };
        //            var body = str.Split(sep, StringSplitOptions.RemoveEmptyEntries).ToList();
        //            if (body[0] == "function")
        //            {
        //                returnType = GetType(body[body.IndexOf("return") + 1]);
        //                isFunction = true;
        //            }
        //            else
        //            {
        //                returnType = "void";
        //                isFunction = false;
        //            }
        //            fname = body[1];
        //            var val = new List<string>();
        //            for (int i = 2; i < body.IndexOf("return"); i++)
        //                val.Add(body[i]);
        //            var a = string.Join(" ", val);
        //            var aarg = a.Split(' ', ',').ToList();
        //            for (int i = 0; i < aarg.Count; i++)
        //                if (aarg[i].Length < 1)
        //                    aarg.RemoveAt(i);
        //            for (int i = 0; i < aarg.Count; i += 2)
        //                args.Add((GetType(aarg[i + 1]), CheckArgs(aarg[i])));
        //            if (isFunction)
        //            {
        //                text += "\t\t/// <summary>\n" +
        //                $"\t\t/// This method represents function {fname}.\n" +
        //                "\t\t/// </summary>\n" +
        //                $"\t\tpublic {returnType} {fname.ToPascalCase()}";
        //                var txt = "(";
        //                var param = "(";
        //                foreach (var arg in args)
        //                {
        //                    txt += $"{arg.Item1} {arg.Item2}, ";
        //                    param += $"{arg.Item2}, ";
        //                }
        //                if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
        //                if (!param.EndsWith("(")) param = param.Remove(param.Length - 2);
        //                txt += ")";
        //                param += ")";
        //                text += $"{txt}\n" +
        //                    "\t\t{\n" +
        //                    "\t\t\tvar command = connection.CreateCommand();\n" +
        //                    $"\t\t\tcommand.CommandText = \"select {fname}{param}\";\n" +
        //                    "\t\t\tcommand.CommandType = CommandType.Text;\n";
        //                for (int i = 0; i < args.Count; i++)
        //                {
        //                    text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
        //                        $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
        //                        $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
        //                        $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
        //                        $"\t\t\tcommand.Parameters.Add(p{i});\n";
        //                }
        //                text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
        //                    "\t\t\t\tconnection.Open();\n" +
        //                    "\t\t\tvar reader = command.ExecuteReader();\n" +
        //                    $"\t\t\t{returnType} result = default;\n" +
        //                    "\t\t\twhile (reader.Read())\n" +
        //                    "\t\t\t{\n" +
        //                    $"\t\t\t\tresult = {theType(returnType)};\n" +
        //                    "\t\t\t\tbreak;\n" +
        //                    "\t\t\t}\n" +
        //                    "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
        //                    "\t\t\t\tconnection.Close();\n" +
        //                    "\t\t\treturn result;\n" +
        //                    "\t\t}\n";
        //            }
        //            else
        //            {
        //                text += "\t\t/// <summary>\n" +
        //                $"\t\t/// This method represents procedure {fname}.\n" +
        //                "\t\t/// </summary>\n" +
        //                $"\t\tpublic {returnType} {fname.ToPascalCase()}";
        //                var txt = "(";
        //                foreach (var arg in args)
        //                {
        //                    txt += $"{arg.Item1} {arg.Item2}, ";
        //                }
        //                if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
        //                txt += ")";
        //                text += $"{txt}\n" +
        //                    "\t\t{\n" +
        //                    "\t\t\tvar command = connection.CreateCommand();\n" +
        //                    $"\t\t\tcommand.CommandText = \"{fname}\";\n" +
        //                    "\t\t\tcommand.CommandType = CommandType.StoredProcedure;\n";
        //                for (int i = 0; i < args.Count; i++)
        //                {
        //                    text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
        //                        $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
        //                        $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
        //                        $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
        //                        $"\t\t\tcommand.Parameters.Add(p{i});\n";
        //                }
        //                text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
        //                    "\t\t\t\tconnection.Open();\n" +
        //                    "\t\t\tcommand.ExecuteNonQuery();\n" +
        //                    "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
        //                    "\t\t\t\tconnection.Close();\n" +
        //                    "\t\t}\n";

        //            }
        //        }
        //    }
        //    command.CommandText = viewName;
        //    reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        exists = true;
        //        var vname = reader.GetString(0);
        //        text += "\t\t/// <summary>\n" +
        //            $"\t\t/// This method represents view {vname}.\n" +
        //            "\t\t/// </summary>\n" +
        //            $"\t\tpublic IDataReader {vname.ToPascalCase()}()\n" +
        //            "\t\t{\n" +
        //            "\t\t\tvar command = connection.CreateCommand();\n" +
        //            $"\t\t\tcommand.CommandText = \"select * from {vname}\";\n" +
        //            "\t\t\tcommand.CommandType = CommandType.Text;\n" +
        //            "\t\t\tif (connection.State != ConnectionState.Open)\n" +
        //            "\t\t\t\tconnection.Open();\n" +
        //            "\t\t\treturn command.ExecuteReader();\n" +
        //            "\t\t}\n";
        //    }
        //    reader.Close();
        //    text += "\t}\n" +
        //        "}";
        //    if (exists)
        //    {
        //        var dir = new DirectoryInfo(path + "\\MapData");
        //        var p = $"{dir.FullName}\\SqlData.cs";
        //        File.WriteAllText(p, text);
        //        var keyword = "<ItemGroup>";
        //        var contents = $"\t<Compile Include=\"{dir.Name}\\SqlData.cs\" />";
        //        p = $"{path}\\{@namespace}.csproj";
        //        InsertText(p, keyword, contents);
        //    }
        //}

        private void WritePostgresClass(DirectoryInfo directory, string ConnectionStringName)
        {
            var procedureName = "SELECT p.proname AS function_name " +
                "FROM pg_proc p LEFT JOIN pg_namespace n ON p.pronamespace = n.oid " +
                "WHERE n.nspname NOT IN('pg_catalog', 'information_schema'); ";
            var procedureArg = "SELECT proname, unnest(proargnames) as arguments, " +
                "unnest(string_to_array((oidvectortypes(proargtypes)), ',')) as arguments_type, typname " +
                "as return FROM    pg_catalog.pg_namespace n " +
                "JOIN pg_catalog.pg_proc p   ON pronamespace = n.oid JOIN pg_type t ON p.prorettype = t.oid " +
                "WHERE nspname = 'public' and proname =";
            var viewName = "select table_name from INFORMATION_SCHEMA.views WHERE table_schema = ANY (current_schemas(false));";

            var path = directory.FullName;
            var @namespace = directory.Name;
            string theType(string type) =>
                !type.Contains("int") ? (!type.Contains("string") ? (!type.Contains("char") ? (!type.Contains("double") ?
                (!type.Contains("DateTime") ? (!type.Contains("bool") ? (!type.Contains("byte[]") ?
                "reader[0]" : "reader[0] as byte[]") : "Convert.ToBoolean(reader[0])") : "Convert.ToDateTime(reader[0])") :
                "Convert.ToDouble(reader[0])") : "Convert.ToChar(reader[0])") : "Convert.ToString(reader[0])") :
                "Convert.ToInt32(reader[0])";
            string text = "";
            text = "//------------------------------------------------------------------------------\n" +
                "// <auto-generated>\n" +
                "//     This code was generated from a template.\n" +
                "//     Changes to this file may cause your application to behave unexpectedly.\n" +
                "//     Changes to this file are overwritten if the code is regenerated.\n" +
                "//     By TheMaxt.\n" +
                "// </auto-generated>\n" +
                "//------------------------------------------------------------------------------\n\n" +
                "namespace " + @namespace + ".MapData\n" +
                "{\n" +
                "\tusing System;\n" +
                "\tusing System.Data;\n" +
                "\tusing System.Data.Common;\n" +
                "\t/// <summary>\n" +
                "\t/// This class contains the functions mapped from the database.\n" +
                "\t/// </summary>\n" +
                "\tpublic class SqlData\n" +
                "\t{\n" +
                "\t\tprivate readonly IDbConnection connection;\n" +
                "\t\tprivate readonly DbProviderFactory factory;\n" +
                "\t\tpublic SqlData(IDbConnection connection)\n" +
                "\t\t{\n" +
                $"\t\t\tthis.connection = connection;\n" +
                "\t\t\tfactory = DbProviderFactories.GetFactory((DbConnection)connection);\n" +
                "\t\t}\n";
            var exists = false;
            var command = Connection.CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.Text;
            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            var reader = command.ExecuteReader();
            List<string> names = new List<string>();
            while (reader.Read())
                names.Add(reader.GetString(0));
            reader.Close();

            if (names.Count > 0)
            {
                exists = true;
                foreach (var name in names)
                {
                    bool isFunction = false, firstPassage = true;
                    string fname = "", returnType = "";
                    List<(string, string)> args = new List<(string, string)>();
                    var cmd = Connection.CreateCommand();
                    cmd.CommandText = $"{procedureArg} '{name}';";
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        if (firstPassage)
                        {
                            if (rd.GetString(3).ToLower() != "void")
                            {
                                returnType = GetType(rd.GetString(3));
                                isFunction = true;
                            }
                            else
                            {
                                returnType = "void";
                                isFunction = false;
                            }
                            args.Add((GetType(rd.GetString(2)), CheckArgs(rd.GetString(1))));
                            fname = rd.GetString(0);
                            firstPassage = false;
                        }
                        else args.Add((GetType(rd.GetString(2)), CheckArgs(rd.GetString(1))));
                    }
                    rd.Close();
                    if (isFunction)
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents function {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        var param = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                            param += $"{arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        if (!param.EndsWith("(")) param = param.Remove(param.Length - 2);
                        txt += ")";
                        param += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"select {fname}{param}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.Text;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tvar reader = command.ExecuteReader();\n" +
                            $"\t\t\t{returnType} result = default;\n" +
                            "\t\t\twhile (reader.Read())\n" +
                            "\t\t\t{\n" +
                            $"\t\t\t\tresult = {theType(returnType)};\n" +
                            "\t\t\t\tbreak;\n" +
                            "\t\t\t}\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t\treturn result;\n" +
                            "\t\t}\n";
                    }
                    else
                    {
                        text += "\t\t/// <summary>\n" +
                        $"\t\t/// This method represents procedure {fname}.\n" +
                        "\t\t/// </summary>\n" +
                        $"\t\tpublic {returnType} {fname.ToPascalCase()}";
                        var txt = "(";
                        foreach (var arg in args)
                        {
                            txt += $"{arg.Item1} {arg.Item2}, ";
                        }
                        if (!txt.EndsWith("(")) txt = txt.Remove(txt.Length - 2);
                        txt += ")";
                        text += $"{txt}\n" +
                            "\t\t{\n" +
                            "\t\t\tvar command = connection.CreateCommand();\n" +
                            $"\t\t\tcommand.CommandText = \"{fname}\";\n" +
                            "\t\t\tcommand.CommandType = CommandType.StoredProcedure;\n";
                        for (int i = 0; i < args.Count; i++)
                        {
                            text += $"\t\t\tvar p{i} = factory.CreateParameter();\n" +
                                $"\t\t\tp{i}.ParameterName = \"{args[i].Item2}\";\n" +
                                $"\t\t\tp{i}.DbType = {GetDbType(args[i].Item1)};\n" +
                                $"\t\t\tp{i}.Value = {args[i].Item2};\n" +
                                $"\t\t\tcommand.Parameters.Add(p{i});\n";
                        }
                        text += "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                            "\t\t\t\tconnection.Open();\n" +
                            "\t\t\tcommand.ExecuteNonQuery();\n" +
                            "\t\t\tif (connection.State != ConnectionState.Closed)\n" +
                            "\t\t\t\tconnection.Close();\n" +
                            "\t\t}\n";

                    }
                }
            }
            command.CommandText = viewName;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                exists = true;
                var vname = reader.GetString(0);
                text += "\t\t/// <summary>\n" +
                    $"\t\t/// This method represents view {vname}.\n" +
                    "\t\t/// </summary>\n" +
                    $"\t\tpublic IDataReader {vname.ToPascalCase()}()\n" +
                    "\t\t{\n" +
                    "\t\t\tvar command = connection.CreateCommand();\n" +
                    $"\t\t\tcommand.CommandText = \"select * from {vname}\";\n" +
                    "\t\t\tcommand.CommandType = CommandType.Text;\n" +
                    "\t\t\tif (connection.State != ConnectionState.Open)\n" +
                    "\t\t\t\tconnection.Open();\n" +
                    "\t\t\treturn command.ExecuteReader();\n" +
                    "\t\t}\n";
            }
            reader.Close();
            text += "\t}\n" +
                "}";
            if (exists)
            {
                var dir = new DirectoryInfo(path + "\\MapData");
                var p = $"{dir.FullName}\\SqlData.cs";
                File.WriteAllText(p, text);
                var keyword = "<ItemGroup>";
                var contents = $"\t<Compile Include=\"{dir.Name}\\SqlData.cs\" />";
                p = $"{path}\\{@namespace}.csproj";
                InsertText(p, keyword, contents);
            }
        }

        private string GetDbType(string property)
        {
            if (property.Contains("int")) return "DbType.Int32";
            if (property == "string") return "DbType.String";
            if (property.Contains("double")) return "DbType.Double";
            if (property == "Byte []") return "DbType.Object";
            if (property.Contains("DateTime")) return "DbType.DateTime2";
            if (property.Contains("bool")) return "DbType.Boolean";
            return "DbType.Object";
        }

        /// <summary>
        /// Inserts text where a keyword is in a file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="keyword">Keyword.</param>
        /// <param name="contents">Text to insert.</param>
        private static void InsertText(string path, string keyword, string contents)
        {
            var texts = File.ReadAllText(path).Split('\n').ToList();
            var index = 0;
            for (int j = 0; j < texts.Count; j++)
            {
                if (texts[j].Contains(keyword))
                {
                    index = j + 1;
                    break;
                }
            }
            texts.Insert(index, contents);
            var text = "";
            foreach (var item in texts)
                text += item + '\n';
            text.Remove(text.Length - 1);
            File.WriteAllText(path, text);
        }

        /// <summary>
        /// Add a connection in the project configuration file.
        /// </summary>
        /// <param name="path">Path of folder containing configuration file(.config).</param>
        /// <param name="connectionName">Connection name.</param>
        private void AddConnection(string path, string connectionName)
        {
            var app = path + "\\App.config";
            var web = path + "\\Web.config";
            if (File.Exists(app))
            {
                string text, keyword;
                if (File.ReadAllText(app).Contains("<connectionStrings>"))
                {
                    text = $"\t\t<add name = \"{connectionName}\" connectionString = \"{Connection.ConnectionString}\" />";
                    keyword = "<connectionStrings>";
                }
                else
                {
                    text = "\t<connectionStrings>\n" +
                    $"\t\t<add name = \"{connectionName}\" connectionString = \"{Connection.ConnectionString}\" />\n" +
                    "\t</connectionStrings> ";
                    keyword = "</startup>";
                }
                InsertText(app, keyword, text);
            }
            if (File.Exists(web))
            {
                string text, keyword;
                if (File.ReadAllText(app).Contains("<connectionStrings>"))
                {
                    text = $"\t\t<add name = \"{connectionName}\" connectionString = \"{Connection.ConnectionString}\" />";
                    keyword = "<connectionStrings>";
                }
                else
                {
                    text = "\t<connectionStrings>\n" +
                    $"\t\t<add name = \"{connectionName}\" connectionString = \"{Connection.ConnectionString}\" />\n" +
                    "\t</connectionStrings> ";
                    keyword = "</appSettings>";
                }
                InsertText(app, keyword, text);
            }
        }

        /// <summary>
        /// Gets c# type from a database code.
        /// </summary>
        /// <param name="code">Database code.</param>
        /// <returns><see cref="string"/> that represents a c# type.</returns>
        private string GetType(string code)
        {
            foreach (var key in keys)
                if (key.Key == code)
                    return key.Value;
            return "object";
        }

        /// <summary>
        /// Removes parentheses ().
        /// </summary>
        /// <param name="text">String to evaluate.</param>
        /// <returns></returns>
        private string RemoveParenthesis(string text)
        {
            var index = text.IndexOf('(');
            if (index != -1)
                return text.Remove(index);
            else return text;
        }

        /// <summary>
        /// Checks if arg has a .NET convention form.
        /// </summary>
        /// <param name="arg">Arg to evaluate</param>
        /// <returns>Arg with .NET convention form.</returns>
        private string CheckArgs(string arg)
        {
            if (arg.StartsWith("@")) return arg;
            else return "@" + arg;
        }
    }
}
