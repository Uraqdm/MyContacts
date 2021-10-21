using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class StatisticViewModel
    {
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Начало периода")]
        public DateTime BeginDate { get; set; } = DateTime.Now.AddDays(-1);

        [Required]
        [Display(Name = "Конец периода")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Display(Name = "Количество звонков")]
        public int CallsCount { get; set; }
    }
}
