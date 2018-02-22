using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olinuck.Dario.Aula20;

namespace Olinuck.Dario.MainAula20
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n------------##   Delegados   ##--------------");            
            Empleado emp = new Empleado("Juan", "Gonzalez", 4545);
            emp.SueldoEvent += SobrePasaSueldo;
            emp.SueldoMejorado += Emp_SueldoMejorado;

            //********************* Con método de instancia

            Console.WriteLine("\n\n--------## Agrego metodo de intancia  ##------");
            Program p1 = new Program();

            emp.SueldoEvent += p1.SobrePasaSueldoTest;

            try
            {
                emp.Sueldo = 22000;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\n\n------------## Le quito uno  ##--------------");
            emp.SueldoEvent -= SobrePasaSueldo;
            try
            {
                emp.Sueldo = 15000;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            


            Console.ReadLine();

        }

        private static void Emp_SueldoMejorado(object o, EventArgs e)
        {
            Console.WriteLine(o.ToString());

            //throw new NotImplementedException();
        }

        public static void SobrePasaSueldo()
        {
            Console.WriteLine("El sueldo sobrepasa el límite!!");
        
        }


        public void SobrePasaSueldoTest()
        {
            Console.WriteLine("El sueldo sobre pasa el límite--en instancia!!");

        }
    }
}
