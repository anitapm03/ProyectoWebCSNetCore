using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;

namespace ProyectoWebCSNetCore.Repositories
{
    #region
    /*CREATE PROCEDURE ADD_ARTISTACONCIERTO
(@IDCONCIERTO INT, 
@IDARTISTA INT)
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDARTISTA_CONCIERTO) + 1 FROM ARTISTA_CONCIERTO
	INSERT INTO ARTISTA_CONCIERTO VALUES(@ID, @IDARTISTA, @IDCONCIERTO)
GO

CREATE PROCEDURE ADD_GENEROARTISTA
(@IDARTISTA INT, 
@IDGENERO INT)
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDARTISTA_GENERO) + 1 FROM ARTISTA_GENERO
	INSERT INTO ARTISTA_GENERO VALUES(@ID, @IDARTISTA, @IDGENERO)
GO*/
    #endregion
    public class RepositoryRelaciones
    {
        private CSContext context;

        public RepositoryRelaciones(CSContext context)
        {
            this.context = context;
        }

        /*public List<ArtistaConcierto> GetArtistaConcierto()
        {
            var consulta = from datos in context.RelacionesConcierto
                           select datos;
            return consulta.ToList();
        }*/

        public List<ArtistaConcierto> GetConciertosArtista(int idartista)
        {
            var consulta = from datos in context.RelacionesConcierto
                           where datos.IdArtista == idartista
                           select datos;
            return consulta.ToList();
        }

        public List<ArtistaConcierto> GetArtistasConcierto(int idconcierto)
        {
            var consulta = from datos in context.RelacionesConcierto
                           where datos.IdConcierto == idconcierto
                           select datos;
            return consulta.ToList();
        }

        public void InsertarArtistaConcierto(int idartista, int idconcierto)
        {
            string sql = "ADD_ARTISTACONCIERTO @ICONCIERTO, @IDARTISTA";
            SqlParameter pcon = new SqlParameter("@IDCONCIERTO", idconcierto);
            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);

            this.context.Database.ExecuteSqlRaw
                (sql, pcon, part);

        }

        public void InsertarArtistaGenero(int idartista, int idgenero)
        {
            string sql = "ADD_GENEROARTISTA @ICONCIERTO, @IDGENERO";
            
            SqlParameter part = new SqlParameter("@IDARTISTA", idartista);
            SqlParameter pgen = new SqlParameter("@IDGENERO", idgenero);

            this.context.Database.ExecuteSqlRaw
                (sql, part, pgen);
        }
        /*
        public List<ArtistaGenero> GetArtistaGenero()
        {
            var consulta = from datos in context.RelacionesGenero
                           select datos;
            return consulta.ToList();
        }
        */
        public List<ArtistaGenero> GetGenerosArtista(int idartista)
        {
            var consulta = from datos in context.RelacionesGenero
                           where datos.IdArtista == idartista
                           select datos;
            return consulta.ToList();
        }
    }
}
