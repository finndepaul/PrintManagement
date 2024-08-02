using PrintManagement.Application.Payloads.RequestModels.ResourseForPrintjobRequest;
using PrintManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.InterfaceServices
{
    public interface IResourseForPrintjobService
    {
        Task AddResourseForPrintJob(ResourseForPrintjobCreateRequest request, CancellationToken cancellationToken);
    }
}
