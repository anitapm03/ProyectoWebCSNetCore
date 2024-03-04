using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROC
    /*CREATE PROCEDURE SP_INSERTAR_USUARIO
    (@ID INT,
    @NOMBRE NVARCHAR(30),
    @EMAIL NVARCHAR(50),
    @CONTRASENA NVARCHAR(30),
    @BIO NVARCHAR(500),
    @IMAGEN NVARCHAR(200))
    AS
        SELECT @ID = MAX(IDUSUARIO) + 1 FROM USUARIO;
        INSERT INTO USUARIO VALUES(@ID, @NOMBRE, @EMAIL,
        @CONTRASENA, 0, @BIO , @IMAGEN)
    GO*/

    #endregion
    public class RepositorySesion
    {
        private CSContext context;
        public RepositorySesion(CSContext context) 
        {
            this.context = context;
        }

        public void InsertarUsuario
            (string nombre, string email, 
            string contrasena, string bio)
        {
            string sql = "SP_INSERTAR_USUARIO @ID, @NOMBRE, @EMAIL, " +
                "@CONTRASENA, @BIO, @IMAGEN";

            SqlParameter pid = new SqlParameter("@ID", 1);
            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pmail = new SqlParameter("@EMAIL", email);
            SqlParameter pcont = new SqlParameter("@CONTRASENA", contrasena);
            SqlParameter pbio = new SqlParameter("@BIO", bio);
            SqlParameter pimg = new SqlParameter("@IMAGEN", "defaultprofile.jpg");

            this.context.Database.ExecuteSqlRaw(sql, pid, pnom, pmail, 
                pcont, pbio, pimg);

        }


    }
}
