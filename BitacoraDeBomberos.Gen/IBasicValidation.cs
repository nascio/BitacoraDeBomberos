using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitacoraDeBomberos.Gen {

    public interface IBasicValidation {

        #region Metodos

        void Validar (String propertyName, out String background, out Boolean error);

        #endregion
    }
}