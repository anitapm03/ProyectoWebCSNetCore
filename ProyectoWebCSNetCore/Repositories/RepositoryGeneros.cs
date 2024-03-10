using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
#region PROC
/*
CREATE PROCEDURE SP_INSERTGENERO
(@NOMBRE NVARCHAR(30))
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDGENERO)+1 from GENERO
	INSERT INTO GENERO VALUES(@ID, @NOMBRE)
GO

CREATE PROCEDURE SP_ELIMINARGENERO
(@ID INT)
AS
	DELETE FROM GENERO WHERE IDGENERO = @ID;
GO
*/
#endregion
namespace ProyectoWebCSNetCore.Repositories
{
    public class RepositoryGeneros
    {
        private CSContext context;

        public RepositoryGeneros(CSContext context)
        {
            this.context = context;
        }

        public List<Genero> GetGeneros()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return consulta.ToList();
        } 

        public void InsertGenero(string nombre)
        {
            string sql = "SP_INSERTGENERO @NOMBRE";
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);

            this.context.Database.ExecuteSqlRaw(sql, nom);
        }

        public void EliminarGenero(int id)
        {
            string sql = "SP_ELIMINARGENERO @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
