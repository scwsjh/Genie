using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : AdminController
    {
        private readonly IGenieService genieService;

        public SkillController(IGenieService genieService)
        {
            this.genieService = genieService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await genieService.GetGenieSkillData();
            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            genie_skill data = await genieService.GetGenieSkillInfo(id);
            if (data == null)
            {
                data = new genie_skill();
            }
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> EditOk([FromForm] int skId, [FromForm] string name, [FromForm] string type, [FromForm] string attr)
        {
            genie_skill add = new genie_skill();
            add.skId = skId;
            add.name = name;
            add.type = type;
            add.attr = attr;

            if (await genieService.SaveGenieSkill(add))
            {
                return MessageHelper.MsgPage(this, "操作成功!", "/Admin/Skill/Index");
            }
            else
            {
                return MessageHelper.MsgPage(this, "操作失败!", "/Admin/Skill/Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await genieService.DeleteGenieSkill(id);
            if (result)
            {
                return MessageHelper.MsgPage(this, "删除成功!", "/Admin/Skill/Index");
            }
            else
            {
                return MessageHelper.MsgPage(this, "删除失败!", "/Admin/Skill/Index");
            }
        }
    }
}