using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public interface IContactoCliente
    {
        //atributos solo para la Gestión de Citas y Control de clientes
        public int Telefono { get; set; }
        public string Correo { get; set; }

        public bool ValidarEspaciosVacios(string valor);
        public bool ValidarTelefono(int numerotelefono);
        public bool ValidarCorreo(string correo);

    }
    
}
