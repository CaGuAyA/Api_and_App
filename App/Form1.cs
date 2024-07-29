using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App.Controlador;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace App
{
    public partial class Form1 : Form
    {
        private readonly Controlador.Controlador _controlador;

        public Form1()
        {
            InitializeComponent();
            _controlador = new Controlador.Controlador();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await _controlador.LoadAllTables(dataGridView1);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse("2");

                var pdfBytes = await _controlador.GeneratePdfAsync(id);
                string filePath = Path.Combine(Path.GetTempPath(), "generated.pdf");
                File.WriteAllBytes(filePath, pdfBytes);

                OpenPdfInBrowser(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenPdfInBrowser(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await _controlador.AddItems(textBox2.Text, int.Parse(textBox3.Text), textBox4.Text, textBox5.Text, textBox6.Text);
            await _controlador.LoadAllTables(dataGridView1);
        }
    }
}
