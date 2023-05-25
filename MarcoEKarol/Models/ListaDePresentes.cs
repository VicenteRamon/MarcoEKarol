using System.ComponentModel.DataAnnotations;

namespace MarcoEKarol.Models
{
    public class ListaDePresentes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] ImageData { get; set; }
        public decimal Pryce { get; set; }

    }
}
