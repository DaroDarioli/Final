using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class Deposito<T>where T:Producto
    {
        public string nombre;
        T[] productos = new T[6];
        public int capacidad = 6;


        public static Deposito<T> operator +(Deposito<T> d1, Deposito<T> d2)
        {
            Deposito<T> depo = new Deposito<T>();

            foreach(T aux in d2.productos)
            {

                if(!ReferenceEquals(aux,null))
                {
                    if (!Verifica(d1, aux))
                    {
                        d1 += aux;
                    }
                }

                
            }
            return d1;
        }

        public static bool Verifica(Deposito<T> t,T p)
        {
            for(int i = 0; i< t.productos.Length; i++)
            {

                if(!(object.ReferenceEquals(t.productos[i],null)) && !(object.ReferenceEquals(p,null)))
                {
                    if (t.productos[i] == p)
                    {
                        t.productos[i].Stock += p.Stock;
                        return true;
                    }
                }
            
                    
            }
            return false;
        }








        public static Deposito<T> operator +(Deposito<T> d,T p)
        {
            if(d.productos.Length > 3)
            {
                throw new DepositoCompletoException("Deposito lleno");
            }
            else
            {
                for (int i = 0; i < d.productos.Length; i++)
                {
                    if(d.productos[i] == null)
                    {
                        d.productos[i] = p;
                        break;
                    }
                }
            }
            return d;
            
        }
    }

    public class ProdVendido:ProdExport
    {
        public string cliente;

        public ProdVendido(ProdExport pr,string vcliente):base(pr,pr.origen)
        {
            this.cliente = vcliente;
        }
    }

    public class ProdExport:ProdImpuesto
    {
        public string origen;
        public ProdExport(ProdImpuesto pr,string voringen):base(pr.Nombre,pr.Stock,pr.impuesto)
        {
            this.origen = voringen;
        }

    }

    public class ProdImpuesto:Producto
    {
        public double impuesto;

        public ProdImpuesto(string vnombre,int vstock, double vimpuesto):base(vnombre,vstock)
        {
            this.impuesto = vimpuesto;
        }
    }


    public class Producto
    {
        public string Nombre;
        public int Stock;

        public Producto(string vnombre, int vstock)
        {
            this.Nombre = vnombre;
            this.Stock = vstock;
        }

        public override string ToString()
        {
            return this.Nombre + " - "+this.Stock;
        }

        public static bool operator ==(Producto a, Producto b)
        {
            if(!ReferenceEquals(a,null) && !ReferenceEquals(b,null))
            {
                return (a.Nombre == b.Nombre);
            }
            return false;
            
        }

        public static bool operator !=(Producto a, Producto b)
        {
            return !(a == b);
        }
    }
}