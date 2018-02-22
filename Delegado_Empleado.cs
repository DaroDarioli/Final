using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olinuck.Dario.Aula20
{
    
    public delegate void DelegadoSueldo();

    public delegate void DelegadoSueldoNegativo();  

    public delegate void DelSueldoMejorado(Object o, EventArgs e);


    
    public class Empleado
    {       
        public string nombre;

        public string apellido;

        public int legajo;

        private double sueldo;

        
        public event DelegadoSueldo SueldoEvent;

        public event DelSueldoMejorado SueldoMejorado;

        public DelegadoSueldoNegativo SueldoNegativo;



        public double Sueldo
        {
            get { return sueldo; }
            set
            {
                if (value < 0)
                {
                    this.SueldoNegativo();
                    //throw new Exception("Sueldo negativo!!");
                }
                else if (value > 20000)
                {
                    this.SueldoMejorado(this, new EventArgs());
                }
                else if (value > 9500)
                {
                    this.SueldoEvent();
                }              
                else
                {
                    this.sueldo = value; 
                }
                
            
            }
        }
        
        public Empleado(string nom, string ape, int leg)
        {
            this.nombre = nom;
            this.apellido = ape;
            this.legajo = leg;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Nombre: " + this.nombre);
            sb.AppendLine("Apellido: " + this.apellido);
            sb.AppendLine("Legajo: " + this.legajo);
            sb.AppendLine("Sueldo: " + this.sueldo);

            return sb.ToString();
        }

        
    }
}
