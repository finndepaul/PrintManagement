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
    public class ProjectConverter
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectConverter(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<DataResponseProject> EntityToDTOAsync(Project project, CancellationToken cancellationToken)
        {
            return new DataResponseProject
            {
                ProjectName = project.ProjectName,
                RequestDescriptionFromCustomer = project.RequestDescriptionFromCustomer,
                CustomerName = project.CustomerId != null ? await _projectRepository.GetCustomerNameAsync(project.CustomerId, cancellationToken) : "N/A",
                StartDate = project.StartDate,
                ExpectedEndDate = project.ExpectedEndDate,
                EmployeeName = project.EmployeeId != null ? await _projectRepository.GetLeaderNameAsync(project.EmployeeId, cancellationToken) : "N/A",
                ProjectStatus = project.ProjectStatus.ToString(),
            };
        }
    }
}
