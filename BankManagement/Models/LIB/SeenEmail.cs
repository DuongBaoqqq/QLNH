using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
namespace BankManagement.Models.LIB
{
    public class SeenEmail
    {
        private MailMessage msg;
        private SmtpClient client;
        private static SeenEmail _Instance;
        public string OTP { get; set; }
        public static SeenEmail Instance
        {
            get { return _Instance ?? (_Instance = new SeenEmail()); }
            set { }
        }
        private SeenEmail()
        {
            msg = new MailMessage();
            client = new SmtpClient();
            msg.From = new MailAddress("pbl3quanlinganhang@gmail.com");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("pbl3quanlinganhang@gmail.com", "yfhiqrwvbnuvykal");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
        }
        public void SeenNewCreateAccount(string Email, string Name, string PhoneNumber, string Password, string PinCode)
        {
            msg.To.Add(Email);
            msg.Subject = "SmartBank New Account";
            string message = "<p style='font-size:20px;'>Xin chào " + Name + "</p>";
            message += "<p>Bạn vừa mở tài khoản ngân hàng với số điện thoại: " + PhoneNumber + "</p>";
            message += "<p>Mật khẩu để đăng nhập vào tài khoản của bạn là:</p>";
            message += "<p style='font-size:20px'>" + Password + "</p>";
            message += "<p>Mã pin của bạn là:</p>";
            message += "<p style='font-size:20px'>" + PinCode + "</p>";
            message += "<p>Cảm ơn quý khách đã sử dụng dịch vụ.</p>";
            msg.Body = message;
            msg.IsBodyHtml = true;
            client.Send(msg);
        }
        public void SeenOTPForgetPass(string Email, string NameCustomer, string PhoneNumber)
        {
            OTP = Function.OTPForgetPass();
            msg.To.Add(Email);
            msg.Subject = "SmartBank OTP Request";
            string message = "<p style='font-size:20px;'>Xin chào " + NameCustomer + "</p>";
            message += "<p>Gần đây bạn vừa yêu cầu cấp lại mật khẩu cho tài khoản: " + PhoneNumber + "</p>";
            message += "<p>Nếu đó là yêu cầu của bạn, mã OTP lấy lại mật khẩu của bạn là:</p>";
            message += "<p style='font-size:20px'>" + OTP + "</p>";
            message += "<p>Nếu đây không phải là yêu cầu của bạn hãy bỏ qua email này.</p>";
            msg.Body = message;
            msg.IsBodyHtml = true;
            client.Send(msg);
        }
        public void SeenNewPassword(string Email, string Name, string PhoneNumber,string Pass)
        {
            msg.To.Add(Email);
            msg.Subject = "SmartBank Password Reset Request";
            string message = "<p style='font-size:20px;'>Xin chào " + Name + "</p>";
            message += "<p>Gần đây bạn vừa yêu cầu cấp lại mật khẩu cho tài khoản: " + PhoneNumber + "</p>";
            message += "<p>Nếu đó là yêu cầu của bạn, mật khẩu để đăng nhập vào tài khoản của bạn là:</p>";
            message += "<p style='font-size:20px'>" + Pass + "</p>";
            message += "<p>Cảm ơn quý khách đã sử dụng dịch vụ.</p>";
            msg.Body = message;
            msg.IsBodyHtml = true;
            client.Send(msg);
        }
        public bool CheckOTP(string OTP)
        {
            if (this.OTP == OTP)
            {
                return true;
            }
            return false;
        }
    }
}