using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROCEDIMIENTOS
    //recupera la información de un evento completo
    //concierto/sala/provincia
    /*ALTER VIEW V_EVENTOS
    AS

        SELECT C.IDCONCIERTO, C.NOMBRE, C.FECHA, C.IMAGEN, C.ENTRADAS,
        C.GRUPO, S.DIRECCION, S.NOMBRE AS NOMBRESALA, 
		P.NOMBRE AS NOMBREPROVNCIA
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

alter PROCEDURE SP_UPDATE_CONCIERTO
(@ID INT,
@NOMBRE NVARCHAR(50),
@FECHA DATETIME,
@ENTRADAS NVARCHAR(200),
@IDSALA INT,
@GRUPO NVARCHAR(200))
AS
	UPDATE CONCIERTO SET NOMBRE = @NOMBRE,
	FECHA = @FECHA, ENTRADAS = @ENTRADAS,
	IDSALA = @IDSALA, GRUPO = @GRUPO
	where IDCONCIERTO = @ID
GO

alter PROCEDURE SP_UPDATE_CONCIERTO_FOTO
(@ID INT,
@NOMBRE NVARCHAR(50),
@FECHA DATETIME,
@IMAGEN NVARCHAR(200),
@ENTRADAS NVARCHAR(200),
@IDSALA INT,
@GRUPO NVARCHAR(200))
AS
	UPDATE CONCIERTO SET NOMBRE = @NOMBRE,
	FECHA = @FECHA, IMAGEN = @IMAGEN,
	ENTRADAS = @ENTRADAS,
	IDSALA = @IDSALA, GRUPO = @GRUPO
	where IDCONCIERTO = @ID
GO

    CREATE PROCEDURE SP_DESTACAR_EVENTO
(@IDCONCIERTO INT)
AS
	UPDATE CONCIERTO SET DESTACADO = 1
	WHERE IDCONCIERTO = @IDCONCIERTO
GO

    CREATE PROCEDURE SP_NODESTACAR_EVENTO
(@IDCONCIERTO INT)
AS
	UPDATE CONCIERTO SET DESTACADO = 0
	WHERE IDCONCIERTO = @IDCONCIERTO
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

        public List<Evento> GetDestacados()
        {
            var consulta = from datos in this.context.Eventos
                           where datos.Destacado == true
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

        public Concierto FindConcierto(int id)
        {
            var consulta = from datos in this.context.Conciertos
                           where datos.IdConcierto == id
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

        public void EditarConcierto
            (int id, string nombre, DateTime fecha,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_UPDATE_CONCIERTO @ID, @NOMBRE, @FECHA, " +
                " @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, fe, ent, ids, gr);
        }

        public void EditarConciertoFoto
            (int id, string nombre, DateTime fecha, string foto,
            string entradas, int sala, string grupo)
        {
            string sql = "SP_UPDATE_CONCIERTO_FOTO @ID, @NOMBRE, @FECHA, " +
            " @FOTO, @ENTRADAS, @IDSALA, @GRUPO";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter fe = new SqlParameter("@FECHA", fecha);
            SqlParameter fo = new SqlParameter("@FOTO", foto);
            SqlParameter ent = new SqlParameter("@ENTRADAS", entradas);
            SqlParameter ids = new SqlParameter("@IDSALA", sala);
            SqlParameter gr = new SqlParameter("@GRUPO", grupo);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, fe, fo, ent, ids, gr);
        }

        public void EliminarConcierto(int id)
        {
            string sql = "SP_ELIMINARCONCIERTO @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public void DestacarEvento(int idconcierto)
        {
            string sql = "SP_DESTACAR_EVENTO @IDCONCIERTO";
            SqlParameter pid = new SqlParameter("@IDCONCIERTO", idconcierto);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public void NoDestacarEvento(int idconcierto)
        {
            string sql = "SP_NODESTACAR_EVENTO @IDCONCIERTO";
            SqlParameter pid = new SqlParameter("@IDCONCIERTO", idconcierto);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }
    }
}
