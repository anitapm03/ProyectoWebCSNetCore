using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROC
    /*CREATE PROCEDURE SP_INSERT_PUBLI
    (@USER INT,
    @TEXTO NVARCHAR(600),
    @IMAGEN NVARCHAR(200),
    @FECHA DATETIME)
    AS
	    DECLARE @ID INT
	    SELECT @ID = MAX(IDPUBLICACION)+1 FROM PUBLICACION
	    INSERT INTO PUBLICACION VALUES(@ID,@USER, @TEXTO, 
	    @IMAGEN, @FECHA)
    GO*/
    #endregion
    public class RepositoryPublicaciones
    {
        private CSContext context;

        public RepositoryPublicaciones(CSContext context)
        {
            this.context = context;
        }

        public List<UserPubli> GetPublicaciones()
        {
            var consulta = from datos in context.PublicacionesUsuarios
                           orderby datos.Fecha descending
                           select datos;

            return consulta.ToList();
        }

        public void InsertPubli( int iduser, string texto,
            string imagen, DateTime fecha)
        {
            string sql = "SP_INSERT_PUBLI @USER, @TEXTO, @IMAGEN, @FECHA";

            SqlParameter user = new SqlParameter("@USER", iduser);
            SqlParameter txt = new SqlParameter("@TEXTO", texto);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);

            this.context.Database.ExecuteSqlRaw(sql, user,txt, img, fe);
        }
    }
}
