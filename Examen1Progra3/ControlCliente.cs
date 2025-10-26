using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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

                Console.WriteLine("Ingrese la fecha de la cita (dd/MM/yyyy): ");
                string fechaInput = Console.ReadLine().Trim();

                //Tomado de: stackoverflow (DateTime.TryParseExact CultureInfo.InvariantCulture)
                if (!DateTime.TryParseExact(fechaInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaCita))
                    {
                    Console.WriteLine("La fecha ingresada no es válida, intente de nuevo");
                    return;
                }

                AgregarClienteALista(cliente, telefono, correo, fechaCita);
                Console.WriteLine("\n--- El cliente fue registrado exitosamente ---\n");

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
                if (numeroString[i] < '0' || numeroString[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        //tomado de: stackoverflow (Expresión regular de un email en C#)
        bool IContactoCliente.ValidarCorreo(string correo)
        {
            if (!((IContactoCliente)this).ValidarEspaciosVacios(correo))
            {
                return false;
            }

            return Regex.IsMatch(correo, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$");
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
                    Console.WriteLine($"[Nombre: {recorrer.cliente} | Teléfono: {recorrer.telefono} | Correo: {recorrer.correo} | Fecha: {recorrer.FechaCita.ToString("dd/MM/yyyy")}]");
                    recorrer = recorrer.Siguiente;
                }
                Console.WriteLine("--------------------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrio un error al visualizar en registro de las citas. Error: " + ex.Message);
            }
        }

        public void BuscarCliente()
        {
            try 
            {
                if (inicio == null)
                {
                    Console.WriteLine("No hay clientes registrados.");
                    return;
                }

                Console.Write("---- BUSQUEDA DE CLIENTES (POR NOMBRE) ----\n");
                Console.Write("Ingrese el nombre del cliente a buscar: ");
                string nombreCliente = Console.ReadLine().Trim();

                NodoListaEnlazada actual = inicio;
                bool existente = false;

                while (actual != null)
                {
                    if (string.Equals(actual.cliente, nombreCliente, StringComparison.OrdinalIgnoreCase))
                    {
                        if (!existente)
                        {
                            Console.WriteLine($"\n>> El cliente '{nombreCliente}' fue encontrado\n<<");
                            Console.WriteLine("Información del cliente: ");
                            Console.WriteLine("--------------------------------");
                            existente = true;
                        }
                        Console.WriteLine($"Nombre del cliente: {actual.cliente}");
                        Console.WriteLine($"Telefono: {actual.telefono}");
                        Console.WriteLine($"Correo: {actual.correo}");
                        Console.WriteLine($"Fecha de Cita: {actual.FechaCita.ToString("dd/MM/yyyy")}]");
                        Console.WriteLine("--------------------------------");
                    }
                    actual = actual.Siguiente;
                }
                if (!existente)
                {
                    Console.WriteLine("El cliente no fue encontrado");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ha ocurrido un error al buscar el cliente. Error: " + ex.Message);
            }
        }

        public void SubmenuControlClientes()
        {
            bool submenu = true;
            do
            {
                Console.WriteLine("\n---- MENÚ: CONTROL DE CLIENTES ----");
                Console.WriteLine("1. Añadir cliente");
                Console.WriteLine("2. Buscar cliente");
                Console.WriteLine("3. Historial de citas");
                Console.WriteLine("4. regresar al menú principal");
                Console.Write("Elija una opción: ");

                try
                {
                    int opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            RegistrarCliente();
                            break;
                        case 2:
                            BuscarCliente();
                            break;
                        case 3:
                            VisualizarTodoRegistroCitas();
                            break;
                        case 4:
                            submenu = false;
                            break;
                        default:
                            Console.WriteLine("Opción inválida, debe ingresar un numero del 1 al 4");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Entrada inválida, por favor ingrese un numero del 1 al 4");
                }
            }
            while (submenu);
        }
    }
}
