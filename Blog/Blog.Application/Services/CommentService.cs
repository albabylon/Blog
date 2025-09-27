using AutoMapper;
using Blog.Application.Contracts.Interfaces;
using Blog.Application.Exceptions;
using Blog.Domain.Entities;
using Blog.Domain.Identity;
using Blog.DTOs.Comment;
using Blog.Infrastructure.Contracts.Interfaces;
using Blog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly CommentsRepository _commentRepos;
        private readonly ArticlesRepository _articleRepos;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IUserService userService)
        {
            _commentRepos = unitOfWork.GetRepository<Comment>() as CommentsRepository
                ?? throw new Exception();
            _articleRepos = unitOfWork.GetRepository<Article>() as ArticlesRepository
                ?? throw new Exception();
            _userManager = userManager
                ?? throw new Exception();
            _userService = userService
                ?? throw new Exception();
            _mapper = mapper;
        }


        public async Task<CommentDTO> GetCommentAsync(int commentId)
        {
            var result = await _commentRepos.Get(commentId);
            return _mapper.Map<CommentDTO>(result);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync()
        {
            var result = await _commentRepos.GetAll();
            return _mapper.Map<IEnumerable<CommentDTO>>(result);
        }

        public async Task CreateCommentAsync(CreateCommentDTO dto, int articleId, string authorId)
        {           
            var article = await _articleRepos.Get(articleId)
                ?? throw new NotFoundException($"Не найдена статья {articleId}");
            
            var user = await _userManager.FindByIdAsync(authorId) 
                ?? throw new UserProblemException($"Не найден пользователь {authorId}");
            
            var comment = _mapper.Map<Comment>(dto);

            comment.Content = dto.Content;
            comment.Article = article;
            comment.ArticleId = articleId;
            comment.User = user;
            comment.UserId = authorId;

            await _commentRepos.Create(comment);
        }

        public async Task EditCommentAsync(EditCommentDTO dto, string userId)
        {
            var comment = await _commentRepos.Get(dto.Id)
                ?? throw new NotFoundException($"Комментарий {dto.Id} не найден");

            var hasPriorRole = await _userService.HasPriorityRole(userId);
            if (comment.UserId != userId && !hasPriorRole)
                throw new UnauthorizedAccessException($"Нет прав доступа");

            comment.Content = dto.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            await _commentRepos.Update(comment);
        }

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            var result = await _commentRepos.Get(commentId);

            var hasPriorRole = await _userService.HasPriorityRole(userId);
            if (result.UserId != userId && !hasPriorRole)
                throw new UnauthorizedAccessException($"Нет прав доступа");

            await _commentRepos.Delete(result);
        }
    }
}
