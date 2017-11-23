﻿using System.Collections.Generic;
using System.Linq;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Core
{
    public class ApplicationConfigService : IApplicationConfigService
    {
        IApplicationConfigRepository _repository;
        public ApplicationConfigService(IApplicationConfigRepository repository)
        {
            _repository = repository;
        }

        public List<ApplicationConfig> ListApplicationConfig()
        {
            var entities = _repository.ListEntities().ToList();
            return entities;
        }
    }
}
