using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using System.Diagnostics.Metrics;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROCEDIMIENTOS
    //recupera la información de un evento completo
    //concierto/sala/provincia
    /*CREATE VIEW V_EVENTOS
    AS

        SELECT C.IDCONCIERTO, C.NOMBRE, C.FECHA, C.IMAGEN, C.ENTRADAS,
        S.DIRECCION, S.NOMBRE AS NOMBRESALA, P.NOMBRE AS NOMBREPROVNCIA
        FROM CONCIERTO C

       INNER JOIN SALA S

       ON C.IDSALA = S.IDSALA

        INNER JOIN PROVINCIA P

        ON S.IDPROVINCIA = P.IDPROVINCIA
    GO
    
     CREATE PROCEDURE SP_INSERT_CONCIERTO
    (@NOMBRE NVARCHAR(50),
    @FECHA DATETIME,
    @FOTO NVARCHAR(200),
    @ENTRADAS NVARCHAR(200),
    @IDSALA INT,
    @GRUPO NVARCHAR(200))
    AS
	    DECLARE @ID INT
	    SELECT @ID = MAX(IDCONCIERTO) + 1 FROM CONCIERTO
	    INSERT INTO CONCIERTO VALUES(@ID, @NOMBRE, @FECHA, @FOTO, @ENTRADAS, @IDSALA, 0, @GRUPO)
    GO
     
    
CREATE PROCEDURE SP_ELIMINARCONCIERTO
(@ID INT)
AS
	DELETE FROM CONCIERTO WHERE IDCONCIERTO = @ID;
GO
     */
    #endregion

    public class RepositoryConciertos
    {
        private CSContext context;

        public RepositoryConciertos(CSContext context)
        {
            this.context = context;
        }

        public List<Evento> GetEventos()
        {
            var consulta = from datos in this.context.Eventos
                           select datos;
            return consulta.ToList();
        }

        public Evento FindEvento(int idevento)
        {
            var consulta = from datos in this.context.Eventos
                           where datos.IdConcierto == idevento
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void InsertarConcierto(string nombre, DateTime fecha, string foto,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_INSERT_CONCIERTO @NOMBRE, @FECHA, " +
                "@FOTO, @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter fo = new SqlParameter("@FOTO", foto);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, nom, fe, fo, ent, ids, gr);
        }

        public void EliminarConcierto(int id)
        {
            string sql = "SP_ELIMINARCONCIERTO @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
