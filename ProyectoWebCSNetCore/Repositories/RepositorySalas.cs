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
GO*/
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

        public void CrearSala(string direccion, string nombre, int prov)
        {
            string sql = "SP_INSERT_SALA @DIRECCION, @NOMBRE, @PROV";

            SqlParameter dir = new SqlParameter("@DIRECCION", direccion);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter provincia = new SqlParameter("@PROV", prov);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, nom, dir, provincia);
        }

        
    }
}
