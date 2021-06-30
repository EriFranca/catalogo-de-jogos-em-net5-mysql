using System;   
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace catalogoJogosAPI.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must have between 3 and 100 characters")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Studio name must have between 1 and 100 characters")]
        public string Produtora { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000")]
        public double Preco { get; set; }

    }
}