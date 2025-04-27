using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comment;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        public CommentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Comment> CreateCommentAsync(Comment commenModel)
        {
            await _db.Comments.AddAsync(commenModel);
            await _db.SaveChangesAsync();
            return commenModel;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject)
        {
            var comments = _db.Comments.Include(u => u.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                comments = comments.Where(u => u.Stock.Symbol == queryObject.Symbol);
            }

            if (!queryObject.IsDecsending == true)
            {
                comments = comments.OrderByDescending(u => u.CreatedOn);
            }

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _db.Comments.Include(u => u.AppUser).FirstOrDefaultAsync(u => u.CommentId == id);
            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentRequestDTO commentModel)
        {
            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _db.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var commentModel = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);
            if (commentModel == null)
            {
                return null;
            }

            _db.Comments.Remove(commentModel);
            await _db.SaveChangesAsync();

            return commentModel;
        }
    }
}