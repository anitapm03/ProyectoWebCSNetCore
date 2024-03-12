using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region PROC
/*CREATE PROCEDURE SP_INSERT_SALA 
(@DIRECCION NVARCHAR(100),
@NOMBRE NVARCHAR(50),
@PROV INT)
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDSALA)+1 FROM SALA
	INSERT INTO SALA VALUES(@ID, @DIRECCION, @NOMBRE, @PROV)
GO

 
 CREATE PROCEDURE SP_ELIMINARSALA
(@ID INT)
AS
	DELETE FROM SALA WHERE IDSALA = @ID;
GO


CREATE PROCEDURE SP_UPDATE_SALA
(@ID INT,
@NOMBRE NVARCHAR(50),
@DIRECCION NVARCHAR(100),
@PROV INT)
AS
	UPDATE SALA SET NOMBRE = @NOMBRE,
	DIRECCION = @DIRECCION, IDPROVINCIA = @PROV
	WHERE IDSALA = @ID
GO
 */
#endregion
namespace ProyectoWebCSNetCore.Repositories
{

    public class RepositorySalas
    {
        private CSContext context;

        public RepositorySalas(CSContext context)
        {
            this.context = context;
        }

        public List<Sala> GetSalas()
        {
            var consulta = from datos in this.context.Salas
                           select datos;
            return consulta.ToList();
        }

        public Sala FindSala(int id)
        {
            var consulta = from datos in this.context.Salas
                           where datos.IdSala == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void CrearSala(string direccion, string nombre, int prov)
        {
            string sql = "SP_INSERT_SALA @DIRECCION, @NOMBRE, @PROV";

            SqlParameter dir = new SqlParameter("@DIRECCION", direccion);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter provincia = new SqlParameter("@PROV", prov);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, nom, dir, provincia);
        }

        public void EliminarSala(int id)
        {
            string sql = "SP_ELIMINARSALA @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);

        }

        public void EditarSala(int id, string nombre, 
            string direccion, int prov)
        {
            string sql = "SP_UPDATE_SALA  @ID, @NOMBRE, @DIRECCION, @PROV";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter dir = new SqlParameter("@DIRECCION", direccion);
            SqlParameter provincia = new SqlParameter("@PROV", prov);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, dir, provincia);
        }
    }
}
