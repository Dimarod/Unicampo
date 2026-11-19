using System;
using unicampo.DAL;
using unicampo.entities;

namespace unicampo.BLL
{
    public class AuthService
    {
        private readonly UsuarioRepository _repository;

        public AuthService()
        {
            _repository = new UsuarioRepository();
        }

        public UsuarioDto IniciarSesion(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new Exception("El correo es obligatorio.");

            var usuario = _repository.ObtenerPorCorreo(correo);

            if (usuario == null)
                throw new Exception("El correo no está registrado en el sistema.");

            return usuario;
        }
    }
}