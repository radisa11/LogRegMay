using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace LogRegMay.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required]
        public string username {get;set;}
        [Required]
        [EmailAddress]
        public string email {get;set;}
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confirm {get;set;}

    }

}