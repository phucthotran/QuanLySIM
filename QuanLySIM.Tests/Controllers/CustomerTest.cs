using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuanLySIM.Controllers;
using Moq;
using QuanLySIM.Data.EntityRepositories;
using QuanLySIM.Entities;
using QuanLySIM.Data.DbInteractions;
using System.Web.Mvc;
using QuanLySIM.Tests.FakeRepositories;
using QuanLySIM.Tests.FakeServices;
using QuanLySIM.Services;
using NUnit.Framework;

namespace QuanLySIM.Tests.Controllers
{
    [TestFixture]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class CustomerTest
    {
        private ICustomerRepository MockCustomerRepository;
        private IDbService MockDbService;

        public CustomerTest()
        {
            Mock<IDbService> mockDbService = new Mock<IDbService>();
            mockDbService.Setup(ms => ms.Customer).Returns(new FakeCustomerRepository());
            mockDbService.Setup(ms => ms.Order).Returns(new FakeOrderRepository());
            mockDbService.Setup(ms => ms.Group).Returns(new FakeGroupRepository());
            mockDbService.Setup(ms => ms.Staff).Returns(new FakeStaffRepository());
            mockDbService.Setup(ms => ms.Role).Returns(new FakeRoleRepository());
            mockDbService.Setup(ms => ms.SIM).Returns(new FakeSimRepository());

            this.MockDbService = mockDbService.Object;

            IList<KhachHang> customers = new List<KhachHang>
            {
                new KhachHang { MaKH = 1, TenTK = "user1234", MatKhau = "pass1234", Email = "email1@gmail.com", TenKH = "Khach Hang 1", DiaChi = "Dia Chi 1", CMND = "111111111", SDT = "011111111", SoLuongDaMua = 1, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) },
                new KhachHang { MaKH = 2, TenTK = "user2234", MatKhau = "pass2234", Email = "email2@gmail.com", TenKH = "Khach Hang 2", DiaChi = "Dia Chi 2", CMND = "211111111", SDT = "021111111", SoLuongDaMua = 2, MaNV = 2, NhanVien = new FakeStaffRepository().Find(2) },
                new KhachHang { MaKH = 3, TenTK = "user3234", MatKhau = "pass3234", Email = "email3@gmail.com", TenKH = "Khach Hang 3", DiaChi = "Dia Chi 3", CMND = "311111111", SDT = "031111111", SoLuongDaMua = 3, MaNV = 3, NhanVien = new FakeStaffRepository().Find(3) },
                new KhachHang { MaKH = 4, TenTK = "user4234", MatKhau = "pass4234", Email = "email4@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "411111111", SDT = "041111111", SoLuongDaMua = 4, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) },
                new KhachHang { MaKH = 5, TenTK = "user4234", MatKhau = "pass4234", Email = "email2@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "411111111", SDT = "051111111", SoLuongDaMua = 5, MaNV = 2, NhanVien = new FakeStaffRepository().Find(2) },
                new KhachHang { MaKH = 6, TenTK = "user6234", MatKhau = "pass4234", Email = "email6@gmail.com", TenKH = "Khach Hang 4", DiaChi = "Dia Chi 4", CMND = "611111111", SDT = "041111111", SoLuongDaMua = 6, MaNV = 1, NhanVien = new FakeStaffRepository().Find(1) }
            };

            Mock<ICustomerRepository> mockCustomerRepository = new Mock<ICustomerRepository>();

            //Get all customer
            mockCustomerRepository.Setup(mr => mr.All).Returns(customers);

            //Get customer by id
            mockCustomerRepository.Setup(mr => mr.Find(It.IsInRange(1, 6, Range.Inclusive))).Returns((int i) => customers.Where(x => x.MaKH == i).SingleOrDefault());

            //Get newest customers id
            mockCustomerRepository.Setup(mr => mr.NewestCustomerId()).Returns(customers.Last().MaKH);

            //Check account
            mockCustomerRepository.Setup(mr => mr.IsAccountExist(It.IsAny<String>())).Returns((String s) => customers.Where(x => x.TenTK == s).SingleOrDefault() != null);

            //Check email
            mockCustomerRepository.Setup(mr => mr.IsEmailExist(It.IsAny<String>())).Returns((String s) => customers.Where(x => x.Email == s).SingleOrDefault() != null);

            //Check passport
            mockCustomerRepository.Setup(mr => mr.IsIdCardExist(It.IsAny<String>())).Returns((String s) => customers.Where(x => x.CMND == s).SingleOrDefault() != null);

            //Check phone
            mockCustomerRepository.Setup(mr => mr.IsPhoneNumExist(It.IsAny<String>())).Returns((String s) => customers.Where(x => x.SDT == s).SingleOrDefault() != null);

            //Save
            mockCustomerRepository.Setup(mr => mr.Save(It.IsAny<KhachHang>())).Returns(
                    (KhachHang target) =>
                    {
                        if (target.MaKH.Equals(default(int)))
                        {
                            target.MaKH = customers.Count() + 1;
                            target.MaNV = It.IsInRange<int>(1, 3, Range.Inclusive);
                        }
                        else
                        {
                            var original = customers.Where(q => q.MaKH == target.MaKH).SingleOrDefault();

                            if (original == null)
                                return 0;

                            original.TenTK = target.TenTK;
                            original.MatKhau = target.MatKhau;
                            original.Email = target.Email;
                            original.DiaChi = target.DiaChi;
                            original.TenKH = target.TenKH;
                            original.CMND = target.CMND;
                            original.SDT = target.SDT;
                            original.MaNV = target.MaNV;
                        }

                        return 1;
                    }
                );

            //Save little            
            mockCustomerRepository.Setup(mr => mr.LittleSave(It.IsAny<KhachHang>())).Returns(
                    (KhachHang target) =>
                    {
                        target.MaKH = customers.Count() + 1;
                        target.MaNV = It.IsInRange<int>(1, 3, Range.Inclusive);

                        return 1;
                    }
                );

            //Update order amount
            mockCustomerRepository.Setup(mr => mr.UpdateOrderAmount(It.IsInRange<int>(1, 6, Range.Inclusive), It.IsAny<bool>()));

            //Delete
            mockCustomerRepository.Setup(mr => mr.Delete(It.IsInRange<int>(1, 6, Range.Inclusive))).Returns((int i) => {
                    KhachHang remove = customers.Where(x => x.MaKH == i).SingleOrDefault();
                    int countBefore = customers.Count;

                    customers.Remove(remove);

                    return countBefore - customers.Count;
                });

            this.MockCustomerRepository = mockCustomerRepository.Object;
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Profile_View_Result()
        {
            int customerId = 3;

            //Arrange
            var expectedCustomer = MockCustomerRepository.Find(customerId);
            var expectedIsNotCompleted = expectedCustomer.IsNotCompleted();

            //Act            
            CustomerController controller = new CustomerController(MockDbService);
            //controller.MaKH = customerId;

            var viewResult = (ViewResult)controller.Profile();
            var actualCustomer = viewResult.Model as KhachHang;
            var actualIsNotCompleted = viewResult.ViewBag.IsProfileNotCompleted;

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);
            Assert.AreEqual(expectedIsNotCompleted, actualIsNotCompleted);
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Profile_Update_Success()
        {
            int customerId = 3;

            //Arrange
            var expectedCustomer = MockCustomerRepository.Find(customerId);
            String expectedSuccessMsg = "Cập nhật thông tin thành công";

            //Act
            CustomerController controller = new CustomerController(MockDbService);
            //controller.MaKH = customerId;

            var viewResult = (ViewResult)controller.ProfileUpdate(expectedCustomer);
            var actualCustomer = viewResult.Model as KhachHang;
            var actualSuccessMsg = viewResult.ViewBag.SuccessMsg;

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);
            Assert.AreEqual(expectedSuccessMsg, actualSuccessMsg);
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Profile_Update_Email_Exist()
        {
            int customerId = 3;

            //Arrange
            var expectedCustomer = MockCustomerRepository.Find(customerId); expectedCustomer.Email = "email4@gmail.com";
            String expectedFailMsg = "Email đã tồn tại trên hệ thống";

            //Act
            CustomerController controller = new CustomerController(MockDbService);
            //controller.MaKH = customerId;

            var viewResult = (ViewResult)controller.ProfileUpdate(expectedCustomer);
            var actualCustomer = viewResult.Model as KhachHang;
            var actualFailMsg = viewResult.ViewBag.FailMsg;

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);
            Assert.AreEqual(expectedFailMsg, actualFailMsg);
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Profile_Update_IdCard_Exist()
        {
            int customerId = 3;

            //Arrange
            var expectedCustomer = MockCustomerRepository.Find(customerId); expectedCustomer.CMND = "611111111";
            String expectedFailMsg = "Số CMND đã tồn tại trên hệ thống";

            //Act
            CustomerController controller = new CustomerController(MockDbService);
            //controller.MaKH = customerId;

            var viewResult = (ViewResult)controller.ProfileUpdate(expectedCustomer);
            var actualCustomer = viewResult.Model as KhachHang;
            var actualFailMsg = viewResult.ViewBag.FailMsg;

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);
            Assert.AreEqual(expectedFailMsg, actualFailMsg);
        }

        [Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Profile_Update_PhoneNum_Exist()
        {
            int customerId = 3;

            //Arrange
            var expectedCustomer = MockCustomerRepository.Find(customerId); expectedCustomer.SDT = "051111111";
            String expectedFailMsg = "SĐT đã tồn tại trên hệ thống";

            //Act
            CustomerController controller = new CustomerController(MockDbService);
            //controller.MaKH = customerId;

            var viewResult = (ViewResult)controller.ProfileUpdate(expectedCustomer);
            var actualCustomer = viewResult.Model as KhachHang;
            var actualFailMsg = viewResult.ViewBag.FailMsg;

            //Assert
            Assert.AreEqual(expectedCustomer, actualCustomer);
            Assert.AreEqual(expectedFailMsg, actualFailMsg);
        }
    }
}
