using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using Asignar_ClavesClientes.Models;
using System.Data;

namespace Asignar_ClavesClientes.Controller
{
    public class Firebird
    {
        /*private FbConnection Conexion;
        private FbCommand Comando;
        private FbDataAdapter Adapter;

        public FireBird()
        {
            Conexion = new FbConnection();
            Comando = new FbCommand();
            Adapter = new FbDataAdapter();
        }

        private string GetConnectionString()
        {
            FbConnectionStringBuilder FCSB = new FbConnectionStringBuilder();
            
            FCSB.DataSource = ConfigurationManager.AppSettings["Server"];
            FCSB.Database = ConfigurationManager.AppSettings["Database"];
            FCSB.UserID = ConfigurationManager.AppSettings["User"];
            FCSB.Password = ConfigurationManager.AppSettings["Pass"];
            FCSB.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            return FCSB.ToString();
        }

        public void GenerarClave()
        {
            List<Clientes> lstClientes = ObtenerClientesSinClave();

            Conexion.ConnectionString = GetConnectionString();
            Comando.Connection = Conexion;

            int ClaveCliente = 1001;
            foreach(Clientes cliente in lstClientes)
            {
                Comando.CommandText = 
                    String.Format(@"INSERT INTO CLAVES_CLIENTES (Clave_Cliente_ID, Clave_Cliente, Cliente_ID, Rol_Clave_ID)
                                    VALUES (-1, '{0}', {1}, 2) ", ClaveCliente, cliente.ID);
                Comando.ExecuteNonQuery();

                Comando.CommandText = @"SELECT MAX(CLAVE_CLIENTE_ID) FROM CLAVES_CLIENTES";
                cliente.Clave_ID = Convert.ToInt32(Comando.ExecuteScalar());

                Comando.CommandText = 
                    String.Format(@"UPDATE CLIENTES SET CLAVE_CLIENTE_ID = ''",cliente.Clave_ID);
                Comando.ExecuteNonQuery();

                Console.WriteLine("Cliente actualizado con clave: " + ClaveCliente);
                ClaveCliente++;
            } 

            Console.WriteLine();
            Console.WriteLine("El proceso ha terminado con exito");
        }

        private List<Clientes> ObtenerClientesSinClave()
        {
            List<Clientes> lstClientes = new List<Clientes>();
            Conexion.ConnectionString = GetConnectionString();
            Comando.Connection = Conexion;
            Comando.CommandText =
                @"SELECT 
                  CLIENTES.CLIENTE_ID,
                  CLIENTES.NOMBRE
                FROM
                  CLIENTES
                  LEFT OUTER JOIN CLAVES_CLIENTES ON (CLIENTES.CLIENTE_ID = CLAVES_CLIENTES.CLIENTE_ID)
                WHERE
                  CLAVES_CLIENTES.CLAVE_CLIENTE IS NULL
                ORDER BY Nombre";
            Adapter.SelectCommand = Comando;
            DataTable dt = new DataTable();
            Adapter.Fill(dt);

            Clientes cliente;
            foreach(DataRow Row in dt.Rows)
            {
                cliente = new Clientes();
                cliente.ID = Convert.ToInt32(Row["CLIENTE_ID"]);
                cliente.Nombre = Convert.ToString(Row["NOMBRE"]);
                lstClientes.Add(cliente);
            }

            return lstClientes;
        }
    }*/
    }
}