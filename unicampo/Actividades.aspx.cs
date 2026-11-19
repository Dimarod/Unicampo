using System;
using System.Collections.Generic;
using unicampo.entities;
using unicampo.Presenters;
using unicampo.Views;

namespace unicampo
{
    public partial class Actividades : System.Web.UI.Page, IActividadesView
    {
        private ActividadesPresenter _presenter;

        
        public string ActividadIdSeleccionada
        {
            get => hfActividadId.Value;
            set => hfActividadId.Value = value;
        }

        public string TituloActividad => txtTitulo.Text;
        public string DescripcionActividad => txtDescripcion.Text;
        public string FechaInicioActividad => txtFecha.Text;
        public string LugarActividad => txtLugar.Text;
        public string CuposActividad => txtCupos.Text;

        public int CalificacionSeleccionada => int.Parse(ddlCalificacion.SelectedValue);
        public string ComentarioResena => txtComentarioResena.Text;
        

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenter = new ActividadesPresenter(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioId"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                _presenter.Cargar();
                string rol = Session["Rol"]?.ToString();
                panelFormulario.Visible = (rol == "Administrador");
            }
        }

        public void MostrarActividades(IList<ActividadDto> actividades)
        {
            gvActividades.DataSource = actividades;
            gvActividades.DataBind();
            lblMensaje.Text = actividades.Count == 0 ? "Por ahora no hay actividades publicadas." : "Actividades disponibles: " + actividades.Count;
        }

        public void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = Server.HtmlEncode(mensaje);
        }

        public void LimpiarFormulario()
        {
            ActividadIdSeleccionada = "";
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            txtFecha.Text = "";
            txtLugar.Text = "";
            txtCupos.Text = "";
        }

        public void LlenarFormularioEdicion(ActividadDto actividad)
        {
            ActividadIdSeleccionada = actividad.ActividadId.ToString();
            txtTitulo.Text = actividad.Titulo;
            txtDescripcion.Text = actividad.Descripcion;
            txtFecha.Text = actividad.FechaInicio.ToString("yyyy-MM-ddTHH:mm");
            txtLugar.Text = actividad.Lugar;
            txtCupos.Text = actividad.Cupos.ToString();
        }

        // =================================================================
        // INICIO PUNTO 8.1: Métodos de Mostrar/Ocultar el panel
        // =================================================================
        public void MostrarPanelResena(string tituloActividad)
        {
            lblTituloResena.Text = tituloActividad;
            panelResena.Visible = true;
        }

        public void OcultarPanelResena()
        {
            panelResena.Visible = false;
            txtComentarioResena.Text = "";
            ddlCalificacion.SelectedIndex = 0;
            ActividadIdSeleccionada = "";
        }
        

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.Guardar();
        }

        protected void gvActividades_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                var actividades = new unicampo.BLL.ActividadService().ListarActividades();
                var actividadEditar = actividades.Find(a => a.ActividadId == id);
                if (actividadEditar != null) _presenter.SeleccionarParaEditar(actividadEditar);
            }
            else if (e.CommandName == "Cancelar")
            {
                _presenter.Cancelar(id);
            }
            else if (e.CommandName == "Inscribir")
            {
                int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
                _presenter.Inscribir(id, usuarioId);
            }
           
            else if (e.CommandName == "Calificar")
            {
                _presenter.PrepararResena(id);
            }
            else if (e.CommandName == "VerResenas")
            {
                var resenaService = new unicampo.BLL.ResenaService();
                gvResenas.DataSource = resenaService.ListarResenas(id);
                gvResenas.DataBind();
                panelListaResenas.Visible = true;
            }

        }

        protected void gvActividades_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
            {
                string rol = Session["Rol"]?.ToString();

                var btnEditar = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnEditar");
                var btnEliminar = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnEliminar");
                var btnInscribir = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnInscribir");

                var btnCalificar = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnCalificar");
                var btnVerResenas = (System.Web.UI.WebControls.LinkButton)e.Row.FindControl("btnVerResenas");

                if (rol == "Estudiante")
                {
                    if (btnEditar != null) btnEditar.Visible = false;
                    if (btnEliminar != null) btnEliminar.Visible = false;
                    if (btnVerResenas != null) btnVerResenas.Visible = false;
                }
                else if (rol == "Administrador")
                {
                    if (btnInscribir != null) btnInscribir.Visible = false;
                    if (btnCalificar != null) btnCalificar.Visible = false; 
                }
            
            }
        }

     
        protected void btnEnviarResena_Click(object sender, EventArgs e)
        {
            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);
            _presenter.GuardarResena(usuarioId);
        }

        protected void btnCancelarResena_Click(object sender, EventArgs e)
        {
            OcultarPanelResena();
        }
        protected void btnCerrarListaResenas_Click(object sender, EventArgs e)
        {
            panelListaResenas.Visible = false;
        }

    } 
} 