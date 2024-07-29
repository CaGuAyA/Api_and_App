using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using App.Modelo;
using App.Servisio;

namespace App.Controlador
{
    internal class Controlador
    {
        private readonly Servicios _servicios;

        public Controlador()
        {
            _servicios = new Servicios();
        }

        public async Task LoadAllTables(DataGridView dataGridView)
        {
            var tables = await _servicios.GetAllTables();
            dataGridView.DataSource = tables;
        }

        public async Task<byte[]> GeneratePdfAsync(int id)
        {
            return await _servicios.GeneratePdfAsync(id);
        }

        
        public async Task AddItems(string nombre_curso, int id_estudiante, string nombre, string apellido_paterno, string apellido_materno)
        {
            Curso curso = new Curso(nombre_curso, id_estudiante);
            Estudiante estudiante = new Estudiante(nombre, apellido_paterno, apellido_materno);
            CreateUpdateRequest cur = new CreateUpdateRequest{ curso = curso, estudiante = estudiante };
            await _servicios.CreateProductAsync(cur);
        }

        /*
        public async Task UpdateProduct(int id, string name, decimal price)
        {
            var product = new Producto { Id = id, Nombre = name, Precio = price };
            await _apiService.UpdateProductAsync(id, product);
        }

        public async Task DeleteProduct(int id)
        {
            await _apiService.DeleteProductAsync(id);
        }
        */
    }
}
