using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargadorFotos
{
    public class MiClase
    {
        public string foto;
    }

    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;

        public string ruta = "";
        
        public Form1()
        {
            InitializeComponent();
            ConfigurarOpenSaveFileDialog();
            this.button1.Click += new EventHandler(CargoFoto_Click);
        }


        public void ActualizarFoto(String camino)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(camino);
            }
            catch (Exception c)
            {
                MessageBox.Show(c.Message);
            }
        }


        private void CargoFoto_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            this.ruta = this.openFileDialog.FileName.ToString();
            ActualizarFoto(this.ruta);
        }


        private void ConfigurarOpenSaveFileDialog()
        {
            this.openFileDialog = new OpenFileDialog();
            this.openFileDialog.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            this.openFileDialog.Multiselect = false;
        }
    }
}
