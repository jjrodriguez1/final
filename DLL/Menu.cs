using System.Collections.Generic;

namespace DLL
{
    public class Menu
    {
        public Menu()
        {

        }

        public int Id { get; set; }
		public string IdProducto {get; set;}
        public string DescripcionMenu { get; set; }

        public List<Producto> Productos { get; set; }
    }
}
