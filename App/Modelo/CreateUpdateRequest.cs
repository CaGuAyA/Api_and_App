using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modelo
{
    internal class CreateUpdateRequest
    {
        public Curso curso { get; set; }
        public Estudiante estudiante { get; set; }
    }
}
