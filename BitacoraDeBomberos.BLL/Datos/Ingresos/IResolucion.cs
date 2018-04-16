using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.BLL.Datos.Ingresos {

    public interface IResolucion {

        EstadoResolucion Estado {
            get;
        }

        Int32? OutID {
            get;
        }

        String Mensaje {
            get;
            set;
        }
    }
}