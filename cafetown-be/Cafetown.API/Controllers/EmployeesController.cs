using Cafetown.BL;
using Cafetown.Common;
using Cafetown.Common.Entities.DTO;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private readonly IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xuất file excel danh sách nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>File excel</returns>
        /// Modified by: TTTuan (5/3/2023)
        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] string? keyword)
        {
            try
            {
                var stream = _employeeBL.ExportExcel(keyword);
                string excelName = $"{ExcelResource.Export_Excel_FileName}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        [HttpGet("login")]
        public IActionResult login([FromQuery] string username, [FromQuery] string password)
        {
            try
            {

                byte[] bytes = Convert.FromBase64String(password);
                string pass = Encoding.UTF8.GetString(bytes);
                var employee = _employeeBL.login(username, pass);

                if (employee != null)
                {
                    if (employee.Password != null)
                    {
                        //Convert thành base64
                        byte[] bytesPass = Encoding.UTF8.GetBytes(employee.Password);
                        employee.Password = Convert.ToBase64String(bytesPass);
                    }
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, employee.EmployeeName),
                        // Thêm các claim khác tùy theo yêu cầu của ứng dụng
                    };
                    // gen token
                    // Tạo khóa bí mật từ chuỗi
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key_here_asda_dasda_ads"));

                    // Tạo mã xác thực (credentials) từ khóa bí mật
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Tạo token
                    var token = new JwtSecurityToken(
                        issuer: "",
                        audience: "",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(1), // Thời gian hết hạn của token
                        signingCredentials: creds);

                    // Trả về token dưới dạng chuỗi
                    var jsonToken = new JwtSecurityTokenHandler().WriteToken(token);

                    return StatusCode(StatusCodes.Status200OK, new LoginResult
                    {
                        Token = jsonToken,
                        UserName = employee.EmployeeName,
                        IsManager = employee.IsManager
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidInput,
                        DevMsg = ErrorResultResource.DevMsg_InvalidInput,
                        UserMsg = ErrorResultResource.UserMsg_InvalidInput,
                        MoreInfo = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <param name="record">Đối tượng bản ghi cần sửa</param>
        /// <returns>ID của bản ghi vừa sửa</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpPut("login/{id}")]
        public IActionResult UpdateRecordByLogin([FromRoute] Guid id, [FromBody] Employee record)
        {
            try
            {
                var result = _employeeBL.UpdateRecordByLogin(id, record);

                // Xử lý kết quả trả về
                if (result.StatusResponse == StatusResponse.Done)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result.StatusResponse == StatusResponse.Invalid || result.StatusResponse == StatusResponse.DuplicateCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        ErrorCode = ErrorCode.UpdateFailed,
                        DevMsg = ErrorResultResource.DevMsg_UpdateFailed,
                        UserMsg = ErrorResultResource.UserMsg_UpdateFailed,
                        MoreInfo = ErrorResultResource.MoreInfo_UpdateFailed,
                        TraceID = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <param name="record">Đối tượng bản ghi cần sửa</param>
        /// <returns>ID của bản ghi vừa sửa</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpPost("sendMail/{mail}")]
        public IActionResult SendEmail([FromRoute] string mail = "hovananh2312@gmail.com")
        {
            try
            {
                _employeeBL.UpdateByEmail(mail);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("hovananh2312@gmail.com"));
                email.To.Add(MailboxAddress.Parse(mail));
                email.Subject = "Mật khẩu tài khoản quản lý cửa hàng cafe Văn Anh của bạn đã được reset";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = AuthenResource.ResetPassword
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("hovananh2312@gmail.com", "mmcasyotjpbirixx");
                smtp.Send(email);
                smtp.Disconnect(true);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }
        #endregion
    }
}
