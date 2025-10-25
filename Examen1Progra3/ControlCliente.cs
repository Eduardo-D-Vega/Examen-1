using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public class ControlCliente : ServicioCitas, IContactoCliente
    {

        public int Telefono { get; set; }
        public string Correo { get; set; }

        private NodoListaEnlazada inicio;

        public ControlCliente(string cliente, string tipoServicio) : base(cliente, tipoServicio)
        {
        }

        private void AgregarClienteALista(string cliente, int telefono, string correo, DateTime fechaCita)
        {
            inicio = new NodoListaEnlazada(cliente, telefono, correo,fechaCita, inicio);
        }

        public void RegistrarCliente()
        {
            try
            {
                Console.WriteLine("\n---- REGISTRO DE CLIENTES----\n");

                Console.Write("Ingrese el nombre del cliente a registrar: ");
                string cliente = Console.ReadLine().Trim();
                if (!((IContactoCliente)this).ValidarEspaciosVacios(cliente))
                {
                    Console.WriteLine("Tiene que ingresar un nombre válido, intente de nuevo");
                    return;
                }

                Console.Write("Ingrese el número de telefono del cliente: ");
                string telefonoCliente = Console.ReadLine().Trim();
                if (!((IContactoCliente)this).ValidarEspaciosVacios(telefonoCliente))
                {
                    Console.WriteLine("El número de teléfono no puede estar vacio, intente de nuevo");
                    return;
                }
                int telefono = int.Parse(telefonoCliente);
                if (!((IContactoCliente)this).ValidarTelefono(telefono))
                { 
                    Console.WriteLine("El número de telefono solo debe tener 8 dígitos y solo contener números, intente nuevamente");
                    return;
                }

                Console.Write("Ingrese el correo electronico del cliente: ");
                string correo = Console.ReadLine().Trim();
                if (!((IContactoCliente)this).ValidarEspaciosVacios(correo))
                {
                    Console.WriteLine("El correo electrónico no puede estar vacío, intente nuevamente");
                    return;
                }

                Console.WriteLine("Ingrese la fecha de la cita (Año-mes-día):  ");
                string fechaInput = Console.ReadLine().Trim();
                if (!DateTime.TryParse(fechaInput, out DateTime fechaCita))
                {
                    Console.WriteLine("La fecha ingresada no es válida, intente de nuevo");
                    return;
                }

                AgregarClienteALista(cliente, telefono, correo, fechaCita);
                Console.WriteLine("---Cliente registrado exitosamente ---\n");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al registrar el cliente. Error: " + ex.Message);
            }
        }

        bool IContactoCliente.ValidarEspaciosVacios(string valor)
        {
            if (valor == null || valor.Trim() == "")
            {
                return false;
            }
            return true; //si el valor no esta vacio
        }

        bool IContactoCliente.ValidarTelefono(int numerotelefono)
        {
            string numeroString = numerotelefono.ToString();

            if (numeroString.Length != 8)
            {
                return false;
            }

            for (int i = 0; i < numeroString.Length; i++)
            {
                if (numeroString[i] < 0 || numeroString[i] > 8)
                {
                    return false;
                }
            }
            return true;
        }

        bool IContactoCliente.ValidarCorreo(string correo)
        {
            if (!((IContactoCliente)this).ValidarEspaciosVacios(correo))
            {
                return false;
            }

            bool arroba = false;
            bool punto = false;
            for (int i = 0; i < correo.Length; i++)
            {
                if (correo[i] == '@')
                {
                    arroba = true;
                }
                if (correo[i] == '.')
                {
                    punto = true;
                }
            }
            return arroba && punto;

            //anadir referencia
        }
        
        protected override void VisualizarTodoRegistroCitas()
        {
            try
            {
                Console.WriteLine("\n------ HISTORIAL DE CITAS ------\n");

                if (inicio == null)
                {
                    Console.WriteLine("No hay un registros de citas");
                    return;
                }

                NodoListaEnlazada recorrer = inicio;
                while (recorrer != null)
                {
                    Console.WriteLine($"[ Nombre: {recorrer.cliente} | Teléfono: {recorrer.telefono} | Correo: {recorrer.correo} | Fecha: {recorrer.FechaCita.ToString("Año - mes - día")} ] --->");
                    recorrer = recorrer.Siguiente;
                }
                Console.WriteLine("----------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al visualizar en registro de las citas. Error: " + ex.Message);
            }
        }
    }
}
