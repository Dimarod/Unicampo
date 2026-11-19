<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Actividades.aspx.cs" Inherits="unicampo.Actividades" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>UniCampus | Actividades Académicas</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600&display=swap" rel="stylesheet">
    <style>
        :root {
            --primary: #0f2c59;
            --primary-hover: #1a458a;
            --bg: #f8fafc;
            --surface: #ffffff;
            --text-main: #1e293b;
            --text-muted: #64748b;
            --border: #e2e8f0;
            --radius: 12px;
            --shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.05);
        }

        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
        }

        body {
            font-family: 'Inter', sans-serif;
            background-color: var(--bg);
            color: var(--text-main);
            padding: 40px 20px;
            line-height: 1.6;
        }

        .container {
            max-width: 1100px;
            margin: 0 auto;
        }

        .header {
            margin-bottom: 40px;
            text-align: center;
        }

            .header h1 {
                font-size: 2.5rem;
                font-weight: 600;
                color: var(--primary);
                letter-spacing: -0.02em;
            }

            .header p {
                color: var(--text-muted);
                font-size: 1.1rem;
                margin-top: 8px;
            }


        .card {
            background: var(--surface);
            border-radius: var(--radius);
            box-shadow: var(--shadow);
            padding: 30px;
            margin-bottom: 30px;
            border: 1px solid var(--border);
        }

        .card-title {
            font-size: 1.25rem;
            font-weight: 600;
            color: var(--primary);
            margin-bottom: 24px;
            padding-bottom: 12px;
            border-bottom: 2px solid var(--bg);
        }


        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin-bottom: 24px;
        }

        .form-group {
            display: flex;
            flex-direction: column;
            gap: 8px;
        }

            .form-group.wide {
                grid-column: 1 / -1;
            }

            .form-group label {
                font-size: 0.875rem;
                font-weight: 500;
                color: var(--text-main);
            }

        .form-control {
            padding: 12px 14px;
            border: 1px solid var(--border);
            border-radius: 8px;
            font-family: inherit;
            font-size: 0.95rem;
            transition: all 0.2s;
            background: #fdfdfd;
        }

            .form-control:focus {
                outline: none;
                border-color: var(--primary);
                box-shadow: 0 0 0 3px rgba(15, 44, 89, 0.1);
                background: #fff;
            }


        .btn-primary {
            background: var(--primary);
            color: white;
            border: none;
            padding: 12px 28px;
            border-radius: 8px;
            font-weight: 500;
            font-size: 1rem;
            cursor: pointer;
            transition: background 0.2s, transform 0.1s;
        }

            .btn-primary:hover {
                background: var(--primary-hover);
            }

            .btn-primary:active {
                transform: translateY(1px);
            }

        .btn-container {
            display: flex;
            justify-content: flex-end;
        }


        .table-container {
            overflow-x: auto;
            border-radius: 8px;
            border: 1px solid var(--border);
        }

        .tabla {
            width: 100%;
            border-collapse: collapse;
            text-align: left;
        }

            .tabla th {
                background-color: var(--bg);
                color: var(--text-muted);
                font-weight: 600;
                font-size: 0.85rem;
                text-transform: uppercase;
                letter-spacing: 0.05em;
                padding: 16px;
                border-bottom: 1px solid var(--border);
            }

            .tabla td {
                padding: 16px;
                border-bottom: 1px solid var(--border);
                color: var(--text-main);
                font-size: 0.95rem;
            }

            .tabla tr:last-child td {
                border-bottom: none;
            }

            .tabla tr:hover td {
                background-color: #f8fafc;
            }


        .mensaje {
            display: block;
            margin-bottom: 24px;
            padding: 12px 16px;
            border-radius: 8px;
            background: #eff6ff;
            color: #1e3a8a;
            border: 1px solid #bfdbfe;
            font-size: 0.95rem;
            font-weight: 500;
            text-align: center;
        }

            .mensaje:empty {
                display: none;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

            <div class="header">
                <h1>UniCampus</h1>
                <p>Gestión y Registro de Actividades Académicas</p>
            </div>


            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />


            <div class="card" id="panelFormulario" runat="server">
                <h2 class="card-title">Registrar Nueva Actividad</h2>

                <div class="form-grid">
                    <div class="form-group wide">
                        <label>Título de la actividad</label>
                        <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej. Seminario de Inteligencia Artificial"></asp:TextBox>
                    </div>

                    <div class="form-group wide">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Detalles de la actividad, objetivos, etc..."></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Fecha y hora de inicio</label>
                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Lugar o Enlace</label>
                        <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control" placeholder="Ej. Auditorio Principal"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label>Cupos Disponibles</label>
                        <asp:TextBox ID="txtCupos" runat="server" CssClass="form-control" TextMode="Number" placeholder="0"></asp:TextBox>
                    </div>
                </div>

                <div class="btn-container">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Actividad" CssClass="btn-primary" OnClick="btnGuardar_Click" />
                </div>
            </div>


            <div class="card">
                <h2 class="card-title">Actividades Publicadas</h2>

                <div class="table-container">

                    <asp:HiddenField ID="hfActividadId" runat="server" />

                    <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="False"
                        CssClass="tabla" GridLines="None" EmptyDataText="No hay actividades publicadas."
                        DataKeyNames="ActividadId" OnRowCommand="gvActividades_RowCommand" OnRowDataBound="gvActividades_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="Titulo" HeaderText="Actividad" />
                            <asp:BoundField DataField="FechaInicio" HeaderText="Fecha y hora" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="Cupos" HeaderText="Cupos" />

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar"
                                        CommandArgument='<%# Eval("ActividadId") %>' Text="Editar"
                                        ForeColor="#0f2c59" Font-Bold="true" Style="margin-right: 15px; text-decoration: none;" />

                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Cancelar"
                                        CommandArgument='<%# Eval("ActividadId") %>' Text="Eliminar"
                                        ForeColor="#dc2626" Font-Bold="true" Style="text-decoration: none;"
                                        OnClientClick="return confirm('¿Seguro que deseas cancelar esta actividad? Ya no aparecerá en el listado.');" />
                                    <asp:LinkButton ID="btnInscribir" runat="server" CommandName="Inscribir"
                                        CommandArgument='<%# Eval("ActividadId") %>' Text="Inscribirme"
                                        ForeColor="#16a34a" Font-Bold="true" Style="text-decoration: none;" />
                                    <asp:LinkButton ID="btnCalificar" runat="server" CommandName="Calificar"
                                        CommandArgument='<%# Eval("ActividadId") %>' Text="Calificar"
                                        ForeColor="#ca8a04" Font-Bold="true" Style="margin-left: 15px; text-decoration: none;" />
                                    <asp:LinkButton ID="btnVerResenas" runat="server" CommandName="VerResenas"
                                        CommandArgument='<%# Eval("ActividadId") %>' Text="Ver Reseñas"
                                        ForeColor="#2563eb" Font-Bold="true" Style="text-decoration: none;" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div id="panelResena" runat="server" visible="false" style="margin-top: 30px; padding: 20px; border: 1px solid #bfdbfe; border-radius: 8px; background: #eff6ff;">
                        <h3 style="color: #1e3a8a; margin-bottom: 15px;">Dejar Reseña:
                            <asp:Label ID="lblTituloResena" runat="server"></asp:Label></h3>
                        <div class="form-grid">
                            <div class="form-group">
                                <label>Calificación</label>
                                <asp:DropDownList ID="ddlCalificacion" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="5">⭐⭐⭐⭐⭐ Excelente</asp:ListItem>
                                    <asp:ListItem Value="4">⭐⭐⭐⭐ Muy bueno</asp:ListItem>
                                    <asp:ListItem Value="3">⭐⭐⭐ Bueno</asp:ListItem>
                                    <asp:ListItem Value="2">⭐⭐ Regular</asp:ListItem>
                                    <asp:ListItem Value="1">⭐ Malo</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group wide">
                                <label>Comentario</label>
                                <asp:TextBox ID="txtComentarioResena" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                        <div style="margin-top: 15px;">
                            <asp:Button ID="btnEnviarResena" runat="server" Text="Enviar Reseña" CssClass="btn-primary" OnClick="btnEnviarResena_Click" />
                            <asp:Button ID="btnCancelarResena" runat="server" Text="Cancelar" CssClass="btn-primary" BackColor="#64748b" OnClick="btnCancelarResena_Click" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div id="panelListaResenas" runat="server" visible="false" style="margin-top: 30px; padding: 20px; border: 1px solid #e2e8f0; border-radius: 8px; background: white;">
            <h3 style="color: #0f2c59; margin-bottom: 15px;">Reseñas de la Actividad</h3>

            <asp:GridView ID="gvResenas" runat="server" AutoGenerateColumns="False" CssClass="tabla" GridLines="None" EmptyDataText="Aún no hay reseñas para esta actividad.">
                <Columns>
                    <asp:BoundField DataField="NombreEstudiante" HeaderText="Estudiante" />
                    <asp:BoundField DataField="Puntuacion" HeaderText="Estrellas (1-5)" />
                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" />
                </Columns>
            </asp:GridView>

            <div style="margin-top: 15px;">
                <asp:Button ID="btnCerrarListaResenas" runat="server" Text="Cerrar Reporte" CssClass="btn-primary" BackColor="#64748b" OnClick="btnCerrarListaResenas_Click" />
            </div>
        </div>
    </form>
</body>
</html>
