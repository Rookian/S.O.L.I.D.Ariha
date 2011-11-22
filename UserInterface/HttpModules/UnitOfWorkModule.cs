using System;
using System.Web;
using Core.Factories;
using Core.Interfaces;

namespace UserInterface.HttpModules
{
    public class UnitOfWorkModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
        }

        public void Dispose() { }

        private static void ContextBeginRequest(object sender, EventArgs e)
        {
            IUnitOfWork instance = UnitOfWorkFactory.GetDefault();
            instance.Begin();
        }

        private static void ContextEndRequest(object sender, EventArgs e)
        {
            IUnitOfWork instance = UnitOfWorkFactory.GetDefault();
            try
            {
                instance.Commit();
            }
            catch
            {
                instance.RollBack();
                throw;
            }
            finally
            {
                instance.Dispose();
            }
        }
    }
}