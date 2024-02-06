﻿using Savi.Core.DTO;

namespace Savi.Core.IServices
{
    public interface IGroupSavingsMembersServices
    {
        Task<ResponseDto<bool>> JoinGroupSavingsAsync(string userId, string groupId);
    }
}
