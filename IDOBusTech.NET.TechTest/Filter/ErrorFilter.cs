namespace IDOBusTech.NET.TechTest.Filter
{
    using System.Web.Mvc;

    public class ErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/Index.cshtml"
            };          
           
        }

    }
}