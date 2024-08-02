using Microsoft.AspNetCore.Http;
using PrintManagement.Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.RequestModels.DesignRequests
{
    public class UploadFileRequest
    {
        public Guid ProjectId { get; set; }
        public IFormFile FilePath { get; set; } // using
    }
}
