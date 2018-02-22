using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;



using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace PracticaParcial
{
    public delegate void CargarBase(List<Base> lista);

    public abstract class Base
    {

        private static int _subversion;

        private static int _version;

        public static event CargarBase CargarBaseEvent;

        //__________________

        private static SqlConnection _connection;

        private static SqlCommand _command;

        //___________________

        #region Propiedades

        public string Version
        {
            get { return _version.ToString(); }


            set
            {

                try
                {
                    _version = int.Parse(value);
                }
                catch
                {

                }

            }
        }

        public abstract string VersionFull
        {
            get;
        }


        #endregion

        static Base()
        {
            _subversion = 0;
            _version = 1;
            _connection = new SqlConnection(Properties.Settings.Default.Setting);
            _command = new SqlCommand();
            CargarBaseEvent += EjecutarEvento;
        }


        public Base(int version, int subversion)
        {
            _version = version;
            _subversion = subversion;        

        }

        public override int GetHashCode()
        {
            return _subversion + _version;
        }
              

        public static void EjecutarEvento(List<Base> objeto)
        {
           // CargarBaseEvent(objeto);

           

            List<Base> lista = (List<Base>)objeto;
            foreach (Base b in lista)
            {
                 Base.GuardarDatos(b);       
            }
            /*
           Queue<Base> cola = new Queue<Base>();

           cola = LeerDatos<Base>();

          //Luego lanzará un evento retornando una cola con los elementos presentes 
          //en la base, usar llerdatos

   */
        }



        public static bool GuardarDatos<T>(T dato)where T:class
        {
            int derivada = 0;
            string aux = "";          

            if(dato is DerivadaUno)
            {
                DerivadaUno auxRevision = (DerivadaUno)Convert.ChangeType(dato, typeof(DerivadaUno));

                int cont = 0;
                int info = 0;
                foreach (char d in auxRevision.MostrarVersion())
                {
                    cont++;
                    if (cont == 3)
                        info = int.Parse(d.ToString());
                }

                derivada = 1;
                aux = string.Format("Insert into Datos(version,subversion,revision,derivada) Values({0},{1},{2},{3})", _version, _subversion,info, derivada);
            }
            else
            {
                derivada = 2;
                aux = string.Format("Insert into Datos(version,subversion,derivada) Values({0},{1},{2})", _version, _subversion,derivada);

            }


           try
            {
                _command.Connection = _connection;
                _command.CommandType = CommandType.Text;
                _command.CommandText = aux;
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();
                return true;


            }
            catch (Exception e)
            {
                
                Test t = new Test();
                t.richTest.AppendText(e.Message);
                t.richTest.AppendText(e.StackTrace);
                t.richTest.AppendText(e.InnerException.Message);
                t.ShowDialog();

                return false;
            } 

        }



        public static Queue<T> LeerDatos<T>()where T :Base
        {
            Queue<Base> myQ = new Queue<Base>();                    
            
            try
            {
                _command.Connection = _connection;
                _command.CommandType = CommandType.Text;
                _command.CommandText = "SELECT * FROM Datos";
                _connection.Open();

                SqlDataReader sr = _command.ExecuteReader();

                while (sr.Read())
                {
                   if(sr["derivada"].ToString() == "1")
                    {
                        DerivadaUno b = new DerivadaUno(int.Parse(sr[0].ToString()), int.Parse(sr[1].ToString()), int.Parse(sr[2].ToString()));
                        myQ.Enqueue(b);
                    }
                    else
                    {
                        Base b = new DerivadaDos(int.Parse(sr[0].ToString()), int.Parse(sr[1].ToString()));
                        myQ.Enqueue(b);
                    }
                    
                                    
                }
                sr.Close();
                _connection.Close();


               

            }
            catch (Exception e)
            {

                Test lectura = new Test();
                lectura.richTest.AppendText(e.Message);
                lectura.ShowDialog();
            }

            Queue<T> retorno = (Queue<T>)Convert.ChangeType(myQ, typeof(Queue<T>));

            
            return retorno; 

        }

        
        protected virtual string MostrarVersion()
        {
            return string.Format("{0}{1}",_version,_subversion);
        }

        public static string operator ~(Base b)
        {
            return Reverse((b.MostrarVersion()));
        }


        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        
        public static void CargarForm(Queue<Base> cola)
        {


            Test t = new Test();
            
           
            foreach(Base c in cola)
            {
                t.richTest.AppendText(c.VersionFull);
            }

            t.ShowDialog();
        }



    }
}
