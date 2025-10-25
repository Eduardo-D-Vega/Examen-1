using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen1Progra3
{
    public abstract class ServicioCitas 
    {
        protected string Cliente { get; set; }
        public TipoServicio TipoServicio { get; set; }

        public ServicioCitas(string cliente, string tipoServicio)
        {
            Cliente = cliente;
            TipoServicio = Enum.Parse<TipoServicio>(tipoServicio);
        }

        protected abstract void VisualizarTodoRegistroCitas(); //método para sobrescribir

    }
}
