using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public class ControlCliente : ServicioCitas, IContactoCliente
    {
        int IContactoCliente.Telefono { get; set; }
        string IContactoCliente.Correo { get; set; }

        public ControlCliente(string cliente, string tipoServicio) : base(cliente, tipoServicio)
        {
        }


        public override void VisualizarTodoRegistroCitas()
        {

        }
    }
}
