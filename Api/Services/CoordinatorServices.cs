using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Api.Data;
using Api.Models;

namespace Api.Services
{
    public class CoordinatorServices
    {
        private readonly AppDbContext _context;

        public CoordinatorServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JoinModels>> GetAllTables()
        {
            var query = from estudiante in _context.Estudiante
                        join curso in _context.Curso
                        on estudiante.Id_Estudiante equals curso.Id_Estudiante
                        select new JoinModels
                        {
                            Id = estudiante.Id_Estudiante,
                            Nombre = estudiante.Nombre ?? "",
                            Apellido_Paterno = estudiante.Apellido_Paterno ?? "",
                            Apellido_Materno = estudiante.Apellido_Materno ?? "",
                            Nombre_Curso = curso.Nombre_Curso ?? ""
                        };

            return await query.ToListAsync();
        }

        public async Task<JoinModels?> GetIdTable(int id)
        {
            var query = from estudiante in _context.Estudiante
                        join curso in _context.Curso
                        on estudiante.Id_Estudiante equals curso.Id_Estudiante
                        where estudiante.Id_Estudiante == id
                        select new JoinModels
                        {
                            Id = estudiante.Id_Estudiante,
                            Nombre = estudiante.Nombre ?? "",
                            Apellido_Paterno = estudiante.Apellido_Paterno ?? "",
                            Apellido_Materno = estudiante.Apellido_Materno ?? "",
                            Nombre_Curso = curso.Nombre_Curso ?? ""
                        };

            return await query.FirstOrDefaultAsync();
        }

        public async Task CreateItems(Curso curso, Estudiante estudiante)
        {
            // se envia primero lso que no contienen la clave foranea
            _context.Add(estudiante);
            // Obtenemos el valor maximo de su id
            var maxEstudianteId = await _context.Estudiante.MaxAsync(e => (int?)e.Id_Estudiante) ?? 0;

            curso.Id_Estudiante = maxEstudianteId + 1;
            // se envia despues los que contienen la clave foranea
            _context.Add(curso);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task<bool> UpdateItem(Curso curso, Estudiante estudiante)
        {
            try
            {
                _context.Entry(curso).State = EntityState.Modified;
                _context.Entry(estudiante).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            } catch (DbUpdateException e)
            {
                return false;
            } 
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var curso = await _context.Curso.FindAsync(id);
                var estudiante = await _context.Estudiante.FindAsync(id);

                if (curso != null && estudiante != null)
                {
                    _context.Curso.Remove(curso);
                    _context.Estudiante.Remove(estudiante);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<byte []> GenerarPDF(int id)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);

                        // Agregar contenido al PDF
                        document.Add(new Paragraph("Hello, World!")
                            .SetFontSize(24)
                            .SetBold());

                        document.Add(new Paragraph($"This is a sample PDF created using iText7.{id}"));

                        var list = new List()
                            .Add(new ListItem("Item 1"))
                            .Add(new ListItem("Item 2"))
                            .Add(new ListItem("Item 3"));
                        document.Add(list);

                        var table = new Table(3);
                        table.AddHeaderCell("Header 1");
                        table.AddHeaderCell("Header 2");
                        table.AddHeaderCell("Header 3");
                        table.AddCell("Cell 1");
                        table.AddCell("Cell 2");
                        table.AddCell("Cell 3");
                        document.Add(table);

                        document.Close();
                    }
                }

                // Retornar el contenido del PDF como un array de bytes
                return stream.ToArray();
            }
        }
    }
}
