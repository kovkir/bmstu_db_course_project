using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using db_cp.Models;

namespace db_cp.ViewModels
{
    public class AddPlayerViewModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Не указана страна")]
        public string country { get; set; }

        [Required(ErrorMessage = "Не указан клуб")]
        public string clubName { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        [Range(typeof(uint), "1000", "300000", ErrorMessage = "Невозможная цена")]
        public uint price { get; set; }

        [Required(ErrorMessage = "Не указан рейтинг")]
        [Range(typeof(uint), "50", "99", ErrorMessage = "Невозможный рейтинг")]
        public uint rating { get; set; }
    }
}
