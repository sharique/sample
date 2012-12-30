using System;
using System.Linq;
using NUnit.Framework;
using Sample.Data;

namespace Sample.Tests
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void SiteInstalledFail()
        {
            var setting = Repository.Instance.GetSetting("installed");
            //Assert.AreEqual("yes",setting.First());
            Assert.IsNull(setting);
        }
    }
}

