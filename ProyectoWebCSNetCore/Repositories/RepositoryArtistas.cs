﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoWebCSNetCore.Data;
using ProyectoWebCSNetCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
#region procedures
/*
 CREATE PROCEDURE SP_INSERT_ARTISTA
(@NOMBRE NVARCHAR(30),
@IMAGEN NVARCHAR(200),
@SPOTIFY NVARCHAR(200),
@DESCRIPCION NVARCHAR(500))
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDARTISTA)+1 FROM ARTISTA
	INSERT INTO ARTISTA VALUES(@ID, @NOMBRE, @IMAGEN,
	@SPOTIFY, @DESCRIPCION)
GO


CREATE PROCEDURE SP_ELIMINARARTISTA
(@ID INT)
AS
	DELETE FROM ARTISTA WHERE IDARTISTA = @ID;
GO

CREATE PROCEDURE SP_UPDATE_ARTISTA
(@ID INT,
@NOMBRE NVARCHAR(30),
@SPOTIFY NVARCHAR(200),
@DESCRIPCION NVARCHAR(500))
AS
	UPDATE ARTISTA SET NOMBRE = @NOMBRE,
	SPOTIFY = @SPOTIFY, DESCRIPCION	= @DESCRIPCION
	WHERE IDARTISTA = @ID 
GO


CREATE PROCEDURE SP_UPDATE_ARTISTA_FOTO
(@ID INT,
@NOMBRE NVARCHAR(30),
@IMAGEN NVARCHAR(200),
@SPOTIFY NVARCHAR(200),
@DESCRIPCION NVARCHAR(500))
AS
	UPDATE ARTISTA SET NOMBRE = @NOMBRE,
	IMAGEN = @IMAGEN,
	SPOTIFY = @SPOTIFY, DESCRIPCION	= @DESCRIPCION
	WHERE IDARTISTA = @ID 
GO
 */
#endregion
namespace ProyectoWebCSNetCore.Repositories
{
    public class RepositoryArtistas
    {

        private CSContext context;

        public RepositoryArtistas(CSContext context)
        {
            this.context = context;
        }

        public List<Artista> GetArtistas()
        {
            var consulta = from datos in this.context.Artistas
                           select datos;
            return consulta.ToList();
        }

        public Artista FindArtista(int id)
        {
            var consulta = from datos in this.context.Artistas
                           where datos.IdArtista == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public void InsertarArtista(string nombre, string imagen, 
            string spotify, string descripcion)
        {
            string sql = "SP_INSERT_ARTISTA @NOMBRE, @IMAGEN," +
                "@SPOTIFY, @DESCRIPCION";
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter spt = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            this.context.Database.ExecuteSqlRaw(sql, nom, img, spt, desc);
        }

        public void EliminarArtista(int id)
        {
            string sql = "SP_ELIMINARARTISTA @ID";
            SqlParameter pid = new SqlParameter("@ID", id);

            this.context.Database.ExecuteSqlRaw(sql, pid);
        }

        public void EditarArtista
            (int id, string nombre, 
             string spotify, string descripcion)
        {

            string sql = "SP_UPDATE_ARTISTA @ID, @NOMBRE, @SPOTIFY, " +
                " @DESCRIPCION";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter sp = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, sp, desc);
        }

        public void EditarArtistaFoto
            (int id, string nombre, string imagen,
             string spotify, string descripcion)
        {
            string sql = "SP_UPDATE_ARTISTA_FOTO @ID, @NOMBRE, @IMAGEN," +
                " @SPOTIFY, @DESCRIPCION";

            SqlParameter pid = new SqlParameter("@ID", id);
            SqlParameter nom = new SqlParameter("@NOMBRE", nombre);
            SqlParameter img = new SqlParameter("@IMAGEN", imagen);
            SqlParameter sp = new SqlParameter("@SPOTIFY", spotify);
            SqlParameter desc = new SqlParameter("@DESCRIPCION", descripcion);

            var consulta = this.context.Database.ExecuteSqlRaw
                (sql, pid, nom, img, sp, desc);
        }
    }
}
