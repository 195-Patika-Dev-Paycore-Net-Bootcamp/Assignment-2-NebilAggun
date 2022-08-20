using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BootCampHafta2.Entity
{
    //personel sınıfı için gerekli propertyler ve validasyonlar.
    public class Personal 
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(120),MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(120), MinLength(5)]
        public string Lastname { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(13), MinLength(13)]
        public string PhoneNumber { get; set; }
        [Required]
        [Range(2000,9000)]
        public decimal Salary { get; set; }

    }
}

