using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientesAPI.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(80)]
        public string Nome { get; set; }
        [Required, StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Idade { get; set; }
    }
}
