                                                    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

namespace Entidades
{
    //Data Source=MUSEOGONZO\SQLEXPRESS;Initial Catalog=final-20171207;Integrated Security=True
    public static class PersonaDAO
    {
        #region Atributos
        private static SqlConnection _conexion;
        private static SqlCommand _comando;
        #endregion

        #region Constructores
        static PersonaDAO()
        {
           
            PersonaDAO._conexion = new SqlConnection(Properties.Settings.Default.CadenaConexion);//CadenaConexionMDE);//
            PersonaDAO._comando = new SqlCommand();
            PersonaDAO._comando.CommandType = System.Data.CommandType.Text;
            PersonaDAO._comando.Connection = PersonaDAO._conexion;
        }
        #endregion

        #region Métodos

        #region Getters
        public static Persona ObtienePersona()
        {
            bool TodoOk = false;
            Persona persona = null;

            try
            {
                PersonaDAO._comando.CommandText = "SELECT TOP 1 id,nombre,apellido,dni FROM Personas";
                PersonaDAO._conexion.Open();                              
                SqlDataReader oDr = PersonaDAO._comando.ExecuteReader();

                if (oDr.Read())
                {
                    // ACCEDO POR NOMBRE O POR INDICE
                    persona = new Persona(int.Parse(oDr["id"].ToString()), oDr["nombre"].ToString(), oDr["apellido"].ToString(), int.Parse(oDr["dni"].ToString()));
                }
                
                oDr.Close();
                TodoOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (TodoOk)
                    PersonaDAO._conexion.Close();
            }
            return persona;
        }
        #endregion

        #region Insertar Persona
        public static bool InsertaPersona(Persona p)
        {
            string sql = "INSERT INTO Personas (nombre,apellido,dni) VALUES(";
            sql = sql + "'" + p.Nombre + "','" + p.Apellido + "'," + p.DNI.ToString() + ")";

            return EjecutarNonQuery(sql);

        }
        #endregion

        #region Modificar Persona
        public static bool ModificaPersona(Persona p)
        {
            string sql = "UPDATE Personas SET nombre = '" + p.Nombre + "', apellido = '";
            sql = sql + p.Apellido + "', dni = " + p.DNI.ToString() + " WHERE id = " + p.ID.ToString();

            return EjecutarNonQuery(sql);
        }
        #endregion

        #region Eliminar Persona
        public static bool EliminaPersona(Persona p)
        {

            string sql = "DELETE FROM Personas WHERE id = " + p.ID.ToString();

            return EjecutarNonQuery(sql);
        }
        #endregion

        private static bool EjecutarNonQuery(string sql)
        {
            bool todoOk = false;
            try
            {
                PersonaDAO._comando.CommandText = sql;

                PersonaDAO._conexion.Open();
                PersonaDAO._comando.ExecuteNonQuery();
                todoOk = true;
            }
            catch (Exception e)
            {
                todoOk = false;
            }
            finally
            {
                if (todoOk)
                    PersonaDAO._conexion.Close();
            }
            return todoOk;
        }

        #endregion
    }


/* Crear Tabla en Base de Datos

USE [Final]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mascotas](
[id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
[Nombre] [nchar](10) NULL,
[patas] [numeric](18, 0) NULL
) ON [PRIMARY]
GO */

}
