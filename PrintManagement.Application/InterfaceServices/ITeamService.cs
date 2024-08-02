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
        Task<IQueryable<DataResponseTeam>> GetAllTeam(CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseTeam>> CreateTeam(TeamCreateRequest request, CancellationToken cancellationToken);
        Task<ResponseObject<DataResponseTeam>> UpdateTeam(TeamUpdateRequest request, CancellationToken cancellationToken);
        Task<string> DeleteTeam(TeamDeleteRequest request, CancellationToken cancellationToken);
        Task<string> SwapManager(Guid teamId, Guid managerId, CancellationToken cancellationToken);
    }
}
