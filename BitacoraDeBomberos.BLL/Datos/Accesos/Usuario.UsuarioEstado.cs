namespace BitacoraDeBomberos.BLL.Datos.Accesos {

    public enum UsuarioEstado {

        /// <summary>
        ///     El primer acceso del usuario no ha sido registrado. 
        /// </summary>
        AunSinAcceder = 0,

        /// <summary>
        ///     El usuario a sido inactivado y no debe de acceder al sistema. 
        /// </summary>
        Inactivo = 1,

        /// <summary>
        ///     El usuario tiene completo acceso al sistema. 
        /// </summary>
        Activo = 2,
    }
}