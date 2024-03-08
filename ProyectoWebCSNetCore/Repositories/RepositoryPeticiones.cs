using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using System.Security.Cryptography;
#region PROC
/*CREATE PROCEDURE SP_INSERT_PETICION (
@NOMBRE NVARCHAR(50),
@PROV INT,
@FECHA DATETIME,
@TELF NVARCHAR(15))
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDPETICION) + 1 FROM PETICIONEVENTO
	INSERT INTO PETICIONEVENTO VALUES (@ID, @NOMBRE, @PROV,
    @FECHA, @TELF)
GO*/
#endregion
namespace ProyectoWebCSNetCore.Repositories
{
    public class RepositoryPeticiones
    {
        private CSContext context;

        public RepositoryPeticiones(CSContext context)
        {
            this.context = context;
        }

        public List<PeticionEvento> GetPeticiones()
        {
            var consulta = from datos in this.context.Peticiones
                           select datos;
            return consulta.ToList();
        }

        public void InsertarPeticion
            (string nombre, int idprovincia, DateTime fecha, string telefono)
        {
            string sql = "SP_INSERT_PETICION @NOMBRE, @PROV, @FECHA, @TELF";

            SqlParameter pnom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pprov = new SqlParameter("@PROV", idprovincia);
            SqlParameter pfecha = new SqlParameter("@FECHA", fecha);
            SqlParameter ptelf = new SqlParameter("@TELF", telefono);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pnom, pprov, pfecha, ptelf);
        }

        public List<Peticion> GetListaPeticiones()
        {
            var consulta = from datos in this.context.ListaPeticiones
                           select datos;
            return consulta.ToList();
        }
    }
}
