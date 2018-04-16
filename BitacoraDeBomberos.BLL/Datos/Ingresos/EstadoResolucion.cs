using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Ingresos {

    public enum EstadoResolucion {

        SinEspecificar = 0,


        No_disponible = -1,

        primer_id_invalido = -101,
        segundo_id_invalido = -102,

        Ingresado = 1,
        Error_ingresado = 51,

        Actualizado = 501,
        Sin_cambios = 502,
        Error_actualizar = 551,

        Eliminado = 1001,
        Error_eliminado = 1051,
        Error_eliminar_referencias = 1052,
    }
}