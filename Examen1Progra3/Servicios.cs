using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public class Servicios : ServicioCitas
    {
        public string NombreServicio { get; set; }
        public float PrecioServicio { get; set; }

        // Lista para almacenar los servicios
        private static List<Servicios> listaServicios = new List<Servicios>();

        // Constructor
        public Servicios(string cliente, string tipoServicio, string nombreServicio, float precioServicio)
            : base(cliente, tipoServicio)
        {
            NombreServicio = nombreServicio;
            PrecioServicio = precioServicio;
        }

        // Sobrescribimos el método abstracto
        public override void VisualizarTodoRegistroCitas()
        {
            if (listaServicios.Count == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }
            //Encabezado del menú de la lista de servicios
            Console.WriteLine("===== Servicios Registrados =====");
            foreach (var servicio in listaServicios)
            {
                Console.WriteLine($"Cliente: {servicio.Cliente}");
                Console.WriteLine($"Tipo de Servicio: {servicio.TipoServicio}");
                Console.WriteLine($"Nombre del Servicio: {servicio.NombreServicio}");
                Console.WriteLine($"Precio: {servicio.PrecioServicio:C}");
                Console.WriteLine("--------------------------------");
            }
        }
        // Menú de servicios
        public static void MenuServicios()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- Menú de Citas ---");
                Console.WriteLine("1. Agregar Cita");
                Console.WriteLine("2. Borrar Cita");
                Console.WriteLine("3. Visualizar Citas");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");


                try
                {
                    int opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            AgregarServicio();
                            break;
                        case 2:
                            BorrarServicio();
                            break;
                        case 3:
                            if (listaServicios.Count == 0)
                                Console.WriteLine("No hay Citas registradas.");
                            else
                                listaServicios[0].VisualizarTodoRegistroCitas(); // usamos cualquier instancia
                            break;
                        case 4:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida, intente de nuevo.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un número válido.");
                }
            }
        }
        // Método para agregar un servicio
        private static void AgregarServicio()
        {
            try
            {
                Console.Write("Ingrese el nombre del cliente: ");
                string cliente = Console.ReadLine();

                Console.WriteLine("Seleccione el tipo de servicio:");
                foreach (var tipo in Enum.GetValues(typeof(TipoServicio)))
                {
                    Console.WriteLine($"{(int)tipo} - {tipo}");
                }
                Console.Write("Opción: ");
                int tipoOpcion = int.Parse(Console.ReadLine());
                TipoServicio tipoServicio = (TipoServicio)tipoOpcion;

                Console.Write("Ingrese el nombre de la cita: ");
                string nombreServicio = Console.ReadLine();

                Console.Write("Ingrese el precio del servicio: ");
                float precioServicio = float.Parse(Console.ReadLine());

                // Creamos la instancia y agregamos a la lista
                Servicios nuevoServicio = new Servicios(cliente, tipoServicio.ToString(), nombreServicio, precioServicio);
                listaServicios.Add(nuevoServicio);

                Console.WriteLine("Cita agregada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la Cita: {ex.Message}");
            }
        }

        // Método para borrar un servicio
        private static void BorrarServicio()
        {
            try
            {
                if (listaServicios.Count == 0)
                {
                    Console.WriteLine("No hay Citas para borrar.");
                    return;
                }

                Console.WriteLine("Ingrese el índice de la Cita a borrar (empezando desde 1):");
                for (int i = 0; i < listaServicios.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaServicios[i].NombreServicio} - Cliente: {listaServicios[i].Cliente}");
                }

                int index = int.Parse(Console.ReadLine()) - 1;

                if (index < 0 || index >= listaServicios.Count)
                {
                    Console.WriteLine("Índice inválido.");
                    return;
                }

                listaServicios.RemoveAt(index);
                Console.WriteLine("Cita borrada correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al borrar la Cita: {ex.Message}");
            }
        }
    }
}




