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
        protected int Telefono { get; set; }
        protected string Correo { get; set; }
    }
}
