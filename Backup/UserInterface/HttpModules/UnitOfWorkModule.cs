﻿using System;
using System.Web;
using Core.Domain.Bases;
using Core.Domain.Factories;

namespace UserInterface.HttpModules
{
    public class UnitOfWorkModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        public void Dispose() { }

        private static void context_BeginRequest(object sender, EventArgs e)
        {
            IUnitOfWork instance = UnitOfWorkFactory.GetDefault();
            instance.Begin();
        }

        private static void context_EndRequest(object sender, EventArgs e)
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