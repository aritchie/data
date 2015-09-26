using System;
using NUnit.Framework;


namespace Acr.Data.Ef.Tests {

    public abstract class AbstractModuleTestFixture {

        protected virtual void SetupModules() {}


        [TestFixtureSetUp]
        public virtual void OnFixtureSetup() {}


        [TestFixtureTearDown]
        public virtual void OnFixtureTearDown() {
            TestDbContext.Modules.Clear();
        }
    }
}
