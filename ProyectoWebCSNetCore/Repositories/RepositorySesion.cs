using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Helpers;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using NuGet.Common;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROC
    /*ALTER PROCEDURE SP_INSERTAR_USUARIO
    (@ID INT,
    @NOMBRE NVARCHAR(30),
    @EMAIL NVARCHAR(50),
    @CONTRASENA VARBINARY(MAX),
    @BIO NVARCHAR(500),
    @IMAGEN NVARCHAR(200),
	@SALT NVARCHAR(50),
    @ACTIVO BIT,
    @TOKENMAIL nvarchar(20))
    AS
        SELECT @ID = MAX(IDUSUARIO) + 1 FROM USUARIO;
        INSERT INTO USUARIO VALUES(@ID, @NOMBRE, @EMAIL,
        @CONTRASENA, 0, @BIO , @IMAGEN, @SALT, @ACTIVO, @TOKENMAIL)
    GO
    
     alter PROCEDURE SP_FINDTOKEN_ACTIVATEUSER
    (@TOKEN NVARCHAR(MAX),
    @ACTIVO BIT)
    AS
	    DECLARE @ID INT;
	    SELECT @ID = IDUSUARIO FROM USUARIO
	    WHERE TOKENMAIL = @TOKEN;
	    UPDATE USUARIO SET TOKENMAIL = '', ACTIVO = @ACTIVO
	    WHERE IDUSUARIO = @ID
    GO
     
    CREATE PROCEDURE SP_FIND_EMAIL
    (@EMAIL NVARCHAR(50))
    AS
	    SELECT * FROM USUARIO
	    WHERE EMAIL = @EMAIL
    GO

    CREATE PROCEDURE SP_UPDATE_USER
    (@ID INT,
    @NOMBRE NVARCHAR(30),
    @EMAIL NVARCHAR(50),
    @BIO NVARCHAR(500))
    AS
	    UPDATE USUARIO SET NOMBRE = @NOMBRE,
	    EMAIL = @EMAIL, BIO = @BIO
	    WHERE IDUSUARIO = @ID
	    SELECT * FROM USUARIO 
	    WHERE IDUSUARIO = @ID
    GO
     
     */

    #endregion
    public class RepositorySesion
    {
        private CSContext context;
        public RepositorySesion(CSContext context) 
        {
            this.context = context;
        }

        public string InsertarUsuario
            (string nombre, string email, 
            string contrasena, string bio)
        {

            string sql = "SP_INSERTAR_USUARIO @ID, @NOMBRE, @EMAIL, " +
                "@CONTRASENA, @BIO, @IMAGEN, @SALT, @ACTIVO, @TOKENMAIL";

            SqlParameter pid = new SqlParameter("@ID", 1);
            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pmail = new SqlParameter("@EMAIL", email);

            string salt = HelperCryptography.GenerateSalt();
            byte[] passw = HelperCryptography.EncryptPassword(contrasena, salt);

            SqlParameter pcont = new SqlParameter("@CONTRASENA", passw);
            SqlParameter pbio = new SqlParameter("@BIO", bio);
            SqlParameter pimg = new SqlParameter("@IMAGEN", "defaultprofile.jpg");
            
            SqlParameter psalt = new SqlParameter("@SALT", salt);

            bool activo = false; 
            string token = HelperTools.GenerateTokenMail();

            SqlParameter pact = new SqlParameter("@ACTIVO", activo);
            SqlParameter ptoken = new SqlParameter("@TOKENMAIL", token);

            this.context.Database.ExecuteSqlRaw(sql, pid, pnom, pmail, 
                pcont, pbio, pimg, psalt, pact, ptoken);

            return token;
        }

        public async Task<Usuario> LogInUserAsync(string email, string contrasena)
        {
            string sql = "SP_FIND_EMAIL @EMAIL";

            SqlParameter pmail = new SqlParameter("@EMAIL", email);

            var consulta = this.context.Usuarios.FromSqlRaw(sql, pmail);

            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(contrasena, salt);
                byte[] passUser = user.Contrasena;
                bool response =
                    HelperCryptography.CompareArrays(temp, passUser);
                if (response == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task ActivateUserAsync(string token)
        {
            //BUSCAMOS EL USUARIO POR SU TOKEN
            //Usuario user = await this.FindUserTokenAsync(token);
            bool activo = true;

            SqlParameter ptoken = new SqlParameter("@TOKEN", token);
            SqlParameter pact = new SqlParameter("@ACTIVO", activo);
            string sql = "SP_FINDTOKEN_ACTIVATEUSER @TOKEN, @ACTIVO";
            

            this.context.Database.ExecuteSqlRaw(sql, ptoken, pact);
        }

        public async Task<Usuario> FindUserTokenAsync(string token)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.TokenMail == token
                           select datos;
            Usuario user = await consulta.FirstOrDefaultAsync();
            return user;
        }

        public async Task<Usuario> FindUserAsync(string idusuario)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.IdUsuario == int.Parse(idusuario)
                           select datos;
            Usuario user = await consulta.FirstOrDefaultAsync();
            return user;
        }

        public Usuario ActualizarInfoUsuario
            (int id, string nombre, string email, string bio)
        {
            string sql = "SP_UPDATE_USER @ID, @NOMBRE, @EMAIL, @BIO";


            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pmail = new SqlParameter("@EMAIL", email);
            SqlParameter pbio = new SqlParameter("@BIO", bio);

            var consulta = this.context.Usuarios.FromSqlRaw(sql, pid, pnom, pmail, pbio);

            Usuario user = consulta.AsEnumerable().FirstOrDefault();

            return user;
        }

    }
}
