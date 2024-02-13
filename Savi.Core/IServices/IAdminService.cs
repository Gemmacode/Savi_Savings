using Savi.Core.DTO;
using Savi.Model;
using Savi.Utility.Pagination;

namespace Savi.Core.IServices
{
    public interface IAdminService
    {
        ApiResponse<GroupDTO> GetGroupSavingById (string groupId);
        Task<ApiResponse<GroupTransactionDto>> GetGroupTransactionsAsync(int page, int perPage);
        Task<ApiResponse<PageResult<List<GroupDTO3>>>> GetAllGroupSavingsTodayAsync(int perPage, int page);
    }
}
