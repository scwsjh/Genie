using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : AdminController
	{
		private readonly IAdminUserService userService;

		public UserController(IAdminUserService _userService)
		{
			userService = _userService;
		}

		public async Task<IActionResult> Index()
		{
			var userList = await userService.GetAdminUserList();
			return View(userList);
		}

		public async Task<IActionResult> Edit(string id)
		{
			var userInfo = await userService.GetAdminUserInfo(id);
			if (userInfo == null)
			{
				userInfo = new admin_user();
			}
			return View(userInfo);
		}

		public async Task<IActionResult> UpStatus(string id)
		{
			var userInfo = await userService.GetAdminUserInfo(id);
			if (userInfo == null)
			{
				return MessageHelper.MsgPage(this, "用户不存在!", "/Admin/User/Index");
			}
			userInfo.status = userInfo.status == 1 ? 0 : 1;
			if (await userService.UpdateAdminUser(userInfo))
			{
				return MessageHelper.MsgPage(this, "操作成功!", "/Admin/User/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "操作失败!", "/Admin/User/Index");
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditUser(string adminId, int no, int status)
		{
			string url = "/Admin/User/Index";
			var userInfo = await userService.GetAdminUserInfo(adminId);
			if (userInfo == null)
			{
				admin_user user = new admin_user();
				user.adminId = StringHelper.NewGuid;
				user.adminNo = no;
				user.name = "";
				user.pwd = "";
				user.status = status;
				user.addTime = DateTime.Now;
				if (await userService.AddAdminUser(user))
				{
					return MessageHelper.MsgPage(this, "添加成功!", url);
				}
				else
				{
					return MessageHelper.MsgPage(this, "添加失败!", url);
				}
			}
			else
			{
				userInfo.adminNo = no;
				userInfo.status = status;
				if (await userService.UpdateAdminUser(userInfo))
				{
					return MessageHelper.MsgPage(this, "修改成功!", url);
				}
				else
				{
					return MessageHelper.MsgPage(this, "操作失败!", url);
				}
			}
		}

		public async Task<IActionResult> DelUser(string id)
		{
			var userInfo = await userService.GetAdminUserInfo(id);
			if (userInfo == null)
			{
				return MessageHelper.MsgPage(this, "账号信息不存在!", "/Admin/User/Index");
			}
			if (await userService.DelAdminUser(userInfo.adminId))
			{
				return MessageHelper.MsgPage(this, "删除成功!", "/Admin/User/Index");
			}
			else
			{
				return MessageHelper.MsgPage(this, "删除失败!", "/Admin/User/Index");
			}
		}
	}
}