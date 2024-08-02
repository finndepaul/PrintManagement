using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.Mappers.Converters
{
    public class TeamConverter
    {
        private readonly ITeamRepository _teamRepository;

        public TeamConverter(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<DataResponseTeam> EntityToDTOAsync(Team team, CancellationToken cancellationToken)
        {
            return new DataResponseTeam
            {
                Id = team.Id,
                Name = team.Name,
                Description = team.Description,
                NumberOfMember = await _teamRepository.GetNumberOfMemberAsync(team.Id, cancellationToken),
                ManagerName = team.ManagerId != null ? await _teamRepository.GetManagerNameAsync(team.ManagerId, cancellationToken) : "N/A"
            };
        }
    }
}
