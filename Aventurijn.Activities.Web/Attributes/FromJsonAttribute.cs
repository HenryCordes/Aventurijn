using System.Web.Mvc;
using Newtonsoft.Json;

namespace Aventurijn.Activities.Web.Attributes
{
    public class FromJsonAttribute : CustomModelBinderAttribute
    {

        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {

                var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(stringified))
                    return null;


                return JsonConvert.DeserializeObject(stringified, bindingContext.ModelType);
            }
        }
    }
}