using Microsoft.AspNetCore.Http.HttpResults;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Helpers;

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
    GO*/

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

        public async Task<Usuario> LogInUserAsync(string email, string password)
        {
            Usuario user = await
                this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp =
                    HelperCryptography.EncryptPassword(password, salt);
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
            Usuario user = await
                this.context.Usuarios.FirstOrDefaultAsync(x => x.TokenMail == token);
            user.Activo = true;
            user.TokenMail = "";
            await this.context.SaveChangesAsync();
        }

    }
}
