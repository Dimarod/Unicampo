<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="unicampo.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>UniCampus | Iniciar Sesión</title>
    <meta charset="utf-8" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600&display=swap" rel="stylesheet">
    <style>
        body { font-family: 'Inter', sans-serif; background-color: #f8fafc; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; }
        .login-card { background: white; padding: 40px; border-radius: 12px; box-shadow: 0 10px 15px -3px rgb(0 0 0 / 0.1); width: 100%; max-width: 400px; text-align: center; }
        h1 { color: #0f2c59; font-size: 2rem; margin-bottom: 5px; }
        p { color: #64748b; margin-bottom: 30px; }
        .form-control { width: 100%; padding: 12px; border: 1px solid #e2e8f0; border-radius: 8px; margin-bottom: 20px; box-sizing: border-box; font-size: 1rem; }
        .btn-primary { background: #0f2c59; color: white; border: none; padding: 12px; border-radius: 8px; width: 100%; font-size: 1rem; font-weight: 600; cursor: pointer; transition: background 0.2s; }
        .btn-primary:hover { background: #1a458a; }
        .error-msg { color: #dc2626; font-size: 0.9rem; margin-bottom: 15px; display: block; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-card">
            <h1>UniCampus</h1>
            <p>Ingresa para gestionar tus actividades</p>
            
            <asp:Label ID="lblError" runat="server" CssClass="error-msg"></asp:Label>
            
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="tucorreo@ejemplo.edu" TextMode="Email"></asp:TextBox>
            
            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-primary" OnClick="btnIngresar_Click" />
            
            <div style="margin-top: 20px; font-size: 0.8rem; color: #94a3b8;">
                Correo de prueba: estudiante.demo@ejemplo.edu
                
            </div>
              
            <div style="margin-top: 20px; font-size: 0.8rem; color: #94a3b8;">
                Correo de prueba de administrador: admin@ejemplo.edu
                
            </div>
        </div>
    </form>
</body>
</html>