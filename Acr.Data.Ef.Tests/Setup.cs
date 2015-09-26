using System;
using System.Data.Entity;
using HibernatingRhinos.Profiler.Appender.EntityFramework;
using NUnit.Framework;


namespace Acr.Data.Ef.Tests {

    [SetUpFixture]
    public class Setup {

        [SetUp]
        public void OnStartUp() {
            EntityFrameworkProfiler.Initialize();
            Database.SetInitializer(new DropCreateDatabaseAlways<TestDbContext>());
        }
    }
}
