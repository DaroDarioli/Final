using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcial
{
    delegate void CargarRichTextBoxCallback(string datos);
    public delegate void SuperHilo(object o, EventArgs e);
    public delegate Queue<Base> TraerLista();

    public partial class FRMPrincipal : Form
    {

        List<Base> listaElementos;
        public TraerLista traigolista;
        public event SuperHilo miHilo;


        public FRMPrincipal()
        {
            InitializeComponent();
            this.Text = "Sabado";
            this.listaElementos = new List<Base>();
            this.traigolista = Base.LeerDatos<Base>;
            Base.CargarBaseEvent += MetodoCargarBase;
            


    }

        private void btnPunto1_Click(object sender, EventArgs e)
        {
            Base derUno1 = new DerivadaUno(10, 11, 12);//103
            DerivadaUno derUno2 = new DerivadaUno(1, 2, 3);//103
            Base derDos1 = new DerivadaDos();//2

            listaElementos.Add(derUno1);
            listaElementos.Add(derUno2);
            listaElementos.Add(derDos1);

            StringBuilder sb = new StringBuilder();
            foreach(Base b in listaElementos)
            {
                sb.AppendLine(b.VersionFull);
            }
            CargarRichTextBox(sb.ToString());


        }

        private void btnPunto2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sólo para quienes recuperan");
        }

        private void btnPunto3_Click(object sender, EventArgs e)
        {

            foreach(Base b in listaElementos)
            {
                Base.GuardarDatos(b);
            }


            Queue < Base > cola = Base.LeerDatos<Base>();

            foreach(Base b in cola)
            {
                rtbTextoSalida.AppendText(b.VersionFull);
            }


        }

        private void btnPunto4_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => Base.EjecutarEvento(listaElementos));
            thread.Start();

            // miHilo.Invoke(this, new EventArgs());        

        }   
        
        public void MetodoCargarBase(List<Base> lista)
        {

            foreach(Base b in lista)
            {
                Base.GuardarDatos(b);
            }

           
        }


        private void btnPunto5_Click(object sender, EventArgs e)
        {


            ManejadorEvento(this.traigolista());

        }

        public void CargarRichTextBox(string datos)
        {

            if (rtbTextoSalida.InvokeRequired)
            {
                CargarRichTextBoxCallback d = new CargarRichTextBoxCallback(CargarRichTextBox);
                this.Invoke(d, new object[] { datos });
            }
            else
            {
                rtbTextoSalida.AppendText(datos);

            }


             
            
        }


        private void ManejadorEvento(Queue<Base> cola)
        {
            foreach(Base b in cola)
            {
                rtbTextoSalida.AppendText(b.VersionFull);
            }
        }

       

    }
}
