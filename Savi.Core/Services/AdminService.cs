﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Savi.Core.DTO;
using Savi.Core.IServices;
using Savi.Data.Repositories.Interface;
using Savi.Model;
using Savi.Utility.Pagination;

namespace Savi.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<AdminService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public ApiResponse<GroupDTO> GetGroupSavingById(string groupId)
        {
            try
            {
                var group = _unitOfWork.GroupRepository.GetById(groupId);
                if (group == null)
                {
                    return new ApiResponse<GroupDTO>(false, "Group not found", StatusCodes.Status404NotFound);
                }
                var response = _mapper.Map<GroupDTO>(group);
                return new ApiResponse<GroupDTO>(true, "Group found successfully", StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting Group Saving by Id");
                return new ApiResponse<GroupDTO>(false, "Error occurred while processing your request", StatusCodes.Status500InternalServerError, new List<string> { ex.Message });
            }
        }

        public Task<ApiResponse<GroupTransactionDto>> GetGroupTransactionsAsync(int page, int perPage)
        {
            throw new NotImplementedException();
        }

       public async Task<ApiResponse<PageResult<List<GroupDTO3>>>> GetAllGroupSavingsTodayAsync(int perPage, int page)
        {
            try
            {
                var groupSavingsToday = await _unitOfWork.GroupRepository.GetGroupSavingsCreatedTodayAsync(); 

                if (groupSavingsToday != null && groupSavingsToday.Any())
                {
                    var mappedGroups = _mapper.Map<List<GroupDTO3>>(groupSavingsToday);

                    var paginatedResult = await PagePagination<GroupDTO3>.GetPager(mappedGroups, perPage, page, group => group.SaveName, group => group.Id);

                    return ApiResponse<PageResult<List<GroupDTO3>>>.Success(
                        new PageResult<List<GroupDTO3>>
                        {
                            Data = paginatedResult.Data.ToList(),
                            TotalPageCount = paginatedResult.TotalPageCount,
                            CurrentPage = paginatedResult.CurrentPage,
                            PerPage = paginatedResult.PerPage,
                            TotalCount = paginatedResult.TotalCount
                        },
                        "Group savings created today found successfully",
                        StatusCodes.Status200OK
                    );
                }
                else
                {
                    return ApiResponse<PageResult<List<GroupDTO3>>>.Failed(
                        false,
                        "No group savings created today",
                        StatusCodes.Status404NotFound,
                        new List<string> { "No group savings created today" }
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting group savings created today");
                return ApiResponse<PageResult<List<GroupDTO3>>>.Failed(
                    false,
                    "Error occurred while processing your request",
                    StatusCodes.Status500InternalServerError,
                    new List<string> { ex.Message }
                );
            }
        }
    }
}
