using Microsoft.AspNetCore.Http;
using PrintManagement.Application.Handle.HandleEmail;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.RequestModels.DeliveryRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.Enumerates;
using PrintManagement.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.ImplementServices
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Delivery> _baseDeliveryRepository;
        private readonly IBaseRepository<Bill> _baseBillRepository;
        private readonly IBaseRepository<Project> _baseProjectRepository;
        private readonly IBaseRepository<User> _baseUserRepository;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly IEmailService _emailService;
        public DeliveryService(IHttpContextAccessor contextAccessor, IBaseRepository<Delivery> baseDeliveryRepository, IBaseRepository<Bill> baseBillRepository, IBaseRepository<Project> baseProjectRepository, IBaseRepository<User> baseUserRepository, IBaseRepository<Customer> baseCustomerRepository, IEmailService emailService)
        {
            _contextAccessor = contextAccessor;
            _baseDeliveryRepository = baseDeliveryRepository;
            _baseBillRepository = baseBillRepository;
            _baseProjectRepository = baseProjectRepository;
            _baseUserRepository = baseUserRepository;
            _baseCustomerRepository = baseCustomerRepository;
            _emailService = emailService;
        }

        public async Task<string> AcceptBill(Guid billId, bool IsAccept, CancellationToken cancellationToken)
        {
            if (IsAccept)
            {
                var bill = await _baseBillRepository.GetByIdAsync(x => x.Id == billId, cancellationToken);
                bill.BillStatus = BillStatus.Delivering;
                bill.UpdateTime = DateTime.Now;
                await _baseBillRepository.UpdateAsync(bill, cancellationToken);
                
                var delivery = await _baseDeliveryRepository.GetByIdAsync(x => x.ProjectId == bill.ProjectId, cancellationToken);
                delivery.DeliveryStatus = DeliveryStatus.Delivering;
                await _baseDeliveryRepository.UpdateAsync(delivery, cancellationToken);

                return "Xác nhận đơn hàng thành công";
            }
            else
            {
                return "Bạn đã hủy thao tác";
            }
            
        }

        public async Task ConfirmBill(Guid billId, DeliveryConfirmRequest deliveryStatus, CancellationToken cancellationToken)
        {
            var bill = await _baseBillRepository.GetByIdAsync(x => x.Id == billId, cancellationToken);
            var delivery = await _baseDeliveryRepository.GetByIdAsync(x => x.ProjectId == bill.ProjectId, cancellationToken);
            var customer = await _baseCustomerRepository.GetByIdAsync(x => x.Id == bill.CustomerId, cancellationToken);
            var project = await _baseProjectRepository.GetByIdAsync(x => x.Id == bill.ProjectId, cancellationToken);

            var emailMessage = new EmailMessage(new string[] {customer.Email}, "Thông tin đơn hàng:", "Đơn hàng của bạn đã đc giao hàng công, Vui lòng phản hồi lại email này nếu bạn đã nhận đc hàng");
            _emailService.SendEmail(emailMessage);
            if (deliveryStatus.ToString() == BillStatus.Received.ToString())
            {
                bill.BillStatus = BillStatus.Received;
                bill.UpdateTime = DateTime.Now;
                await _baseBillRepository.UpdateAsync(bill, cancellationToken);

                delivery.DeliveryStatus = DeliveryStatus.Deliveried;
                await _baseDeliveryRepository.UpdateAsync(delivery, cancellationToken);

                project.ProjectStatus = ProjectStatus.Accomplished;
                await _baseProjectRepository.UpdateAsync(project, cancellationToken);
            }
            else if (deliveryStatus.ToString() == BillStatus.NotReceived.ToString())
            {
                bill.BillStatus = BillStatus.NotReceived;
                bill.UpdateTime = DateTime.Now;
                await _baseBillRepository.UpdateAsync(bill, cancellationToken);

                delivery.DeliveryStatus = DeliveryStatus.Deliveried;
                await _baseDeliveryRepository.UpdateAsync(delivery, cancellationToken);
            }
            else
            {
                bill.BillStatus = BillStatus.Refuse;
                bill.UpdateTime = DateTime.Now;
                await _baseBillRepository.UpdateAsync(bill, cancellationToken);

                delivery.DeliveryStatus = DeliveryStatus.Deliveried;
                await _baseDeliveryRepository.UpdateAsync(delivery, cancellationToken);
            }
        }

        public async Task<string> DeliveryForEmployee(DeliveryForEmployeeRequest request, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return "Token của người dùng không hợp lệ";
            }
            if (!currentUser.IsInRole("Leader"))
            {
                return "Người dùng không có quyền thực hiện chức năng này";
            }
            var project = await _baseProjectRepository.GetByIdAsync(x => x.Id == request.ProjectId, cancellationToken);
            var customer = await _baseCustomerRepository.GetByIdAsync(x => x.Id == project.CustomerId, cancellationToken);
            var user = await _baseUserRepository.GetByIdAsync(x => x.Id == request.EmployeeId, cancellationToken);
            if (user == null)
            {
                return "Không tìm thấy người dùng";
            }
            if (user.TeamId != Guid.Parse("63A8C386-755E-4620-A58A-3A2C3126D28A"))
            {
                return "Người dùng không thuộc phòng ban giao hàng";
            }
            var delivery = new Delivery
            {
                ShippingMethodId = Guid.Parse("2DDFD76B-D034-4D48-BE98-B651D59D1538"),
                CustomerId = project.CustomerId,
                DeliverId = 0, 
                ProjectId = request.ProjectId,
                DeliveryAddress = customer.Address,
                EstimateDeliveryTime = request.EstimateDeliveryTime,
                DeliveryStatus = DeliveryStatus.Waiting,
            };
            await _baseDeliveryRepository.CreateAsync(delivery, cancellationToken);

            var bill = await _baseBillRepository.GetByIdAsync(x => x.ProjectId == request.ProjectId, cancellationToken);
            bill.EmployeeId = request.EmployeeId;
            bill.UpdateTime = DateTime.Now;
            await _baseBillRepository.UpdateAsync(bill, cancellationToken);
            return "Giao hàng cho nhân viên thành công";
        }

        public async Task<IQueryable<DataResponseBill>> GetAllBill(Guid userId, CancellationToken cancellationToken)
        {
            List<DataResponseBill> lstData = new List<DataResponseBill>();
            var bills = await _baseBillRepository.GetAllAsync(x => x.EmployeeId  == userId, cancellationToken);
            foreach (var item in bills)
            {
                var bill = new DataResponseBill
                {
                    BillName = item.BillName,
                    CustomerName = _baseCustomerRepository.GetByIdAsync(x => x.Id == item.CustomerId, cancellationToken).Result.FullName,
                    CustomerAddress = _baseCustomerRepository.GetByIdAsync(x => x.Id == item.CustomerId, cancellationToken).Result.Address,
                    ShippingMethodName = "Nhanh",
                    EmployeeName = _baseCustomerRepository.GetByIdAsync(x => x.Id == item.CustomerId, cancellationToken).Result.FullName,
                    BillStatus = item.BillStatus.ToString(),
                };
                lstData.Add(bill);
            }
            return lstData.AsQueryable();
        }

        public async Task<IQueryable<DataResponseDelivery>> GetDelivererForBill(Guid projectId, CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return null;
            }
            if (!currentUser.IsInRole("Leader"))
            {
                throw new ArgumentException("Bạn không có quyền thực hiện chức năng này");
            }

            List<DataResponseDelivery> lstData = new List<DataResponseDelivery>();
            var project = await _baseProjectRepository.GetByIdAsync(x => x.Id == projectId, cancellationToken);
            var customer = await _baseCustomerRepository.GetByIdAsync(x => x.Id == project.CustomerId, cancellationToken);
            var bills = await _baseBillRepository.GetAllAsync(null, cancellationToken);

            foreach (var bill in bills)
            {
                var customerItem = await _baseCustomerRepository.GetByIdAsync(x => x.Id == bill.CustomerId, cancellationToken);
                if (customerItem.Address.ToLower().Trim().Equals(customer.Address.ToLower().Trim()))
                {
                    if (bill.EmployeeId != null)
                    {
                        var delivery = new DataResponseDelivery()
                        {
                            Id = Guid.Parse(bill.EmployeeId.ToString()),
                            FullName = (await _baseUserRepository.GetByIdAsync(x => x.Id == bill.EmployeeId, cancellationToken)).FullName
                        };
                        lstData.Add(delivery);
                    }
                }
            }
            if (lstData.Count == 0)
            {
                foreach (var item in await _baseUserRepository.GetAllAsync(x => x.TeamId == Guid.Parse("63A8C386-755E-4620-A58A-3A2C3126D28A"), cancellationToken))
                {
                    var data = new DataResponseDelivery
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                    };
                    lstData.Add(data);
                }
            }

            // Loại bỏ các phần tử trùng lặp
            var distinctData = lstData.Distinct(new DataResponseDeliveryComparer()).ToList();
            return distinctData.AsQueryable();
        }


    }
}
