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
    public class DesignConverter
    {
        private readonly IBaseRepository<User> _baseUserRepository;

        public DesignConverter(IBaseRepository<User> baseUserRepository)
        {
            _baseUserRepository = baseUserRepository;
        }

        public async Task<DataResponseDesign> EntityToDto(Design design, CancellationToken cancellationToken)
        {
            return new DataResponseDesign
            {
                Id = design.Id,
                FilePath = design.FilePath,
                DesignerName = design.DesignerId != null ? _baseUserRepository.GetByIdAsync(x => x.Id == design.DesignerId, cancellationToken).Result.FullName : "N/A",
                DesignTime = design.DesignTime,
                DesignStatus = design.DesignStatus.ToString(),
            };
        }
    }
}
