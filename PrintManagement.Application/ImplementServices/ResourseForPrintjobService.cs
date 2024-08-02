using Microsoft.AspNetCore.Http;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.ResourseForPrintjobRequest;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.ImplementServices
{
    public class ResourseForPrintjobService : IResourseForPrintjobService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<ResourceForPrintJob> _baseResourceForPrintJobRepository;
        private readonly IBaseRepository<ResourcePropertyDetail>  _baseResourcePropertyDetailRepository;
        private readonly IBaseRepository<PrintJobs>  _basePrintJobRepository;
        private readonly IBaseRepository<Design>  _baseDesignRepository;
        private readonly IBaseRepository<Project>  _baseProjectRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IBaseRepository<Bill> _baseBillRepository;
        private readonly IEmailService _emailService;
        public ResourseForPrintjobService(IBaseRepository<ResourceForPrintJob> baseResourceForPrintJobRepository, IBaseRepository<ResourcePropertyDetail> baseResourcePropertyDetailRepository, IBaseRepository<PrintJobs> basePrintJobRepository, IHttpContextAccessor contextAccessor, IBaseRepository<Design> baseDesignRepository, IBaseRepository<Project> baseProjectRepository, IBaseRepository<Customer> baseCustomerRepository, IEmailService emailService, IBaseRepository<Bill> baseBillRepository)
        {
            _baseResourceForPrintJobRepository = baseResourceForPrintJobRepository;
            _baseResourcePropertyDetailRepository = baseResourcePropertyDetailRepository;
            _basePrintJobRepository = basePrintJobRepository;
            _contextAccessor = contextAccessor;
            _baseDesignRepository = baseDesignRepository;
            _baseProjectRepository = baseProjectRepository;
            _emailService = emailService;
            _baseCustomerRepository = baseCustomerRepository;
            _baseBillRepository = baseBillRepository;
        }

        public async Task AddResourseForPrintJob(ResourseForPrintjobCreateRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new ArgumentNullException("Token của người dùng không hợp lệ");
            }
            if (currentUser.IsInRole("Leader"))
            {
                var machine = await _baseResourcePropertyDetailRepository.GetByIdAsync(x => x.Id == request.Machine, cancellationToken);
                decimal totalMoney = machine.Price;
                if (machine == null)
                {
                    throw new ArgumentNullException("Không tìm thấy máy móc");
                }
                var lst = new List<ResourceForPrintJob>
                {
                    new ResourceForPrintJob {PrintJobId = request.PrintJobId, ResourcePropertyDetailId = request.Machine, Quantity = 0},
                };
                foreach (var item in request.ResourseRequests)
                {
                    lst.Add(new ResourceForPrintJob { PrintJobId = request.PrintJobId, Quantity = item.Quantity, ResourcePropertyDetailId = item.Resourse });
                    var resoursePropertyDetail = await _baseResourcePropertyDetailRepository.GetByIdAsync(x => x.Id == item.Resourse, cancellationToken);
                    if(item.Quantity > resoursePropertyDetail.Quantity)
                    {
                        throw new ArgumentException(nameof(item) + "Số lượng không đủ");
                    }
                    resoursePropertyDetail.Quantity -= item.Quantity;
                    await _baseResourcePropertyDetailRepository.UpdateAsync(resoursePropertyDetail, cancellationToken);

                    totalMoney += resoursePropertyDetail.Quantity * resoursePropertyDetail.Price;
                }
                var model = await _baseResourceForPrintJobRepository.CreateAsync(lst, cancellationToken);
                var printjob = await _basePrintJobRepository.GetByIdAsync(x => x.Id == request.PrintJobId, cancellationToken);
                printjob.PrintJobStatus = Domain.Enumerates.PrintJobStatus.Success;
                await _basePrintJobRepository.UpdateAsync(printjob, cancellationToken);
                var design = await _baseDesignRepository.GetByIdAsync(x => x.Id == printjob.DesignId, cancellationToken);
                var project = await _baseProjectRepository.GetByIdAsync(x => x.Id == design.ProjectId, cancellationToken);
                var customer = await _baseCustomerRepository.GetByIdAsync(x => x.Id == project.CustomerId, cancellationToken);
                var bill = new Bill
                {
                    BillName = project.ProjectName,
                    BillStatus = Domain.Enumerates.BillStatus.Waiting,
                    TotalMoney = totalMoney,
                    ProjectId = project.Id,
                    CustomerId = customer.Id,
                    CreateTime = DateTime.Now,
                    TradingCode = DateTime.Now.ToString(),
                };
                await _baseBillRepository.CreateAsync(bill, cancellationToken);
                var emailMessage = new EmailMessage(new string[] { customer.Email }, "Thông tin đơn hàng:", $"Mã giao dịch: {bill.TradingCode} | Tên hóa đơn: Hóa đơn thanh toán | Tổng tiền: {totalMoney} | Tên khách hàng: {customer.FullName} | Ngày tạo: {bill.CreateTime}\nTrân trọng,\nDuy Dong Entertainment");
                _emailService.SendEmail(emailMessage);
            } 
            else
            {
                throw new ArgumentException("Bạn không có quyền thực hiện chức năng này");
            }
        }
    }
}
