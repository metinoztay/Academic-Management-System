using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcademicManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        DbAcademicMsContext dbAcademicMsContext = new DbAcademicMsContext();
        TblUser activeAdmin = new TblUser();
        public IActionResult Index(TblUser admin)
        {
            if (!ActiveUser.isSettedInformations)
                SetActiveUserInformations(admin);

            GetActiveUserInformations();
            return View(activeAdmin);
        }

        private void GetActiveUserInformations()
        {
            activeAdmin.Username = ActiveUser.Username;
            activeAdmin.Name = ActiveUser.Name;
            activeAdmin.Surname = ActiveUser.Surname;
            activeAdmin.Email = ActiveUser.Email;
            activeAdmin.Phone = ActiveUser.Phone;
            activeAdmin.Password = ActiveUser.Password;
            activeAdmin.Authority = ActiveUser.Authority;
            activeAdmin.Adress = ActiveUser.Adress;
            activeAdmin.District = ActiveUser.District;
            activeAdmin.Province = ActiveUser.Province;
            activeAdmin.SecurityKey = ActiveUser.SecurityKey;
        }

        private void SetActiveUserInformations(TblUser adminModel)
        {
            ActiveUser.Username = adminModel.Username;
            ActiveUser.Name = adminModel.Name;
            ActiveUser.Surname = adminModel.Surname;
            ActiveUser.Email = adminModel.Email;
            ActiveUser.Phone = adminModel.Phone;
            ActiveUser.Password = adminModel.Password;
            ActiveUser.Authority = adminModel.Authority;
            ActiveUser.Adress = adminModel.Adress;
            ActiveUser.District = adminModel.District;
            ActiveUser.Province = adminModel.Province;
            ActiveUser.SecurityKey = adminModel.SecurityKey;
            ActiveUser.isSettedInformations = true;
        }
    }
}
