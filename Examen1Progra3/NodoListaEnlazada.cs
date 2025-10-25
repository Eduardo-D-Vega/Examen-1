using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public class NodoListaEnlazada
    {
        public string cliente;
        public int telefono;
        public string correo;
        public DateTime FechaCita;
        public NodoListaEnlazada Siguiente;

        public NodoListaEnlazada(string cliente, int telefono, string correo, DateTime FechaCita, NodoListaEnlazada siguiente)
        {
            this.cliente = cliente;
            this.telefono = telefono;
            this.correo = correo;
            this.FechaCita = FechaCita;
            this.Siguiente = siguiente;
        }
    }
}
