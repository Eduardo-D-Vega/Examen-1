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

            try
            {
                TipoServicio = Enum.Parse<TipoServicio>(tipoServicio);
            }
            catch
            {
                TipoServicio = TipoServicio.Corte_Caballero; // valor por defecto
            }
        }


        protected abstract void VisualizarTodoRegistroCitas(); //método para sobrescribir

    }
}
