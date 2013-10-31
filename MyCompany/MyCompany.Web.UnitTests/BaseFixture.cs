using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyCompany.Web.UnitTests
{
    [TestFixture]
    public abstract class BaseFixture
    {
        public abstract void SetupContext();
        public abstract void Act();
        public virtual void PostAct() { }

        [SetUp]
        public virtual void Initialize()
        {
            SetupContext();
            try
            {
                Act();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                PostAct();
            }
        }
    }
}
