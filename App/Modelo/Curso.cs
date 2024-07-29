using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Modelo
{
    internal class Curso
    {
        public int Id_Curso { get; set; }
        public string Nombre_Curso { get; set; }
        public int Id_Estudiante { get; set; }

        public Curso(string nombre_curso, int id_estudiante)
        {
            Nombre_Curso = nombre_curso;
            Id_Estudiante = id_estudiante;
        }

        public Curso(int id_curso, string nombre_curso, int id_estudiante)
        {
            Id_Curso = id_curso;
            Nombre_Curso = nombre_curso;
            Id_Estudiante = id_estudiante;
        }
    }
}
