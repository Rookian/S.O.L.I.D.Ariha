﻿using System;
using System.Web;
using DependencyResolution;
using StructureMap;

namespace UserInterface.HttpModules
{
    public class DependencyRegistrarModule : IHttpModule
    {
        private static bool _dependenciesRegistered;
        private static readonly object Lock = new object();

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        public void Dispose() { }

        private static void context_BeginRequest(object sender, EventArgs e)
        {
            EnsureDependenciesRegistered();
        }

        private static void EnsureDependenciesRegistered()
        {
            if (!_dependenciesRegistered)
            {
                lock (Lock)
                {
                    if (!_dependenciesRegistered)
                    {
                        ObjectFactory.ResetDefaults();
                        new DependencyRegistrar().ConfigureOnStartup();
                        _dependenciesRegistered = true;
                    }
                }
            }
        }
    }
}