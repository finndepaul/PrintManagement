
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using PrintManagement.Application.InterfaceServices;
using PrintManagement.Application.Payloads.Mappers.Converters;
using PrintManagement.Application.Payloads.RequestModels.CustomerRequests;
using PrintManagement.Application.Payloads.ResponseModels;
using PrintManagement.Application.Payloads.Responses;
using PrintManagement.Domain.Entities;
using PrintManagement.Domain.InterfaceRepositories;
using PrintManagement.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.ImplementServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseRepository<Customer> _baseCustomerRepository;
        private readonly ICustomerRepostory _customerRepostory;
        private readonly CustomerConverter _customerConverter;

        public CustomerService(IHttpContextAccessor contextAccessor, IBaseRepository<Customer> baseCustomerRepository, ICustomerRepostory customerRepostory, CustomerConverter customerConverter)
        {
            _contextAccessor = contextAccessor;
            _baseCustomerRepository = baseCustomerRepository;
            _customerRepostory = customerRepostory;
            _customerConverter = customerConverter;
        }

        public async Task<ResponseObject<DataResponseCustomer>> CreateCustomer(CustomerCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _customerRepostory.GetCustomerByPhoneNumberAsync(request.PhoneNumber, cancellationToken) != null)
            {
                return new ResponseObject<DataResponseCustomer>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "SĐT này đã được sử dụng"
                };
            }
            if (!CommonValidation.IsValidPhoneNumber(request.PhoneNumber))
            {
                return new ResponseObject<DataResponseCustomer>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "SĐT sai định dạng! Vui lòng viết lại SĐT"
                };
            }
            var currentUser = _contextAccessor.HttpContext.User;
            var teamIdClaim = currentUser.Claims.FirstOrDefault(c => c.Type == "TeamId");
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseCustomer>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Token của người dùng không hợp lệ"
                };
            }
            if (currentUser.IsInRole("Employee") && teamIdClaim.Value.Equals("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"))
            {
                var customer = new Customer
                {
                    Address = request.Address,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                };
                var model = await _baseCustomerRepository.CreateAsync(customer, cancellationToken);
                return new ResponseObject<DataResponseCustomer>
                {
                    Data = await _customerConverter.EntityToDTOAsync(model),
                    Status = StatusCodes.Status201Created,
                    Message = "Tạo hồ sơ khách hàng thành công!!!"
                };          
            }
            else
            {
                return new ResponseObject<DataResponseCustomer>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Chỉ nhân viên trong phòng ban Sales mới được thực hiện chức năng này!!!"
                };
            }
        }

        public async Task<IQueryable<DataResponseCustomer>> GetAllCustomer(CancellationToken cancellationToken)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            var teamIdClaim = currentUser.Claims.FirstOrDefault(c => c.Type == "TeamId");
            if (!currentUser.Identity.IsAuthenticated)
            {
                return null;
            }
            if (currentUser.IsInRole("Admin") || currentUser.IsInRole("Employee") && teamIdClaim.Value.Equals("6f8624c1-d28f-43f3-9465-bee9d20ecbf6"))
            {
                List<DataResponseCustomer> lstDTO = new List<DataResponseCustomer>();
                var model = await _baseCustomerRepository.GetAllAsync(null, cancellationToken);
                foreach (var item in model)
                {
                    DataResponseCustomer modelItem = await _customerConverter.EntityToDTOAsync(item);
                    lstDTO.Add(modelItem);
                }
                return lstDTO.AsQueryable();
            }
            else
            {
                return null;
            }
        }
    }
}
