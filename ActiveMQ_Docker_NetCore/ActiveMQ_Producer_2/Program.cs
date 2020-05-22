using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
namespace ActiveMQ_Producer_2
{
    class Program
    {
        static string connString = "activemq:tcp://192.168.134.139:61616";
        static string user = "xiajun";
        static string pwd = "1234@abc";
        static string queueName = "xiajun_test_queueName";
        static string topicName = "xiajun_test_topicName";

        static void Main(string[] args)
        {
            TestTopic();
        }
        /// <summary>
        /// 测试主题
        /// </summary>
        static void TestTopic()
        {

            Console.Title = "主题--生产者2";


            //生产者
            var __uri = new Uri(string.Concat(connString));
            IConnectionFactory _factory = new ConnectionFactory(__uri);
            using (IConnection _conn = _factory.CreateConnection(user, pwd))
            {
                using (ISession _session = _conn.CreateSession())
                {
                    IDestination _destination = SessionUtil.GetTopic(_session, topicName);
                    using (IMessageProducer producer = _session.CreateProducer(_destination))
                    {
                        //可以写入字符串，也可以是一个xml字符串等
                        while (true)
                        {
                            Console.WriteLine("请输入主题内容:");
                            string context = Console.ReadLine();
                            ITextMessage request = _session.CreateTextMessage(context);
                            producer.Send(request);
                            Console.WriteLine("发送新新主题：" + context);
                            System.Threading.Thread.Sleep(200);

                        }
                    }
                }
            }
        }

        /// <summary>
        /// 测试队列
        /// </summary>
        static void TestQueue()
        {
            Console.Title = "生产者2";


            //生产者
            var __uri = new Uri(string.Concat(connString));
            IConnectionFactory _factory = new ConnectionFactory(__uri);
            using (IConnection _conn = _factory.CreateConnection(user, pwd))
            {
                using (ISession _session = _conn.CreateSession())
                {
                    IDestination _destination = SessionUtil.GetQueue(_session, queueName);
                    using (IMessageProducer producer = _session.CreateProducer(_destination))
                    {
                        //可以写入字符串，也可以是一个xml字符串等
                        while (true)
                        {
                            Console.WriteLine("请输入队列内容:");
                            string context = Console.ReadLine();
                            ITextMessage request = _session.CreateTextMessage(context);
                            producer.Send(request);
                            Console.WriteLine("发送新新队列信息：" + context);
                            System.Threading.Thread.Sleep(200);

                        }
                    }
                }
            }
        }
    }
}
