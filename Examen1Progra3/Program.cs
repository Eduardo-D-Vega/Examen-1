// See https://aka.ms/new-console-template for more information
using Examen1Progra3;

class Program
{
    static void Main(string[] args)
    {
        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n===== MENÚ PRINCIPAL =====");
            Console.WriteLine("1. Gestión de Citas");
            Console.WriteLine("2. Control de Clientes");
            Console.WriteLine("3. Servicios y Precios");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    MenuCitas.MostrarMenu();
                    break;
                case "2":
                    ControlCliente cliente = new ControlCliente("Sistema", nameof(TipoServicio.Corte_Caballero));
                    cliente.SubmenuControlClientes();
                    break;
                case "3":
                    Servicios.MenuServicios();
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}

