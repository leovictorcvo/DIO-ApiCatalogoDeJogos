using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve ter entre 3 e 100 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve ter entre 3 e 100 caracteres")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do jogo deve ter entre R$ 1,00 e R$ 1000,00")]
        public double Price { get; set; }
    }
}
