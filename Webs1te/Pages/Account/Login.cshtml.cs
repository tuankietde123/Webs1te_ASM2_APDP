using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Webs1te.Pages.Account
{

    public class LoginModel : PageModel

    {
        public string ErrorMessage { get; set; }
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string connectionString = "Data Source=KIETPC;Initial Catalog=myuser;Integrated Security=True";
            string query = "SELECT Role FROM Users WHERE Username=@Username AND Password=@Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", Input.Username);
                command.Parameters.AddWithValue("@Password", Input.Password);

                try
                {
                    connection.Open();
                    var roleObject = command.ExecuteScalar();
                    var role = roleObject.ToString();
                    if (role != null)
                    {
                        if (role == "admin")
                        {
                            // Nếu là admin, chuyển hướng đến trang Index của admin
                            return RedirectToPage("/Clients/Index");
                        }
                        else if (role == "Student")
                        {
                            // Nếu là student, chuyển hướng đến trang Index của student
                            return RedirectToPage("/Clients/Index1");
                        }
                        else
                        {
                            // Nếu là vai trò khác, hiển thị thông báo lỗi
                            ModelState.AddModelError(string.Empty, "Invalid role");
                            return Page();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while processing your request. Please try again later.");
                    _logger.LogError(ex, "An error occurred while processing login request.");
                    return Page();
                }
            }
        }

    }
}
