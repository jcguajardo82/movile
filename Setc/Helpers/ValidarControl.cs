using System;

namespace Setc.Helpers
{
    public static class ValidarControl
    {
        public static string ValidarInput(string texto, string input)
        {
            string Mensaje = String.Empty;
            if (String.IsNullOrEmpty(texto) || String.IsNullOrWhiteSpace(texto))
            {
                switch (input)
                {
                    case "Usuario":
                        Mensaje = "Ingrese su usuario.";
                        break;
                    case "Password":
                        Mensaje = "Ingrese la contraseña.";
                        break;
                }
            }
            return Mensaje;
        }
    }
}