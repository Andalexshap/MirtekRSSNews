using System;
using System.ComponentModel.DataAnnotations;

namespace MirtekRSSNews.Models
{
    public class RSSNews
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название новости")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Содержание новости")]
        public string Text { get; set; }

        public DateTime DateOfNews { get; set; }

    }
}
