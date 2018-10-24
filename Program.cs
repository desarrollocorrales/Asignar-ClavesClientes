using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Configuration;
using Asignar_ClavesClientes.Models;
using System.Data;

namespace Asignar_ClavesClientes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GenerarClave();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }

        public static void GenerarClave()
        {
            List<Clientes> lstClientes = ObtenerClientesSinClave();

            FbConnection Conexion = new FbConnection();
            Conexion.ConnectionString = GetConnectionString();
            Conexion.Open();
            FbCommand Comando = new FbCommand();
            Comando.Connection = Conexion;

            int ClaveCliente = 1001;
            foreach (Clientes cliente in lstClientes)
            {
                Comando.CommandText =
                    String.Format(@"INSERT INTO CLAVES_CLIENTES (Clave_Cliente_ID, Clave_Cliente, Cliente_ID, Rol_Clave_Cli_ID)
                                    VALUES (-1, '" + ConfigurationManager.AppSettings["serie"] + "{0}', {1}, 2) ", ClaveCliente, cliente.ID);
                Comando.ExecuteNonQuery();

                Console.WriteLine(cliente.Nombre + " actualizado con clave: " + ConfigurationManager.AppSettings["serie"] + ClaveCliente);
                ClaveCliente++;
            }

            Conexion.Close();
            Console.WriteLine();
            Console.WriteLine("El proceso ha terminado con exito");
            Console.ReadKey();
        }

        public static List<Clientes> ObtenerClientesSinClave()
        {
            List<Clientes> lstClientes = new List<Clientes>();
            FbConnection Conexion = new FbConnection();
            Conexion.ConnectionString = GetConnectionString();
            Conexion.Open();
            FbCommand Comando = new FbCommand();
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
            FbDataAdapter Adapter = new FbDataAdapter();
            Adapter.SelectCommand = Comando;
            DataTable dt = new DataTable();
            Adapter.Fill(dt);

            Clientes cliente;
            foreach (DataRow Row in dt.Rows)
            {
                cliente = new Clientes();
                cliente.ID = Convert.ToInt32(Row["CLIENTE_ID"]);
                cliente.Nombre = Convert.ToString(Row["NOMBRE"]);
                lstClientes.Add(cliente);
            }
            Conexion.Close();

            return lstClientes;
        }

        public static string GetConnectionString()
        {
            FbConnectionStringBuilder FCSB = new FbConnectionStringBuilder();

            FCSB.DataSource = ConfigurationManager.AppSettings["Server"];
            FCSB.Database = ConfigurationManager.AppSettings["Database"];
            FCSB.UserID = ConfigurationManager.AppSettings["User"];
            FCSB.Password = ConfigurationManager.AppSettings["Pass"];
            FCSB.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            return FCSB.ToString();
        }
    }
}
