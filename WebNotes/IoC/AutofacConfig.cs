using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebNotes.bl.Interfaces;
using WebNotes.bl.Services;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Repository;

namespace WebNotes.IoC
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<WebNotesUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<NoteService>().As<INoteService>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}