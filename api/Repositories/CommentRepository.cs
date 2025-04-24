using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);
            if (comment == null)
            {
                return null;
            }

            return comment;
        }
    }
}