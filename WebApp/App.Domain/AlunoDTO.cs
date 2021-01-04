using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class AlunoDTO
    {
        public int id { get; set; }
        // Data Annotations
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Máximo de caracteres é 100.")]
        public string nome { get; set; }
        [StringLength(100, ErrorMessage = "Máximo de caracteres é 100.")]
        public string sobrenome { get; set; }
        [StringLength(15, ErrorMessage = "Máximo de caracteres é 15.")]
        public string telefone { get; set; }
        [Required(ErrorMessage = "Registro é obrigatório.")]
        [StringLength(3, ErrorMessage = "Máximo de caracteres é 3.")]
        [RegularExpression(@"[0-9]{3}", ErrorMessage = "O registro esta fora do formato 000")]
        public string registro { get; set; }
        [RegularExpression(@"[0-9]{4}\-[0-9]{2}\-[0-9]{2}", ErrorMessage ="A data esta fora do formato YYYY-MM-DD")]
        public string data { get; set; }
    }
}