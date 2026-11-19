using System;
using System.Web;
using unicampo.BLL;
using unicampo.Views;

namespace unicampo.Presenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly AuthService _service;

        public LoginPresenter(ILoginView view)
        {
            _view = view;
            _service = new AuthService();
        }

        public void Autenticar()
        {
            try
            {
                var usuario = _service.IniciarSesion(_view.CorreoUsuario);

                // Guardamos los datos importantes en la sesión del navegador
                HttpContext.Current.Session["UsuarioId"] = usuario.UsuarioId;
                HttpContext.Current.Session["Nombre"] = usuario.Nombre;
                HttpContext.Current.Session["Rol"] = usuario.Rol;

                _view.RedirigirAInicio();
            }
            catch (Exception ex)
            {
                _view.MostrarError(ex.Message);
            }
        }
    }
}