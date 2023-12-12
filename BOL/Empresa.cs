﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using System.Data;              //Objetos manejadores datos
using System.Data.SqlClient;
using ENTITIES;

namespace BOL
{
    public class Empresa
    {
        DBAccess conexion = new DBAccess();

        public int Registrar(EEmpresa entidad)
        {
            int totalRegistros = 0;
            SqlCommand comando = new SqlCommand("sp_empresa_registrar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();

            try
            {
                comando.Parameters.AddWithValue("@razonsocial", entidad.razonsocial);
                comando.Parameters.AddWithValue("@ruc", entidad.ruc);
                comando.Parameters.AddWithValue("@direccion", entidad.direccion);
                comando.Parameters.AddWithValue("@telefono", entidad.telefono);
                comando.Parameters.AddWithValue("@email", entidad.email);
                comando.Parameters.AddWithValue("@website", entidad.website);

                totalRegistros = comando.ExecuteNonQuery();
            }
            catch
            {
                totalRegistros = -1;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return totalRegistros;
        } 

        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            SqlCommand comando = new SqlCommand("sp_empresa_listar", conexion.getConexion());
            comando.CommandType = CommandType.StoredProcedure;

            conexion.abrirConexion();
            dt.Load(comando.ExecuteReader());

            conexion.cerrarConexion();

            return dt;
        }
    }
}
