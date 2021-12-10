using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockNess.Commons.PipleModel
{
    public class PipleModelBuilder: IPipleModelBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _components = new List<Func<RequestDelegate, RequestDelegate>>();
        public IPipleModelBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _components.Add(middleware);
            return this;
        }

        public RequestDelegate Build()
        {
            RequestDelegate app = context =>
            {
                return Task.CompletedTask;
            };
            foreach (var component in _components.Reverse())
            {
                app = component(app);
            }
            return app;
        }
    }
}
