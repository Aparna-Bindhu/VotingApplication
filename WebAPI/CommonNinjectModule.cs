using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Voting.Domain.CommandHandler;

namespace WebAPI
{
    public class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<VotersService>().ToSelf(); // Bind GetAllVotersCommandHandler to itself
        }
    }
}