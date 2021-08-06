using System.Collections.Generic;

namespace Setc.Models
{
    public class CuestionarioModel
    {
        public int id { get; set; }
        public string pregunta { get; set; }
        public List<string> respuestas { get; set; }

    }
}