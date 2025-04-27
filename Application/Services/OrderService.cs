using Application.Commands.Notification;
using Application.DTOs.Orders;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.IRepostories;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static Domain.Models.Car;
using static Domain.Models.Notification;
using static Domain.Models.Order;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerService _customerService;
        private readonly INotificationService _notificationService;

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository, IUserRepository userRepository, ICustomerRepository customerRepository, IMapper mapper, ITransactionRepository transactionRepository, ICustomerService customerService, INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
            _transactionRepository = transactionRepository;
            _customerService = customerService;
            _notificationService = notificationService;
        }


        public async Task CreateOrder(string carId, ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException();
            }

            // User
            var existUser = await _userRepository.FindByIdAsync(userId);

            if (existUser is null)
                throw new Exception("User is not found!");

            // Customer
            var existCustomer = await _customerRepository.GetByUserId(userId);

            if (existCustomer is null)
                throw new Exception("No customer associated with this user found.");

            // Car
            var existCar = await _carRepository.FindByIdAsync(carId);

            if (existCar is null)
                throw new Exception("Car is not found!");

            if (existCar.Status == CarStatus.Sold)
                throw new Exception("This car has already been sold.");


            var order = new Order(existCar.Id, existUser.Id, existCustomer.Id, existCar.Price);


            await _orderRepository.Add(order);

            // Send Notification

            var buyerNotif = new CreateNotificationCommand()
            {
                UserId = userId,
                Message = "Your order has been successfully placed and is being reviewed.",
                Type = NotificationType.Email
            };

            await _notificationService.CreateNotification(buyerNotif);

            var managerIds = await _userRepository.GetAdminUserIdsAsync();

            foreach (var id in managerIds)
            {
                var managerNotif = new CreateNotificationCommand()
                {
                    UserId = id,
                    Message = "A new order was placed by the customer.",
                    Type = NotificationType.Email
                };
                await _notificationService.CreateNotification(managerNotif);
            }
        }

        public async Task ApproveOrders(string orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order is null)
                throw new Exception("Order is not found!");

            if (order.Status != OrderStatus.Pending)
                throw new Exception("Only pending orders can be Approved.");

            order.UpdateStatus(OrderStatus.Approved);

            order.UpdateStatus(OrderStatus.AwaitingPayment);

            _orderRepository.SaveChange();

            // Send Notification

            var customerNotif = new CreateNotificationCommand()
            {
                UserId = order.UserId,
                Message = "Your order has been confirmed by the store! It is being prepared.",
                Type = NotificationType.SMS
            };

            await _notificationService.CreateNotification(customerNotif);
        }

        public async Task RejectOrders(string orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order is null)
                throw new Exception("Order is not found!");

            order.UpdateStatus(OrderStatus.Rejected);

            await _orderRepository.SaveChange();

            // Send Notification

            var customerNotif = new CreateNotificationCommand()
            {
                UserId = order.UserId,
                Message = "Your order was rejected by the store!",
                Type = NotificationType.SMS
            };

            await _notificationService.CreateNotification(customerNotif);
        }

        public async Task<List<GetAllPendingOrdersDto>> GetAllPendingOrders()
        {
            var orderList = await _orderRepository.GetAllPendingOrders();

            if (orderList is null)
                throw new Exception();

            if (orderList.Count == 0)
                throw new Exception("Order not found!");

            var orderListDto = _mapper.Map<List<GetAllPendingOrdersDto>>(orderList);
            
            return orderListDto;
        }

        public async Task UploadReceipt(string orderId, IFormFile receiptFile, string WebRootPath)
        {

            var order = await _orderRepository.GetById(orderId);


            if (receiptFile == null || receiptFile.Length == 0)
                throw new Exception("Please select the receipt file.");

            var uploadsFolder = Path.Combine(WebRootPath, "uploads", "receipts");

            if(!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{receiptFile.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await receiptFile.CopyToAsync(stream);
            }


            var transaction = new AppTransaction(orderId,order.TotalAmount,fileName);

            await _transactionRepository.Add(transaction);

            if (order.Status != OrderStatus.AwaitingPayment)
                throw new Exception("Only AwaitingPayment orders can be PaymentUploaded.");

            order.UpdateStatus(OrderStatus.PaymentUploaded);

            await _orderRepository.SaveChange();
        }

        public async Task<List<GetAllPaymentUploadedOrdersDto>> GetAllPaymentUploadedOrders()
        {
            var orderList = await _orderRepository.GetAllPaymentUploadedOrders();

            if (orderList is null)
                throw new Exception();

            if (orderList.Count == 0)
                throw new Exception("Order not found!");

            var orderListDto = _mapper.Map<List<GetAllPaymentUploadedOrdersDto>>(orderList);

            return orderListDto;
        }

        public async Task ApprovePayments(string orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if(order is null)
                throw new Exception("Order not found!");

            if (order.Status != OrderStatus.PaymentUploaded)
                throw new Exception("Only PaymentUploaded orders can be changed.");

            order.UpdateStatus(OrderStatus.PaymentConfirmed);

            await _orderRepository.SaveChange();

            // Change Car Status

            var car = await _carRepository.FindByIdAsync(order.CarId);

            if (car is null)
                throw new Exception("Car not found!");

            car.ChangeStatus(CarStatus.Sold);

            await _carRepository.SaveChangeAsync();


            // Add Loyalty Points for customer

            await _customerService.AddLoyaltyPoints(order.CustomerId, order.TotalAmount);
            var customer = await _customerRepository.GetById(order.CustomerId);
            await _customerRepository.Update(customer);

            // Send Notification
            var customerNotif = new CreateNotificationCommand()
            {
                UserId = order.UserId,
                Message = "Your payment receipt has been approved by the store manager.",
                Type = NotificationType.Email
            };

            await _notificationService.CreateNotification(customerNotif);
        }

        public async Task RejectPayments(string orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order is null)
                throw new Exception("Order not found!");

            if (order.Status != OrderStatus.PaymentUploaded)
                throw new Exception("Only PaymentUploaded orders can be changed.");

            order.UpdateStatus(OrderStatus.Rejected);

            await _orderRepository.SaveChange();

            // Send Notification 
            var customerNotif = new CreateNotificationCommand()
            {
                UserId = order.UserId,
                Message = "Your payment receipt was rejected by the store manager.",
                Type = NotificationType.Email
            };

            await _notificationService.CreateNotification(customerNotif);
        }

        public async Task<InvoiceDto> GenerateInvoiceFromOrder(string orderId)
        {
            var order = await _orderRepository.GetById(orderId);

            if (order is null)
                throw new Exception("Order not found!");

            if (order.Status != OrderStatus.PaymentConfirmed)
                throw new Exception("Only users whose payment status has been confirmed have invoices.");

            var invoiceDto = new InvoiceDto
            {
                InvoiceNumber = order.InvoiceNumber,
                CustomerName = order.Customer.FullName,
                CustomerPhone = order.Customer.PhoneNumber,
                Price = order.car.Price,
                CarTitle = $"{order.car.Brand} - {order.car.Model}",
                orderDate = order.OrderDate,
                PaidDate = order.Transaction.TransactionDate,
                OrderID = order.Id
            };

            // Send Notification

            var customerNotif = new CreateNotificationCommand()
            {
                UserId = order.UserId,
                Message = "Your order invoice has been issued.",
                Type = NotificationType.Email
            };

            await _notificationService.CreateNotification(customerNotif);

            return invoiceDto;
        }
         
        public async Task<List<GetUserOrdersDto>> GetUserOrders(string userId)
        {
            var orderList = await _orderRepository.GetOrdersByUserId(userId);

            if (orderList is null)
                throw new Exception("Order not found!");

            var orderListDto = new List<GetUserOrdersDto>();

            foreach (var order in orderList)
            {
                orderListDto.Add(
                    new GetUserOrdersDto()
                    {
                        OrderDate = order.OrderDate,
                        Status = order.Status.ToString(),
                        CarBrand = order.car.Brand,
                        CarModel = order.car.Model,
                        CarPrice = order.car.Price
                    });
            }

            foreach(var order in orderListDto)
            {
                switch (order.Status)
                {
                    case "0":
                        order.Status = "Pending";
                        break;
                    case "1":
                        order.Status = "Approved";
                        break;
                    case "2":
                        order.Status = "Rejected";
                        break;
                    case "3":
                        order.Status = "AwaitingPayment";
                        break;
                    case "4":
                        order.Status = "PaymentUploaded";
                        break;
                    case "5":
                        order.Status = "PaymentConfirmed";
                        break;
                    case "6":
                        order.Status = "Canceled";
                        break;
                }
            }

            return orderListDto;
        }



    }
}
