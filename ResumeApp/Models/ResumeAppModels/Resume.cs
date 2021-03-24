using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeApp.Models.ResumeAppModels
{
    public class Resume
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        [Display(Name="Skills")]
        [MaxLength(500)]
        public string Skills { get; set; }

        [ForeignKey("User")]
        [MaxLength(128)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}