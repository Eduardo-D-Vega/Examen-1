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

        private static List<Servicios> listaServicios = new List<Servicios>();

        public Servicios(string cliente, string tipoServicio, string nombreServicio, float precioServicio)
            : base(cliente, tipoServicio)
        {
            NombreServicio = nombreServicio;
            PrecioServicio = precioServicio;
        }

        // Carga automática de servicios predefinidos
        static Servicios()
        {
            listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Corte_Caballero), "Corte Caballero", 3000));
            listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Afeitado_Clasico), "Afeitado Clásico", 2500));
            listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Corte_Y_Afeitado), "Corte y Afeitado", 5000));
            listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Tratamiento_Capilar), "Tratamiento Capilar", 4000));
            listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Arreglo_Y_Perfilado), "Arreglo y Perfilado", 3500));
        }

        // Muestra los servicios disponibles
        protected override void VisualizarTodoRegistroCitas()
        {
            if (listaServicios.Count == 0)
            {
                Console.WriteLine("No hay servicios registrados.");
                return;
            }

            Console.WriteLine("\n===== SERVICIOS DISPONIBLES =====");
            foreach (var servicio in listaServicios)
            {
                Console.WriteLine($"Nombre del Servicio: {servicio.NombreServicio}");
                Console.WriteLine($"Precio: {servicio.PrecioServicio} colones");
                Console.WriteLine("--------------------------------");
            }
        }

        // Menú principal de gestión de servicios
        public static void MenuServicios()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n===== GESTIÓN DE SERVICIOS Y PRECIOS =====");
                Console.WriteLine("1. Ver lista de servicios disponibles");
                Console.WriteLine("2. Modo administrador (añadir, modificar o eliminar)");
                Console.WriteLine("3. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                try
                {
                    int opcion = int.Parse(Console.ReadLine());
                    switch (opcion)
                    {
                        case 1:
                            listaServicios[0].VisualizarTodoRegistroCitas();
                            break;
                        case 2:
                            MenuAdministrador();
                            break;
                        case 3:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un número válido.");
                }
            }
        }

        // Submenú administrativo
        private static void MenuAdministrador()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n--- MENÚ ADMINISTRATIVO ---");
                Console.WriteLine("1. Agregar nuevo servicio");
                Console.WriteLine("2. Modificar precio de un servicio");
                Console.WriteLine("3. Eliminar servicio");
                Console.WriteLine("4. Volver");
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
                            ModificarPrecio();
                            break;
                        case 3:
                            BorrarServicio();
                            break;
                        case 4:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Debe ingresar un número válido.");
                }
            }
        }

        // Agregar servicio
        private static void AgregarServicio()
        {
            try
            {
                Console.WriteLine("\n--- AGREGAR NUEVO SERVICIO ---");

                string nombreServicio;
                while (true)
                {
                    Console.Write("Ingrese el nombre del servicio: ");
                    nombreServicio = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(nombreServicio))
                    {
                        Console.WriteLine("El nombre no puede estar vacío.");
                        continue;
                    }
                    if (!nombreServicio.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    {
                        Console.WriteLine("El nombre solo puede contener letras y espacios.");
                        continue;
                    }
                    break;
                }

                float precioServicio;
                while (true)
                {
                    Console.Write("Ingrese el precio del servicio en colones: ");
                    string precioStr = Console.ReadLine()?.Trim();
                    if (float.TryParse(precioStr, out precioServicio) && precioServicio > 0)
                        break;
                    Console.WriteLine("Precio inválido. Ingrese un número mayor que 0.");
                }

                listaServicios.Add(new Servicios("Sistema", nameof(TipoServicio.Corte_Caballero), nombreServicio, precioServicio));
                Console.WriteLine("Servicio agregado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el servicio: {ex.Message}");
            }
        }

        // Modificar precio
        private static void ModificarPrecio()
        {
            try
            {
                if (listaServicios.Count == 0)
                {
                    Console.WriteLine("No hay servicios registrados para modificar.");
                    return;
                }

                Console.WriteLine("\n--- MODIFICAR PRECIO DE SERVICIO ---");
                for (int i = 0; i < listaServicios.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaServicios[i].NombreServicio} - Precio actual: {listaServicios[i].PrecioServicio} colones");
                }

                Console.Write("Seleccione el número del servicio: ");
                if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > listaServicios.Count)
                {
                    Console.WriteLine("Selección inválida.");
                    return;
                }

                float nuevoPrecio;
                while (true)
                {
                    Console.Write("Ingrese el nuevo precio en colones: ");
                    string nuevoPrecioStr = Console.ReadLine()?.Trim();
                    if (float.TryParse(nuevoPrecioStr, out nuevoPrecio) && nuevoPrecio > 0)
                        break;
                    Console.WriteLine("Precio inválido. Debe ser mayor que 0.");
                }

                listaServicios[index - 1].PrecioServicio = nuevoPrecio;
                Console.WriteLine("Precio modificado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al modificar el precio: {ex.Message}");
            }
        }

        // Eliminar servicio
        private static void BorrarServicio()
        {
            try
            {
                if (listaServicios.Count == 0)
                {
                    Console.WriteLine("No hay servicios para eliminar.");
                    return;
                }

                Console.WriteLine("\n--- ELIMINAR SERVICIO ---");
                for (int i = 0; i < listaServicios.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listaServicios[i].NombreServicio} - Precio: {listaServicios[i].PrecioServicio} colones");
                }

                Console.Write("Seleccione el número del servicio a eliminar: ");
                if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > listaServicios.Count)
                {
                    Console.WriteLine("Selección inválida.");
                    return;
                }

                listaServicios.RemoveAt(index - 1);
                Console.WriteLine("Servicio eliminado correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el servicio: {ex.Message}");
            }
        }
    }
}




