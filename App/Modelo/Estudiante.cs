using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modelo
{
    internal class Estudiante
    {
        public int Id_Estudiante { get; set; }
        public string Nombre { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public Estudiante(string nombre, string apellido_paterno, string apellido_materno)
        {
            Nombre = nombre;
            Apellido_Paterno = apellido_paterno;
            Apellido_Materno = apellido_materno;
        }

        public Estudiante(int id_estudiante, string nombre, string apellido_paterno, string apellido_materno)
        {
            Id_Estudiante = id_estudiante;
            Nombre = nombre;
            Apellido_Paterno = apellido_paterno;
            Apellido_Materno = apellido_materno;
        }
    }
}
