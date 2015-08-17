﻿using System;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Azure.WebJobs.Host.Triggers;
using Redis.WebJobs.Extensions.Bindings;
using Redis.WebJobs.Extensions.Triggers;

namespace Redis.WebJobs.Extensions.Config
{
    internal class RedisExtensionConfig : IExtensionConfigProvider
    {
        private readonly RedisConfiguration _redisConfig;

        public RedisExtensionConfig(RedisConfiguration redisConfig)
        {
            if (redisConfig == null)
            {
                throw new ArgumentNullException("redisConfig");
            }

            _redisConfig = redisConfig;
        }

        public RedisConfiguration Config { get { return _redisConfig; } }

        /// <inheritdoc />
        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IExtensionRegistry extensions = context.Config.GetService<IExtensionRegistry>();

            RedisSubscribeTriggerAttributeBindingProvider triggerBindingProvider = new RedisSubscribeTriggerAttributeBindingProvider(_redisConfig);
            extensions.RegisterExtension<ITriggerBindingProvider>(triggerBindingProvider);

            RedisPublishAttributeBindingProvider bindingProvider = new RedisPublishAttributeBindingProvider(_redisConfig);
            extensions.RegisterExtension<IBindingProvider>(bindingProvider);

            RedisAddOrReplaceAttributeBindingProvider addOrReplaceBindingProvider = new RedisAddOrReplaceAttributeBindingProvider(_redisConfig);
            extensions.RegisterExtension<IBindingProvider>(addOrReplaceBindingProvider);

            RedisGetAttributeBindingProvider getBindingProvider = new RedisGetAttributeBindingProvider(_redisConfig);
            extensions.RegisterExtension<IBindingProvider>(getBindingProvider);
        }
    }
}
