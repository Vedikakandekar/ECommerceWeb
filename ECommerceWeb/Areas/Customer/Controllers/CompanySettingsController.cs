using ECommerce.Data.Repository.IRepository;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CompanySettingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanySettingsCacheRepository _companySettingsCacheRepository;
        public CompanySettingsController(ICompanySettingsCacheRepository companySettingsCacheRepository,IUnitOfWork unitOfWork)
        {
            _companySettingsCacheRepository = companySettingsCacheRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UpdateSettings(string SettingName,string SettingValue)
        {
            //CompanySettings features = _unitOfWork.CompanySettings.Get(u=>u.Name==SettingName);
            //if (features == null)
            //{
            //    return BadRequest("Feature Does not Exists");
            //}
            //features.Value = SettingValue;
            //_unitOfWork.CompanySettings.Update(features);
            //_unitOfWork.Save();
            //_companySettingsCacheRepository.AddFeatureToCache(SettingName, SettingValue);
            return View("Index");
        }
    }
}
