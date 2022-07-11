using Moq;
using NUnit.Framework;
using OnlineStore;
using OnlineStore.Commands;
using OnlineStore.Entity.AccountEntity;
using OnlineStore.Entity.AccountEntity.RoleEnum;
using OnlineStore.Entity.CommercialEntity;
using OnlineStore.Service;
using OnlineStore.Service.ServiceInterface;
using OnlineStore.StoreRepository.Entity.CommercialEntity.OrderStatusEnum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineStoreTests
{
    public class StoreTests
    {

        private Dictionary<string, IService> services;
        private Dictionary<string, IService> servicesForMoq;

        [SetUp]
        public void Setup()
        {
            services = new Dictionary<string, IService>()
            {
                {"productService", new ProductService() },
                {"orderService", new OrderService() },
                {"userService", new UserService() }
            };

            servicesForMoq = new Dictionary<string, IService>();
        }
        
        #region OrderServiceTests

        [Test]
        public void AddOrderTest_Scenario_AddingNewProductToOrderRepository()
        {
            var order = new Order();
            var orderService = (OrderService)services["orderService"];

            orderService.AddOrder(order);

            Assert.AreEqual(order, orderService.GetOrderRepository().Last());
        }

        [Test]
        public void SetOrderIdTest_Scenario_SettingIdToNewOrder()
        {
            var order = new Order();
            var orderService = (OrderService)services["orderService"];

            orderService.AddOrder(order); 
            orderService.SetOrderId(order);

            Assert.AreEqual(order.Id, (orderService.GetOrderRepository().Count() + 1));
        }

        [Test]
        public void GetOrderByIdTest_Scenario_GettingOrderFromrepositoryById()
        {
            var order = new Order();
            var orderService = (OrderService)services["orderService"];

            orderService.AddOrder(order);
            orderService.SetOrderId(order);
            var tryOrder = orderService.GetOrdeById(orderService.GetOrderRepository().Count() + 1);

            Assert.AreEqual(order, tryOrder);
        }

        [Test]
        public void GetOrderQntTest_Scenario_GettingOrdersQnt()
        {
            var order = new Order();
            var orderService = (OrderService)services["orderService"];

            orderService.AddOrder(order);
            var ordersQnt = orderService.GetOrderQnt();

            Assert.AreEqual(orderService.GetOrderRepository().Count, ordersQnt);
        }

        #endregion

        #region ProductServiceTests

        [TestCase("Coke")]
        [TestCase("Pork")]
        [TestCase("White Bread")]
        public void GetProductByNameTest_Scenario_FindingProductByName(string name)
        {
            var productService = (ProductService)services["productService"];

            var product = productService.GetProductByName(name);
            var tryProduct = productService.GetProducts().First(x => x.Name == name);

            Assert.AreEqual(product, tryProduct);
        } 

        [Test]
        public void AddProductTests_Scenario_AddingNewProductToRepository()
        {
            var product = new Product();
            var productService = (ProductService)services["productService"];

            productService.AddProduct(product);

            Assert.AreEqual(product, productService.GetProducts().Last());
        }

        #endregion

        #region UserServiceTests

        [Test]
        public void AddUserTest_Scenario_AddingNewUser()
        {
            var user = new User();
            var userService = new UserService();

            userService.AddUser(user);

            Assert.AreEqual(user, userService.GetUserList().Last());
        }


        [TestCase("arkadii")]
        public void GetUserByName_Scenario_FindingUserByName(string name)
        {
            var userService = new UserService();

            var user = userService.GetUserByName(name);
            var tryUser = userService.GetUserList().First(x => x.Login == name);

            Assert.AreEqual(user, tryUser);
        }

        [TestCase("arkadii", "123")]
        [TestCase("admin", "admin")]
        public void GetUser_Scenario_FindingUserByLoginAndPassword(string login, string password)
        {
            var userService = new UserService();

            var user = userService.GetUser(login, password);
            var tryUser = userService.GetUserList().First(x => x.Login == login && x.Password == password);

            Assert.AreEqual(user, tryUser);
        }

        #endregion

        #region CommandsTests

        [TestCase("Pineapple", "Fruits", 5.5, "Tropical fruit")]
        [TestCase("Beef", "Meat", 17.5, "Fresh meat")]
        public void AddNewProductCmdTest_Scenario_AddingNewProductToRepository(string name,
            string category, decimal price, string description)
        {
            var product = new Product();
            product.SetProductInfo(name, category, description, price);
            var addNewProductCmd = new AddNewProduct(services);
            var productService = (ProductService)services["productService"];
            var expectedList = productService.GetProducts();
            expectedList.Add(product);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(name);
            stringBuilder.AppendLine(category);
            stringBuilder.AppendLine(description);
            stringBuilder.AppendLine(price.ToString());

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            addNewProductCmd.Execute();

            var actualList = productService.GetProducts();

            Assert.AreEqual(expectedList, actualList);
        }

        [TestCase("admin", "admin", 1, 1, true)]
        public void ChangeOrderStatusCmdTest_Scenario_ChangingOrderStatus
            (string login, string password, int id, int operation, bool resultOfAdminVer)
        {
            Mock<OrderService> orderService = new Mock<OrderService>();
            Mock<UserService> userService = new Mock<UserService>();

            var expectedOrder = new Order();
            expectedOrder.Status = OrderStatus.Canceled_by_the_administrator;

            var actualOrder = new Order();
            actualOrder.CurrentOrder = new List<Product>()
            {
                new Product { Name = "Sosage", Category = "Meat", Description = "For BBQ", Cost = 12.5m }      
            };
            actualOrder.Id = 1;
            orderService.Object.GetOrderRepository().Add(actualOrder);

            orderService.Setup(os => os.IsOrderHistoryEmpty()).Returns(false);
            userService.Setup(us => us.IsAdmin(login, password)).Returns(resultOfAdminVer);
            orderService.Setup(os => os.GetOrderQnt()).Returns(id);

            servicesForMoq.Add("orderService", orderService.Object);
            servicesForMoq.Add("userService", userService.Object);

            var changeOrderStatusCmd = new ChangeOrderStatus(servicesForMoq, login, password);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(id.ToString());
            stringBuilder.AppendLine(operation.ToString());

            var stringReader = new StringReader(stringBuilder.ToString());

            Console.SetIn(stringReader);

            changeOrderStatusCmd.Execute();

            Assert.AreEqual(expectedOrder.Status, actualOrder.Status);
        }
        
        [TestCase("cybermaster", "12345", "1", "cutiecat", "54321", "54321")]
        public void ChangePersonalInfoCmdTest_Scenario_ChangingOfUserAccountInformation
            (string login, string password, string action, string newLogin, string newPassword, string tryNewPassword)
        {
            Mock<UserService> userService = new Mock<UserService>();

            var expectedUser = new User();
            expectedUser.Login = newLogin;
            expectedUser.Password = newPassword;

            var actualUser = new User();
            actualUser.Login = login;
            actualUser.Password = password;
            userService.Object.AddUser(actualUser);

            servicesForMoq.Add("userService", userService.Object);

            var changePersonalInfoCmd = new ChangePersonalInfo(servicesForMoq, login, password);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(action);
            stringBuilder.AppendLine(newLogin);
            stringBuilder.AppendLine(newPassword);
            stringBuilder.AppendLine(tryNewPassword);
            stringBuilder.AppendLine((8).ToString());

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            changePersonalInfoCmd.Execute();

            Assert.AreEqual(expectedUser.Login,  actualUser.Login);
            Assert.AreEqual(expectedUser.Password, actualUser.Password);
        }

        [TestCase("Coke", "Fanta", "Beverage", "Sweet drinks", 5.5)]
        public void ChangeProductInfoCmdTest_Scenario_ChangingInfromationOfProduct
            (string name, string newName, string newCategory, string newDescription, decimal newCost)
        {
            var productService = (ProductService)services["productService"];

            var changeProductInfoCmd = new ChangeProductInfo(services);
            var product = new Product();
            product.Name = newName;
            product.Category = newCategory;
            product.Description = newDescription;
            product.Cost = newCost;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(name);
            stringBuilder.AppendLine(newName);
            stringBuilder.AppendLine(newCategory);
            stringBuilder.AppendLine(newDescription);
            stringBuilder.AppendLine(newCost.ToString());

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            changeProductInfoCmd.Execute();

            Assert.AreEqual(product.Name, productService.GetProductByName(newName).Name);
            Assert.AreEqual(product.Category, productService.GetProductByName(newName).Category);
            Assert.AreEqual(product.Description, productService.GetProductByName(newName).Description);
            Assert.AreEqual(product.Cost, productService.GetProductByName(newName).Cost);
        }

        [TestCase("admin", "admin", "Watermelon", "1", "8")]
        public void CreateOrderCmdTest_Scenario_CreatingNewOrder
            (string login, string password, string productName, string operation, string action)
        {
            Mock<UserService> userService = new Mock<UserService>();
            Mock<ProductService> productService = new Mock<ProductService>();
            var orderService = (OrderService)services["orderService"];

            var product = new Product();
            product.Name = productName;
            product.Category = "Berries";
            product.Description = "Base";
            product.Cost = 4;

            var products = new List<Product>();
            products.Add(product);

            productService.Setup(ps => ps.IsProductInList(productName)).Returns(true);
            productService.Setup(ps => ps.GetProductByName(productName)).Returns(product);
            userService.Setup(us => us.IsAdmin(login, password)).Returns(false);

            servicesForMoq.Add("userService", userService.Object);
            servicesForMoq.Add("productService", productService.Object);
            servicesForMoq.Add("orderService", orderService);

            var createOrderCmd = new CreateOrder(servicesForMoq, login, password);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(productName);
            stringBuilder.AppendLine(operation);
            stringBuilder.AppendLine(action);

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            createOrderCmd.Execute();

            Assert.AreEqual(products, orderService.GetOrdeById(1).CurrentOrder);
        }

        [TestCase("admin", "admin")]
        [TestCase("arkadii", "123")]
        public void LogInUserCmdTest_Scenrio_EnteringToUserAccount(string login, string password)
        {
            Mock<UserService> userService = new Mock<UserService>();

            var user = new User();
            user.Login = login;
            user.Password = password;
            if (login is "admin")
                user.Role = Role.Administrator;
            else
                user.Role = Role.User;

            userService.Object.AddUser(user);

            servicesForMoq.Add("userService", userService.Object);

            var logInUserCmd = new LogInUser(servicesForMoq);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(login);
            stringBuilder.AppendLine(password);

            if (userService.Object.GetUserByName(login).Role is Role.Administrator)
                stringBuilder.AppendLine((10).ToString());
            else
                stringBuilder.AppendLine((8).ToString());

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            logInUserCmd.Execute();

            var output = stringWriter.ToString();

            Assert.IsTrue(output.Contains("Wellcome!"));
        }

        [TestCase("astra", "abc123", "abc123")]
        public void RegisterUserCmdTest_Scenario_RegisterNewUser
            (string login, string password, string tryPassword)
        {
            Mock<UserService> userService = new Mock<UserService>();

            servicesForMoq.Add("userService", userService.Object);

            var registerUser = new RegisterUser(servicesForMoq);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(login);
            stringBuilder.AppendLine(password);
            stringBuilder.AppendLine(tryPassword);
            stringBuilder.AppendLine((8).ToString());

            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            registerUser.Execute();

            Assert.IsTrue(userService.Object.GetUserList()
                .Contains(userService.Object.GetUserByName(login)));
        }

        #endregion

    }
}