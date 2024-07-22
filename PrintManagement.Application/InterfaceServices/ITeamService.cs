using PrintManagement.Application.Payloads.RequestModels.TeamRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface ITeamService
    {
        Task<IQueryable<DataResponseTeam>> GetAllAsync(CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseTeam>> CreateTeamAsync(TeamCreateRequest request, CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseTeam>> UpdateTeamAsync(TeamUpdateRequest request, CancellationToken cancellationToken);
        Task<string> DeleteTeamAsync(TeamDeleteRequest request, CancellationToken cancellationToken);
        Task<string> SwapManagerAsync(Guid teamId, Guid managerId, CancellationToken cancellationToken);
    }
}
