using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

[RoutePrefix("Api")]
public class BTController : ApiController
{
	protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());

	private static readonly Guid TeacherApi = Guid.Parse("8646F44F-A854-4AF5-820F-BCC00E31BB51");

    private static readonly Guid SignApi = Guid.Parse("8646F44F-A854-4AF5-820F-BCC00E31BB52");

    [HttpGet]
	[Route("Sign/{key}")]
    public dynamic Get(Guid key, Guid token)
	{
		if (HomoryContext.Value.Api.Count(o => o.Id == SignApi && o.ProviderKey == key && o.State < State.审核) > 0)
		{
			// Comment out the following if no log.
			var providerId =
				HomoryContext.Value.Api.First(o => o.Id == TeacherApi && o.ProviderKey == key && o.State < State.审核).ProviderId;
			LogApi(TeacherApi, providerId);
            // Comment out the above if no log.
            //return HomoryContext.Value.Teacher.ToList();
            // To return part of the information
            if (HomoryContext.Value.UserOnline.Count(o => o.Id == token) == 0)
                return null;
            var u = HomoryContext.Value.UserOnline.First(o => o.Id == token).User;
            return new
            {
                账号 = u.Account,
                姓名 = u.RealName,
                昵称 = u.DisplayName
            };
		}
		else
			return null;
	}

    [HttpGet]
    [Route("Teacher/{key}")]
    public IEnumerable<dynamic> Get(Guid key)
    {
        if (HomoryContext.Value.Api.Count(o => o.Id == TeacherApi && o.ProviderKey == key && o.State < State.审核) > 0)
        {
            // Comment out the following if no log.
            var providerId =
                HomoryContext.Value.Api.First(o => o.Id == TeacherApi && o.ProviderKey == key && o.State < State.审核).ProviderId;
            LogApi(TeacherApi, providerId);
            // Comment out the above if no log.
            //return HomoryContext.Value.Teacher.ToList();
            // To return part of the information
            return HomoryContext.Value.Teacher.ToList().Select(o => new
            {
                手机 = o.Phone,
                身份识别码 = o.User.Account
            });
        }
        else
            return null;
    }

    protected void LogApi(Guid apiId, string providerId)
	{
		HomoryContext.Value.ApiLog.Add(new ApiLog
		{
			Id = HomoryContext.Value.GetId(),
			ApiId = apiId,
			ProviderId = providerId,
			Time = DateTime.Now
		});
		HomoryContext.Value.SaveChanges();
	}
}
