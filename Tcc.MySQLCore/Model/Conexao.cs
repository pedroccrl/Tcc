using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcc.MySQL.Model
{
    public static class Conexao
    {
        public static string ConnString = "server=127.0.0.1;uid=root;pwd=812110;database=cidades";
        static MySqlConnection _connection = null;
        public static MySqlConnection Connection
        {
            get
            {
                if (_connection==null) Conectar();
                var state = _connection.State;
                switch (state)
                {
                    case System.Data.ConnectionState.Closed:
                        Conectar();
                        break;
                    case System.Data.ConnectionState.Broken:
                        Conectar();
                        break;
                    default:
                        break;
                }
                return _connection;
            }
            private set { _connection = value; }
        }

        public static void Conectar()
        {
            try
            {
                _connection = new MySqlConnection();
                _connection.ConnectionString = ConnString;
                _connection.Open();
                Debug.WriteLine(_connection.ServerVersion);
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public static MySqlDataReader DataReader(string query)
        {
            Conectar();
            var cmd = new MySqlCommand(query, Connection);
            var reader = cmd.ExecuteReader();
            _connection.Close();
            return reader;
        }
    }
}
