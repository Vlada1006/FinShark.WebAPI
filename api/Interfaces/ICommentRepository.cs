using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comment;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment commenModel);
        Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDTO commentDTO);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}