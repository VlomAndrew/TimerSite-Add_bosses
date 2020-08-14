using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Models
{
    public class BossViewModel
    { 
        public int Id { get; set; }

        [Display(Name="Название босса")]
        public string Name { get; set; }

        [Display(Name="Время респа в формате чч.мм.сс!!!Проверяйте плз эту хуйню иначе не будет работать!")]
        public string KdTime { get; set; }

        
        public int KdCount { get; set; }

        

        public string LastTime { get; set; }

        public string Color { get; set; }
        
        //public DateTime LastDateTime { get; set; }
    }

    
}