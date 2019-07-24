using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
 

namespace AA.RabbitMQ
{
  public class RabbitMqHelper
  {
    public Func<string, bool> ReceiveMessageCallback { get; set; }

    public readonly RabbitMqClientContext _Context;

    public RabbitMqHelper(string quequeName)
    {
      _Context = new RabbitMqClientContext();
      _Context.QueueName = quequeName;
    }

    public void TriggerEventMessage(EventMessage eventMessage, string exChange = null)
    {
      _Context.SendConnection = RabbitMqClientFactory.CreateConnection();

      using (_Context.SendConnection)
      {
        _Context.SendChannel = RabbitMqClientFactory.CreateModel(_Context.SendConnection);

        const byte deliveryModel = 2;

        using (_Context.SendChannel)
        {
          try
          {
            _Context.SendChannel.QueueDeclare(_Context.QueueName, false, false, false, null);
            var properties = _Context.SendChannel.CreateBasicProperties();
            properties.DeliveryMode = deliveryModel; //标示 持久化消息

            //推送消息
            var body = Encoding.UTF8.GetBytes(eventMessage.JsonBody);
            _Context.SendChannel.BasicPublish("", _Context.QueueName, properties, body);
          }
          catch (Exception ex) { }

        }
      }
    }
    /// <summary>
    /// 开始侦听默认的队列。
    /// </summary>
    public void OnListening()
    {
      try
      {
        Task.Factory.StartNew(ListenInit);
      }
      catch (Exception ex)
      {

      }
    }

    private void ListenInit()
    {
      _Context.ListenConnection = RabbitMqClientFactory.CreateConnection();
      _Context.ListenConnection.ConnectionShutdown += (sender, e) =>
      {
              // e.ReplyText;

            };

      _Context.ListenChannel = RabbitMqClientFactory.CreateModel(_Context.ListenConnection);

      var queue = _Context.ListenChannel.QueueDeclare(_Context.QueueName, false, false, false, null);

      var consumer = new EventingBasicConsumer(_Context.ListenChannel);//创建事件驱动的消费者类型

      consumer.Received += consumer_Received;
      _Context.ListenChannel.BasicQos(0, 1, false); //一次只获取一个消息进行消费
      _Context.ListenChannel.BasicConsume(_Context.QueueName, false, consumer);
       
    }

    private void consumer_Received(object sender, BasicDeliverEventArgs e)
    {
      try
      {
        var result = Encoding.UTF8.GetString(e.Body); //获取消息
        //触发外部侦听事件
        bool ms = ReceiveMessageCallback(result);
        if (ms)
        {
          if (!_Context.ListenChannel.IsClosed)
          {
            _Context.ListenChannel.BasicAck(e.DeliveryTag, false);
          }
        }
        else
        {

        }

      }
      catch (Exception)
      {

        throw;
      }
    }
  }
}
