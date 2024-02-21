using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Savi.Core.DTO;
using Savi.Core.IServices;
using Savi.Data.Context;
using Savi.Model;
using Savi.Model.Entities;

namespace Savi.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SaviDbContext _context;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, SaviDbContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context;
        }

        public async Task<ApiResponse<AppUserDto>> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                return user != null
                    ? SuccessResponse(_mapper.Map<AppUserDto>(user), "User found.")
                    : NotFoundResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        private ApiResponse<AppUserDto> NotFoundResponse()
            => ApiResponse<AppUserDto>.Failed(false, "User not found.", 404, new List<string> { "User not found." });

        private ApiResponse<AppUserDto> SuccessResponse(AppUserDto userDto, string message)
            => ApiResponse<AppUserDto>.Success(userDto, message, 200);

        private ApiResponse<AppUserDto> ErrorResponse(Exception ex)
            => ApiResponse<AppUserDto>.Failed(false, "An error occurred while retrieving the user.", 500, new List<string> { ex.Message });
        public int GetAllRegisteredUsers()
        {
            //var users = _context.Users.ToList();
            //var newUser = new List<AppUser>();
            //foreach (var user in users)
            //{
            //    if (user.CreatedAt.Date == DateTime.Today.Date)
            //    {
            //        newUser.Add(user);
            //    }
            //}
            //return newUser.Count();
            var count = _context.Users
                       .Where(user => user.CreatedAt.Date == DateTime.Today.Date)
                       .Count();  

            return count;
        }
    }
}
