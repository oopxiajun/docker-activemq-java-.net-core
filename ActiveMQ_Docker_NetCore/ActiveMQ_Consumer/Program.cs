using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.Util;
namespace ActiveMQ_Consumer
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
        /// 队列测试
        /// </summary>
        /// <param name="args"></param>
        static void TestTopic()
        {
            Console.Title = "主题--消费者1";

            //消费者
            System.Threading.Tasks.Task.Run(() =>
            {
                Uri _uri = new Uri(String.Concat(connString));
                IConnectionFactory factory = new ConnectionFactory(_uri);
                using (IConnection conn = factory.CreateConnection(user, pwd))
                {
                    using (ISession session = conn.CreateSession())
                    {
                        conn.Start();
                        IDestination destination = SessionUtil.GetTopic(session, topicName);
                        using (IMessageConsumer consumer = session.CreateConsumer(destination))
                        {
                            consumer.Listener += (IMessage message) =>
                            {
                                ITextMessage msg = (ITextMessage)message;
                                Console.WriteLine("接收消息：" + msg.Text);
                            };
                            Console.ReadLine();
                        }
                    }
                }
            });

            Console.ReadLine();

        }

        /// <summary>
        /// 队列测试
        /// </summary>
        /// <param name="args"></param>
        static void TestQueue()
        {
            Console.Title = "队列--消费者2";

            //消费者
            System.Threading.Tasks.Task.Run(() =>
            {
                Uri _uri = new Uri(String.Concat(connString));
                IConnectionFactory factory = new ConnectionFactory(_uri);
                using (IConnection conn = factory.CreateConnection(user, pwd))
                {
                    using (ISession session = conn.CreateSession())
                    {
                        conn.Start();
                        IDestination destination = SessionUtil.GetQueue(session, queueName);
                        using (IMessageConsumer consumer = session.CreateConsumer(destination))
                        {
                            consumer.Listener += (IMessage message) =>
                            {
                                ITextMessage msg = (ITextMessage)message;
                                Console.WriteLine("接收消息：" + msg.Text);
                            };
                            Console.ReadLine();
                        }
                    }
                }
            });

            Console.ReadLine();

        }
    }
}
