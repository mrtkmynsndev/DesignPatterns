using System;
using System.Threading.Tasks;

namespace Creational.Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Singleton!");

            //Singleton Creational tasarım kalıbıdır.
            //Creational -> nesleri yaratmak için kullanırızs
            //Oluşturulan bir Örneğing herkes tarafından kullanılması amaçlanmıştır.
            //Ne zaman kullanmamalıyız!
            //Nesneler aslında pahallı objelerdir.
            //Herkes aynı şeyi kullanacak mı sorunsalı!
            //Yanlızca 1 objeye ihtiyacımız var olduğunda.
            //Objeye her sınıftan erişebilmemiz gerektiğinde.
            //Obje ihtiyacımız olana kadar yaratılmasın (Lazy Initializing)
            //Güncel teknolojiler kullanarak bunları daha aktif kullanabiliriz. IOC Container ile yapabiliriz.: Ninject, Castle.Windsor gibi

            /* 
                Örnek Senaryo:
                Gerçek dünyada LoadBalancing sınıfı singleton olmalıdır. 
                Sadece bu sınıftan bir tane örnek oluşması gerekiyor çünkü
                sunucular dinamik oalrak ya online ya da offlime modunda olabiliyorlar.
                Bu sunuya gönderilen her istek (request) web famrm durumunu hakkında yetkili bilgiye sahip olan
                bu sınıf üzerinden gerçekleşmelidir

            */

            LoadBalancer loadBalancer = LoadBalancer.Instance;
            LoadBalancer loadBalancer2 = LoadBalancer.Instance;
            LoadBalancer loadBalancer3 = LoadBalancer.Instance;
            LoadBalancer loadBalancer4 = LoadBalancer.Instance;


            LoadBalancer loadBalancer5 = LoadBalancer.Instance;


            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine($"Dispatch request to server {loadBalancer5.Server}");
            }

            SingletonLazy singletonLazy = SingletonLazy.Instance;
            SingletonLazy singletonLazy2 = SingletonLazy.Instance;

            if(singletonLazy == singletonLazy2)
                Console.WriteLine("SingletonLazy objects are the instance");
        }

        private static void ThreadSafe()
        {

            LoadBalancerThread loadBalancerThread = LoadBalancerThread.Instance;

            Task.Run(() =>
            {
                LoadBalancerThread loadBalancerThread2 = LoadBalancerThread.Instance;

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Dispatch request to server {loadBalancerThread2.Server}");
                }
            });

            Task.Run(() =>
            {
                LoadBalancerThread loadBalancerThread3 = LoadBalancerThread.Instance;

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Dispatch request to server {loadBalancerThread3.Server}");
                }
            });
        }
    }
}
