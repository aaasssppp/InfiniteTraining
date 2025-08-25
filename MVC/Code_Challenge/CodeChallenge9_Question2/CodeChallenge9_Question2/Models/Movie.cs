using System;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge9_Question2.Models
{
    public class Movie
    {
        [Key]
        public int Mid { get; set; }

        [Required]
        public string MovieName { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfRelease { get; set; }  
    }
}
