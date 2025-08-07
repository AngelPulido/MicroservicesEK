using Confluent.Kafka;
using EK.Microservices.Cqrs.Core.Events;
using Microservices.Ek.Query.Domain;
using Microservices.Ek_Query_Application.Contracts.Persistence;
using Microservices.Ek_Query_Application.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Microservices.Ek.Query.Infrastructure.Consumers
{

    public class MicroservicesEkConsumerService : IHostedService
    {
        private readonly IAsyncServiceEmail _asyncEmailService;
        public KafkaSettings _kafkaSettings { get; }

        public MicroservicesEkConsumerService(IServiceScopeFactory factory)
        {
            _asyncEmailService = factory.CreateScope().ServiceProvider.GetRequiredService<IAsyncServiceEmail>();
            _kafkaSettings = (factory.CreateScope().ServiceProvider.GetRequiredService<IOptions<KafkaSettings>>().Value);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = _kafkaSettings.GroupId,
                BootstrapServers = $"{_kafkaSettings.Hostname}:{_kafkaSettings.Port}",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
                {
                    var microservicesEkTopics = new string[]
                    {
                        typeof(EmailSent).Name
                    };
                    consumerBuilder.Subscribe(microservicesEkTopics);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);

                            if (consumer.Topic == typeof(EmailSent).Name)
                            {
                                var emailSent = JsonConvert.DeserializeObject<EmailSent>(consumer.Message.Value);

                                //var EmailSent = new EmailSentService
                                //{
                                //    To = emailSent!.To,
                                //    Subject = emailSent.Subject,
                                //    Body = emailSent.Body,
                                //    NameFile = emailSent.NameFile,
                                //    Base64Content = emailSent.Base64Content

                                //};
                                _asyncEmailService.SendEmailWithFileAsync(emailSent!.To, emailSent.Subject, emailSent.Body, emailSent.NameFile,emailSent.Base64Content);

                                //await _asyncEmailService.SendEmailWithFileAsync(
                                //    emailSent.To,
                                //    emailSent.Subject,
                                //    emailSent.Body,
                                //    emailSent.NameFile,
                                //    emailSent.Base64Content);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
