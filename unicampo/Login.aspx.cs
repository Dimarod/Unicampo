using System;
using unicampo.Presenters;
using unicampo.Views;

namespace unicampo
{
    public partial class Login : System.Web.UI.Page, ILoginView
    {
        private LoginPresenter _presenter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new LoginPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack && Session["UsuarioId"] != null)
            {
                Response.Redirect("Actividades.aspx");
            }
        }

        public string CorreoUsuario => txtCorreo.Text.Trim();

        public void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
        }

        public void RedirigirAInicio()
        {
            Response.Redirect("Actividades.aspx");
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            _presenter.Autenticar();
        }
    }
}