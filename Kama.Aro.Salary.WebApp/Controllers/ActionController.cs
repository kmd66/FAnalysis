using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kama.Aro.Salary.WebApp.Controllers
{
    public class ActionController : BaseController
    {
        public ActionController(Organization.ApiClient.Interface.IActionService actionService)
        {
            _actionService = actionService;
        }

        readonly Organization.ApiClient.Interface.IActionService _actionService;

        [HttpPost]
        public async Task<JsonResult> Add(Organization.Core.Model.Action model)
        {
            var result = await _actionService.Add(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Edit(Organization.Core.Model.Action model)
        {
            var result = await _actionService.Edit(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Get(Guid id)
        {
            var result = await _actionService.Get(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> List(Organization.Core.Model.ActionListVM model)
        {
            var result = await _actionService.List(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> Remove(Guid id)
        {
            var result = await _actionService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ListByControllerID(Guid id)
        {
            var result = await _actionService.ListByControllerID(id);
            return Json(result);
        }

    }
}