using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.Controllers
{
    public class MyController : IController
    {
        public void Execute(RequestContext requestContext)//контекст запроса, включающий все подряд
        {
            string ip = requestContext.HttpContext.Request.UserHostAddress;//узнаем адрес пользователя
            var response = requestContext.HttpContext.Response;
            response.Write("<h2>Ваш IP-адрес: " + ip + "</h2>");//посылаем ответ пользователю
        }
    }
}