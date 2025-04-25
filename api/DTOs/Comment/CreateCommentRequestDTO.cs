using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Comment
{
    public class CreateCommentRequestDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Title must be longer that that.")]
        [MaxLength(280, ErrorMessage = "Title must be shorter that that.")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be longer that that.")]
        [MaxLength(280, ErrorMessage = "Content must be shorter that that.")]
        public string Content { get; set; } = string.Empty;
    }
}