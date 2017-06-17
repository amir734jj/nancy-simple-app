using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using ViewModels;
using DataBaseManager;
using Mono.CSharp;
using System.Linq;
using Nancy.ModelBinding;
using Nancy.Session;
using Nancy.Cookies;
using Nancy.Bootstrapper;

namespace webserver {

    public class Routes : Nancy.NancyModule {
        public Routes() {

            Get["/"] = _ => {
                if (Session["user"] == null) {
                    return View["index.sshtml", new ViewData()];
                } else {
                    return View["index.sshtml", new ViewData((User)(Session["user"]))];
                }
            };

            Get["/login"] = _ => {
                if (Session["user"] == null) {
                    return View["login.sshtml", new ViewData()];
                } else {
                    return Response.AsRedirect("/");
                }
            };

            Get["/register"] = _ => {
                if (Session["user"] == null) {
                    return View["register.sshtml", new ViewData()];
                } else {
                    return Response.AsRedirect("/");
                }
            };

            Get["/logout"] = _ => {
                Session["user"] = null;
                return Response.AsRedirect("/");
            };

            Post["/register"] = _ => {
                User user = this.Bind<User>();
                user.AfterInitialization();

                user = DBSqlite.AddUser(user);

                if (user != null) {
                    return Response.AsRedirect("/login");
                } else {
                    return View["register.sshtml", new ViewData("Username is already taken, please try again.")];
                }
            };

            Post["/login"] = _ => {
                User user = this.Bind<User>();
                user.AfterInitialization();

                user = DBSqlite.GetUser(user);

                if (user != null) {
                    Session["user"] = user;
                    return Response.AsRedirect("/");
                } else {
                    return View["login.sshtml", new ViewData("Invalid username or password.")];
                }
            };
        }
    }



    /// <summary>
    ///     /static folder is public
    /// </summary>
    public class ApplicationBootstrapper : DefaultNancyBootstrapper {
        protected override void ConfigureConventions(NancyConventions nancyConventions) {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("public", "public"));

            nancyConventions.ViewLocationConventions.Add((viewName, model, context) => {
                    return string.Concat("Views/", viewName);
                });

            base.ConfigureConventions(nancyConventions);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines  pipelines) {
            CookieBasedSessions.Enable(pipelines);
        }
    }


    class Program {
        static void Main(string[] args) {
            var urlObject = new Uri("http://localhost:8080");
            List<Uri> urlList = new List<Uri>();
            urlList.Add(urlObject);

            HostConfiguration hostConfigs = new HostConfiguration() {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };


            using (NancyHost host = new NancyHost(new ApplicationBootstrapper(), hostConfigs, urlList.ToArray())) {
                StaticConfiguration.DisableErrorTraces = false;

                host.Start();
                Console.WriteLine("Press any [Enter] to close the host.");
                Console.ReadLine();
            }
        }
    }
}
