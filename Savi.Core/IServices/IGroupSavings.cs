using Savi.Core.DTO;
namespace Savi.Core.IServices
{
    public interface IGroupSavings
    {
        ResponseDto<List<GroupDTO>> GetExploreGroupSavingGroups();
        ResponseDto<GroupDTO> GetExploreGroupSavingDetails(string groupId);
        ResponseDto<GroupDTO> GetGroupSavingAccountDetails(string groupId);
        ResponseDto<List<GroupDTO>> GetListOfActiveSavingsGroups();
        Task<ResponseDto<string>> CreateSavingsGroup(GroupDTO2 dto);
        ResponseDto<List<GroupDTO>> GetListOfAllGroupSavings();
        Task<ResponseDto<int>> GetTotalSavingsGroupCountAsync();
    }
}
