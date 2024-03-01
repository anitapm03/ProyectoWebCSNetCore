using ProyectoWebCSNetCore.Models;
using System.Diagnostics.Metrics;

namespace ProyectoWebCSNetCore.Repositories
{
    #region PROCEDIMIENTOS
    //recupera la información de un evento completo
    //concierto/sala/provincia
    //ALTER PROCEDURE SP_EVENTOS
    //AS

    //    SELECT C.IDCONCIERTO, C.NOMBRE, C.FECHA, C.IMAGEN, C.ENTRADAS,
    //    S.DIRECCION, S.NOMBRE, P.NOMBRE
    //    FROM CONCIERTO C

    //    INNER JOIN SALA S

    //    ON C.IDSALA = S.IDSALA

    //    INNER JOIN PROVINCIA P

    //    ON S.IDPROVINCIA = P.IDPROVINCIA
    //GO

    public class RepositoryConciertos
    {
    }
}
