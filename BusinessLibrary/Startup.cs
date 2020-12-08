using AutoMapper;
using BusinessLibrary.BusinesLogic.BLServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // .... Ignore code before this

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
