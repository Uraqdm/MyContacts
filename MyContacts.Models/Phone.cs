﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Phone
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        [Phone]
        public string PhoneNumber { get; set; }

    }
}