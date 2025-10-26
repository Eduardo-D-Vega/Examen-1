using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public static class MenuCitas
    {
        private static List<Cita> citas = new List<Cita>();
        private static int contadorId = 1;

        // instancia de ControlCliente para registrar y mostrar historial
        private static ControlCliente controlCliente;

        static MenuCitas()
        {
            try
            {
                controlCliente = new ControlCliente("Sistema", nameof(TipoServicio.Corte_Caballero));
            }
            catch
            {
                controlCliente = new ControlCliente("Sistema", Enum.GetName(typeof(TipoServicio), 0) ?? "Corte_Caballero");
            }
        }


        public static void MostrarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n===== GESTIÓN DE CITAS =====");
                Console.WriteLine("1. Registrar nueva cita");
                Console.WriteLine("2. Consultar citas");
                Console.WriteLine("3. Cancelar cita");
                Console.WriteLine("4. Ver historial de citas");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarCita();
                        break;
                    case "2":
                        ConsultarCitas();
                        break;
                    case "3":
                        CancelarCita();
                        break;
                    case "4":
                        controlCliente.GetType()
                                      .GetMethod("VisualizarTodoRegistroCitas",
                                          System.Reflection.BindingFlags.NonPublic |
                                          System.Reflection.BindingFlags.Instance)
                                      ?.Invoke(controlCliente, null);
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine(" Opción inválida. Intente nuevamente.");
                        break;
                }
            }
        }

        private static void RegistrarCita()
        {
            try
            {
                Console.WriteLine("\n--- REGISTRO DE NUEVA CITA ---");

                string nombre;
                while (true)
                {
                    Console.Write("Ingrese el nombre completo del cliente: ");
                    nombre = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        Console.WriteLine("❌ El nombre no puede estar vacío.\n");
                        continue;
                    }

                    // Verifica que solo haya letras o espacios
                    if (!nombre.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    {
                        Console.WriteLine("❌ El nombre solo puede contener letras y espacios.\n");
                        continue;
                    }
                    break; // válido
                }

                int telefono;
                while (true)
                {
                    Console.Write("Ingrese el número de teléfono (8 dígitos): ");
                    string telefonoStr = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(telefonoStr))
                    {
                        Console.WriteLine("❌ El teléfono no puede estar vacío.\n");
                        continue;
                    }

                    if (!telefonoStr.All(char.IsDigit))
                    {
                        Console.WriteLine("❌ El teléfono solo debe contener números.\n");
                        continue;
                    }

                    if (telefonoStr.Length != 8)
                    {
                        Console.WriteLine("❌ El teléfono debe tener exactamente 8 dígitos.\n");
                        continue;
                    }

                    telefono = int.Parse(telefonoStr);
                    break;
                }

                string correo;
                while (true)
                {
                    Console.Write("Ingrese el correo electrónico: ");
                    correo = Console.ReadLine()?.Trim();

                    if (string.IsNullOrWhiteSpace(correo))
                    {
                        Console.WriteLine("❌ El correo no puede estar vacío.\n");
                        continue;
                    }

                    if (!correo.Contains('@') || !correo.Contains('.'))
                    {
                        Console.WriteLine("❌ Correo inválido. Debe contener '@' y '.'\n");
                        continue;
                    }

                    break;
                }

                TipoServicio tipoServicio;
                while (true)
                {
                    Console.WriteLine("\nSeleccione el tipo de servicio:");
                    foreach (var tipo in Enum.GetValues(typeof(TipoServicio)))
                    {
                        Console.WriteLine($"{(int)tipo} - {tipo}");
                    }

                    Console.Write("Opción: ");
                    string opcionTipo = Console.ReadLine()?.Trim();

                    if (!int.TryParse(opcionTipo, out int tipoSeleccion) ||
                        !Enum.IsDefined(typeof(TipoServicio), tipoSeleccion))
                    {
                        Console.WriteLine("❌ Tipo de servicio no válido. Intente nuevamente.\n");
                        continue;
                    }

                    tipoServicio = (TipoServicio)tipoSeleccion;
                    break;
                }

                DateTime fecha;
                while (true)
                {
                    Console.Write("Ingrese la fecha de la cita (YYYY-MM-DD): ");
                    string fechaStr = Console.ReadLine()?.Trim();

                    if (!DateTime.TryParse(fechaStr, out fecha))
                    {
                        Console.WriteLine("❌ Fecha inválida. Use el formato YYYY-MM-DD.\n");
                        continue;
                    }

                    break;
                }

                TimeSpan hora;
                while (true)
                {
                    Console.Write("Ingrese la hora de la cita (HH:mm): ");
                    string horaStr = Console.ReadLine()?.Trim();

                    if (!TimeSpan.TryParse(horaStr, out hora))
                    {
                        Console.WriteLine("❌ Hora inválida. Use el formato HH:mm.\n");
                        continue;
                    }

                    break;
                }

                DateTime fechaHora = fecha.Date + hora;

                // Validaciones de tiempo
                if (fechaHora < DateTime.Now)
                {
                    Console.WriteLine("❌ No se pueden agendar citas en fechas pasadas.\n");
                    return;
                }

                if (fechaHora > DateTime.Now.AddDays(7))
                {
                    Console.WriteLine("❌ Solo se puede agendar con una semana de anticipación como máximo.\n");
                    return;
                }

                // Registro exitoso
                Cita nuevaCita = new Cita(contadorId++, nombre, telefono, correo, tipoServicio, fechaHora);
                citas.Add(nuevaCita);

                // 🔗 Guardar también en historial (ControlCliente → lista enlazada)
                var agregarMetodo = typeof(ControlCliente).GetMethod("AgregarClienteALista",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                if (agregarMetodo != null)
                {
                    agregarMetodo.Invoke(controlCliente, new object[] { nombre, telefono, correo, fechaHora });
                }

                Console.WriteLine("\n✅ Cita registrada correctamente y añadida al historial.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠ Error al registrar la cita: {ex.Message}");
            }
        }


        private static void ConsultarCitas()
        {
            try
            {
                // 🔹 Si no hay citas registradas, mostrar aviso y salir
                if (citas == null || citas.Count == 0)
                {
                    Console.WriteLine("\n No hay citas registradas actualmente.");
                    return;
                }

                Console.WriteLine("\n¿Desea filtrar por fecha específica? (s/n): ");
                string respuesta = Console.ReadLine()?.Trim().ToLower();

                // 🔹 Si la respuesta no es 's', se muestran todas
                if (respuesta != "s")
                {
                    MostrarListaDeCitas(citas);
                    return;
                }

                Console.Write("Ingrese la fecha (YYYY-MM-DD): ");
                string fechaInput = Console.ReadLine()?.Trim();

                // 🔹 Validación: entrada vacía
                if (string.IsNullOrWhiteSpace(fechaInput))
                {
                    Console.WriteLine(" No ingresó una fecha. Mostrando todas las citas...\n");
                    MostrarListaDeCitas(citas);
                    return;
                }

                // 🔹 Validación: formato de fecha
                if (!DateTime.TryParse(fechaInput, out DateTime fechaFiltro))
                {
                    Console.WriteLine("Formato de fecha inválido. Use el formato YYYY-MM-DD.\n");
                    return;
                }

                // 🔹 Filtrado por fecha
                var citasFiltradas = citas.Where(c => c.FechaHora.Date == fechaFiltro.Date).ToList();

                if (citasFiltradas.Count == 0)
                {
                    Console.WriteLine($"\n No hay citas registradas para el día {fechaFiltro:yyyy-MM-dd}.\n");
                }
                else
                {
                    MostrarListaDeCitas(citasFiltradas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error al consultar las citas: {ex.Message}");
            }
        }
        private static void MostrarListaDeCitas(List<Cita> lista)
        {
            Console.WriteLine("\n===== LISTA DE CITAS =====");

            foreach (var cita in lista)
            {
                Console.WriteLine($"ID: {cita.Id}");
                Console.WriteLine($"Cliente: {cita.Nombre}");
                Console.WriteLine($"Teléfono: {cita.Telefono}");
                Console.WriteLine($"Correo: {cita.Correo}");
                Console.WriteLine($"Servicio: {cita.TipoServicio}");
                Console.WriteLine($"Fecha y hora: {cita.FechaHora}");
                Console.WriteLine("--------------------------------");
            }

            Console.WriteLine($"Total de citas mostradas: {lista.Count}\n");
        }


        private static void CancelarCita()
        {
            if (citas.Count == 0)
            {
                Console.WriteLine("No hay citas registradas.");
                return;
            }

            Console.Write("Ingrese el nombre del cliente o el ID de la cita a cancelar: ");
            string entrada = Console.ReadLine().Trim();

            Cita citaEncontrada = null;

            if (int.TryParse(entrada, out int id))
            {
                citaEncontrada = citas.FirstOrDefault(c => c.Id == id);
            }
            else
            {
                citaEncontrada = citas.FirstOrDefault(c => c.Nombre.Equals(entrada, StringComparison.OrdinalIgnoreCase));
            }

            if (citaEncontrada != null)
            {
                citas.Remove(citaEncontrada);
                Console.WriteLine("✅ Cita cancelada exitosamente.");
            }
            else
            {
                Console.WriteLine("❌ No se encontró ninguna cita con esa información.");
            }
        }
    }

    public class Cita
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public DateTime FechaHora { get; set; }

        public Cita(int id, string nombre, int telefono, string correo, TipoServicio tipoServicio, DateTime fechaHora)
        {
            Id = id;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
            TipoServicio = tipoServicio;
            FechaHora = fechaHora;
        }
    }
}
