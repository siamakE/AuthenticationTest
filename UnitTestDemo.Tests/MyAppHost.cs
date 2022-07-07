using Funq;
using ServiceStack;
using UnitTestDemo.ServiceInterface;
using System;

namespace UnitTestDemo.Tests2;

public partial class IntegrationTest
{
    class MyAppHost : AppSelfHostBase
    {
        private readonly string webHostUrl;
        private readonly Action<Container> configureFn;

        public MyAppHost(string webHostUrl, Action<Container> configureFn = null)
            : base(nameof(IntegrationTest), typeof(MyServices).Assembly)
        {
            this.webHostUrl = webHostUrl;
            this.configureFn = configureFn;
        }

        public override void Configure(Container container)
        {
        }
    }
}